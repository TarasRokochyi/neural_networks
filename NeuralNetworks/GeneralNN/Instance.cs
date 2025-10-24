using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneralNN
{
    public class Instance 
    {
        public double[] X { get; set; }
        public double[] d { get; set; }

        public Instance(double[] X, double[] d)
        {
            this.X = X;
            this.d = d;
        }
    }
}
