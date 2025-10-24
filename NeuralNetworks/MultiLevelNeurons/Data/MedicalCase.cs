using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiLevelNeurons.Data
{
    public class MedicalCase : IData
    {
        public double[] X { get; set; }
        public double[] D { get; set; }


        public MedicalCase(double[] x, double[] d)
        {
            X = x;
            D = d;
        }
        public MedicalCase(double[] x)
        {
            X = x;
        }

        public MedicalCase() { }
    }
}
