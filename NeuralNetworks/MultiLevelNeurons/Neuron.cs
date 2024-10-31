using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiLevelNeurons
{
    public class Neuron
    {
        public double[] w;
        public double w0;
        public static Random rand = new Random();
        public Neuron(int w_quantity)
        {
            w = new double[w_quantity];
            for (int i = 0; i < w.Length; i++)
                w[i] = rand.Next(-5, 5) / 10.0;
            w0 = rand.Next(-5, 5) / 10.0;
        }

        public double check(double[] input)
        {
            double s = 0;
            s += w0;
            for (int i = 0; i < w.Length; i++)
            {
                s += w[i] * input[i];
            }
            return 1.0 / (1.0 + Math.Exp(-s));
        }

        public void changeW(double[] input, double delta)
        {
            double x;
            w0 = w0 + delta;
            for (int i = 0; i < w.Length; i++)
            {
                x = input[i];
                w[i] = w[i] + delta * x;
            }
        }
    }
}
