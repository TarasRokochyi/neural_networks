using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneralNN
{
    public class Normalizer
    {
        double[] XMin;
        double[] XMax;
        double[] dMin;
        double[] dMax;
        public Normalizer(List<Instance> instances)
        {
            XMin = (double[])instances[0].X.Clone();
            XMax = (double[])instances[0].X.Clone();
            dMin = (double[])instances[0].d.Clone();
            dMax = (double[])instances[0].d.Clone();

            foreach (Instance ins in instances)
            {
                for (int i = 0; i < ins.X.Length; i++)
                {
                    if (XMin[i] > ins.X[i])
                        XMin[i] = ins.X[i];
                    if (XMax[i] < ins.X[i])
                        XMax[i] = ins.X[i];
                }
                for (int i = 0; i < ins.d.Length; i++)
                {
                    if (dMin[i] > ins.d[i])
                        dMin[i] = ins.d[i];
                    if (dMax[i] < ins.d[i])
                        dMax[i] = ins.d[i];
                }
            }
        }
        public List<Instance> Normalize(List<Instance> instances)
        {

            List<Instance> instances1 = new List<Instance>();
            foreach (var ins in instances)
            {
                instances1.Add(Norm(ins));
            }
            return instances1;

        }
        public Instance Norm(Instance instance)
        {
            double[] X = new double[instance.X.Length];
            for (int i = 0; i < instance.X.Length; i++)
            {
                X[i] = (instance.X[i] - XMin[i]) / (XMax[i] - XMin[i]);
            }
            double[] d = new double[instance.d.Length];
            for (int i = 0; i < instance.d.Length; i++)
            {
                d[i] = (instance.d[i] - dMin[i]) / (dMax[i] - dMin[i]);
            }
            return new Instance(X, d);
        }
        public double[] Denormalize(double[] d)
        {
            double[] _d = new double[d.Length];
            for (int i = 0; i < d.Length; i++)
            {
                _d[i] = dMin[i] + d[i] * (dMax[i] - dMin[i]);
            }
            return _d;
        }
    }

}
