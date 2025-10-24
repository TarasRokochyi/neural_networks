using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneralNN
{

    public enum ActivationType
    {
        Linear,
        Sigmoid,
        Ln,
        Th
    }

    public class Neuron
    {
        public double[] w;
        public double w0;
        ActivationType activation;
        public Neuron(int w_quantity, ActivationType activation)
        {
            Random rand = new Random(492);
            w = new double[w_quantity];
            for (int i = 0; i < w.Length; i++)
                w[i] = rand.Next(-20, 20) / 10.0;
            w0 = rand.Next(-20, 20) / 10.0;
            this.activation = activation;
        }
        public double check(double[] input)
        {
            double s = 0;
            s += this.w0;
            for (int i = 0; i < w.Length; i++)
            {
                s += this.w[i] * input[i];
            }
            if (activation == ActivationType.Sigmoid)
                return Sigmoid(s);
            //return s;
            return Linear(s);

        }
        public void changeW(double[] input, double delta)
        {
            double x;
            this.w0 = this.w0 + 2 * delta;
            for (int i = 0; i < w.Length; i++)
            {
                x = input[i];
                this.w[i] = this.w[i] + 2 * delta * x;
            }
        }
        private double Sigmoid(double s)
        {
            return 1.0 / (1.0 + Math.Exp(-s));
        }
        private double Linear(double s)
        {
            if (s > 0)
            {
                if (s >= 1)
                    return 0.9999;
                return s;
            }
            return 0.0001;
        }
    }
}
