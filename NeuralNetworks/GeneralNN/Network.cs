using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneralNN
{
    public enum TypeOfLearning
    {
        Accuracy,
        Iteration
    }
    public class Network
    {
        public static int OutputNeuronCount;
        public static int HiddenLayerNeuronCount;
        public Normalizer Normalizer;
        public Neuron[] OutputNeurons;
        public List<Neuron[]> HiddenLayerNeurons;
        public Network(ActivationType inputType, ActivationType hiddenType, ActivationType outputType, int[] layers)
        {
            HiddenLayerNeurons = new List<Neuron[]>();
            for (int i = 1; i < layers.Length; i++)
            {
                if (i == layers.Length - 1)
                {
                    OutputNeurons = new Neuron[layers[i]];
                    for (int j = 0; j < OutputNeurons.Length; j++)
                    {
                        OutputNeurons[j] = new Neuron(layers[i - 1], outputType);
                    }
                    break;
                }
                Neuron[] neurons = new Neuron[layers[i]];
                for (int j = 0; j < neurons.Length; j++)
                {
                    neurons[j] = new Neuron(layers[i - 1], hiddenType);
                }
                HiddenLayerNeurons.Add(neurons);
            }
        }

        public int learn(Normalizer normalizer, List<Instance> instances, TypeOfLearning typeOfLearning, double accuracy, int iter)
        {
            this.Normalizer = normalizer;
            bool global_all_right = false;
            int iteration = 0;
            if (typeOfLearning == TypeOfLearning.Accuracy)
            {
                while (!global_all_right) //actual ERA
                {
                    global_all_right = true;
                    for (int j = 0; j < instances.Count; j++)
                    {
                        if (!memorize(instances[j], accuracy))
                            global_all_right = false;
                    }
                    iteration++;
                    Console.WriteLine(iteration);
                }
            }
            if (typeOfLearning == TypeOfLearning.Iteration)
            {
                while (iteration < iter)
                {
                    global_all_right = true;
                    for (int j = 0; j < instances.Count; j++)
                    {
                        if (!memorize(instances[j], accuracy))
                            global_all_right = false;
                    }
                    iteration++;
                    Console.WriteLine(iteration);
                }
            }
            return iteration;
        }

        private bool memorize(Instance instance, double accuracy = 0.001)
        {
            double[] y_output = new double[OutputNeurons.Length];
            double[] y_middle = new double[HiddenLayerNeuronCount];
            double[] y_output_error = new double[OutputNeurons.Length];
            double average_error = 1;
            bool global_all_right = true;
            double eps;
            double delta = 0;
            double[] output_delta = new double[OutputNeurons.Length];

            while (true)
            {
                List<double[]> input_in_hidden = new List<double[]>();

                double[] X = instance.X;
                input_in_hidden.Add(X);
                double[] y = new double[HiddenLayerNeurons[0].Length];
                for (int i = 0; i < HiddenLayerNeurons.Count; i++)       //get y from middle layer
                {
                    for (int j = 0; j < HiddenLayerNeurons[i].Length; j++)
                    {
                        y[j] = HiddenLayerNeurons[i][j].check(X);
                    }
                    X = y;
                    input_in_hidden.Add(X);
                    if (i == HiddenLayerNeurons.Count - 1) break;
                    y = new double[HiddenLayerNeurons[i + 1].Length];
                }

                for (int i = 0; i < OutputNeurons.Length; i++)       //get y from output layer
                {
                    y_output[i] = OutputNeurons[i].check(X);
                }
                double[] dDenormalized = Normalizer.Denormalize(instance.d);
                double[] yDenormalized = Normalizer.Denormalize(y_output);
                for (int i = 0; i < OutputNeurons.Length; i++)       //get error from output layer
                {
                    // Change y_output[i] to yDenormalized[i]
                    //y_output_error[i] = Math.Abs(y_output[i] - instance.d[i]);
                    y_output_error[i] = Math.Abs(yDenormalized[i] - dDenormalized[i]);
                    //average_error += y_output_error[i];
                }
                //average_error /= (double)OutputNeurons.Length;

                //Console.WriteLine("y output error");
                //foreach(var er in y_output_error)
                //    Console.Write(er + " | ");
                //Console.WriteLine();

                if (!hasMoreThan(y_output_error, accuracy)) break;

                global_all_right = false;

                //Console.WriteLine("Weights of output layer before");
                //foreach(var v in OutputNeurons[0].w)
                //    Console.Write(v + " | ");
                //Console.WriteLine();

                for (int i = 0; i < OutputNeurons.Length; i++)            // change W of output layer
                {
                    output_delta[i] = y_output[i] * (1.0 - y_output[i]) * (instance.d[i] - y_output[i]);
                    OutputNeurons[i].changeW(X, output_delta[i]);
                }

                //Console.WriteLine("Weights of output layer after");
                //foreach(var v in OutputNeurons[0].w)
                //    Console.Write(v + " | ");
                //Console.WriteLine();

                for (int n = HiddenLayerNeurons.Count - 1; n >= 0; n--)
                {
                    //Console.WriteLine("Weights of hidden layer before");
                    //foreach (var v in HiddenLayerNeurons[n][0].w)
                    //    Console.Write(v + " | ");
                    //Console.WriteLine();

                    double[] output_delta_prev = new double[HiddenLayerNeurons[n].Length];
                    for (int j = 0; j < HiddenLayerNeurons[n].Length; j++)       //change W of hidden layer
                    {
                        double neuron_error = 0;
                        delta = 0;
                        if (n == HiddenLayerNeurons.Count - 1)
                        {
                            for (int i = 0; i < OutputNeurons.Length; i++)   //get error for middle neuron 
                            {
                                neuron_error += output_delta[i] * OutputNeurons[i].w[j];
                            }
                        }
                        else
                        {
                            for (int i = 0; i < HiddenLayerNeurons[n + 1].Length; i++)
                            {
                                neuron_error += output_delta[i] * HiddenLayerNeurons[n + 1][i].w[j];
                            }
                        }
                        delta = input_in_hidden[n + 1][j] * (1 - input_in_hidden[n + 1][j]) * neuron_error;
                        output_delta_prev[j] = delta;
                        HiddenLayerNeurons[n][j].changeW(input_in_hidden[n], delta);
                    }
                    //Console.WriteLine("Weights of hidden layer after");
                    //foreach (var v in HiddenLayerNeurons[n][0].w)
                    //    Console.Write(v + " | ");
                    //Console.WriteLine();
                    //Console.WriteLine();
                    output_delta = output_delta_prev;
                }
            }


            return global_all_right;
        }

        public double[] getResult(Instance instance)
        {
            double[] X = instance.X;
            double[] y = new double[HiddenLayerNeurons[0].Length];
            for (int i = 0; i < HiddenLayerNeurons.Count; i++)
            {
                for (int j = 0; j < HiddenLayerNeurons[i].Length; j++)
                {
                    y[j] = HiddenLayerNeurons[i][j].check(X);
                }
                X = y;
                if (i == HiddenLayerNeurons.Count - 1) break;
                y = new double[HiddenLayerNeurons[i + 1].Length];
            }
            double[] y_output = new double[OutputNeurons.Length];
            for (int i = 0; i < OutputNeurons.Length; i++)       //get y from output layer
            {
                y_output[i] = OutputNeurons[i].check(X);
            }
            return y_output;
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
