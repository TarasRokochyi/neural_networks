using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace MultiLevelNeurons
{
    public class Network
    {
        public static int NeuronCount;
        public static int HiddenLayerCount;
        public Neuron[] neurons;
        public Neuron[] hidden_layer;

        // Network for Medical Diagnostic
        public Network(int neuronCount, int hiddenLayerCount, int inputCount)
        {
            NeuronCount = neuronCount;
            HiddenLayerCount = hiddenLayerCount;
            neurons = new Neuron[NeuronCount];
            for (int i = 0; i < neurons.Length; i++)
                neurons[i] = new Neuron(HiddenLayerCount);
            hidden_layer = new Neuron[HiddenLayerCount];
            for (int i = 0; i < hidden_layer.Length; i++)
                hidden_layer[i] = new Neuron(inputCount);
        }

        // network for Letter recognition
        public Network()
        {
            NeuronCount = 26;
            HiddenLayerCount = 60;
            neurons = new Neuron[NeuronCount];
            for (int i = 0; i < neurons.Length; i++)
                neurons[i] = new Neuron(HiddenLayerCount);
            hidden_layer = new Neuron[HiddenLayerCount];
            for (int i = 0; i < hidden_layer.Length; i++)
                hidden_layer[i] = new Neuron(40*40);

        }

        public int learn(List<IData> dataset)
        {
            bool global_all_right = false;
            int iteration = 0;
            while (!global_all_right) //actual ERA
            {
                global_all_right = true;
                for (int j = 0; j < dataset.Count; j++)
                {
                    if (!memorize_data(dataset[j], out int counter))
                        global_all_right = false;


                    double[] y_output = new double[NeuronCount];
                    double[] y_middle = new double[HiddenLayerCount];
                    for (int i = 0; i < HiddenLayerCount; i++)      //get y from middle layer
                        y_middle[i] = hidden_layer[i].check(dataset[j].X);

                    Console.WriteLine($"{counter} | ");
                    for (int i = 0; i < NeuronCount; i++)           //get y from output layer
                    {        
                        y_output[i] = neurons[i].check(y_middle);
                        Console.Write($"{Math.Round(y_output[i], 5)} ");
                    }
                    Console.WriteLine();


                }
                iteration++;
            }
            return iteration;
        }

        private bool memorize_data(IData data, out int counter)
        {

            double[] y_output = new double[NeuronCount];
            double[] y_middle = new double[HiddenLayerCount];
            double[] y_output_error = new double[NeuronCount];
            //double average_error = 1;
            bool global_all_right = true;
            //double eps;
            double delta = 0;
            double[] output_delta = new double[NeuronCount];

            counter = 0;

            while (true)
            {
                for (int i = 0; i < HiddenLayerCount; i++)      //get y from middle layer
                    y_middle[i] = hidden_layer[i].check(data.X);

                for (int i = 0; i < NeuronCount; i++)       //get y from output layer
                    y_output[i] = neurons[i].check(y_middle);

                for (int i = 0; i < NeuronCount; i++)       //get error from output layer
                {
                    y_output_error[i] = Math.Abs(y_output[i] - data.D[i]);
                    //Console.Write($"{y_output_error[i]} ");
                    //average_error += y_output_error[i];
                }
                //Console.WriteLine();
                //average_error /= (double)NeuronCount;

                if (!hasMoreThan(y_output_error, 0.15)) break;

                global_all_right = false;

                for (int i = 0; i < NeuronCount; i++)            // change W of output layer
                {
                    output_delta[i] = y_output[i] * (1.0 - y_output[i]) * (data.D[i] - y_output[i]);
                    neurons[i].changeW(y_middle, output_delta[i]);
                }

                for (int j = 0; j < HiddenLayerCount; j++)       //change W of hidden layer
                {
                    double neuron_error = 0;

                    for (int i = 0; i < NeuronCount; i++)   //get error for middle neuron 
                        neuron_error += output_delta[i] * neurons[i].w[j];

                    delta = y_middle[j] * (1 - y_middle[j]) * neuron_error;
                    hidden_layer[j].changeW(data.X, delta);
                }

                counter++;
                //Console.WriteLine($"{counter++}");
            }

            return global_all_right;
        }

        public double[] getResult(IData data)
        {
            double[] y = new double[NeuronCount];
            double[] y_middle = new double[HiddenLayerCount];
            for (int i = 0; i < HiddenLayerCount; i++)      //get y from middle layer
            {
                y_middle[i] = hidden_layer[i].check(data.X);
            }
            for (int i = 0; i < NeuronCount; i++)
            {
                y[i] = neurons[i].check(y_middle);
            }
            return y;
        }

        private bool hasMoreThan(double[] arr, double num)
        {
            for (int i = 0; i < arr.Length; i++)
            {
                if (arr[i] > num)
                    return true;
            }
            return false;
        }

    }
}
