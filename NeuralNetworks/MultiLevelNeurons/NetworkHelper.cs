using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiLevelNeurons
{
    public static class NetworkHelper
    {
        private const double MAX = 3;
        private const double MIN = 0;

        static NetworkHelper() { }
        public static List<IData> Normalize(List<IData> Dataset)
        {
            for (int i = 0; i < Dataset.Count; i++)
            {
                double[] X = Dataset[i].X;
                for (int j = 0; j < X.Length; j++)
                    X[j] = (X[j] - MIN) / (MAX - MIN);

                double[] D = Dataset[i].D;
                for (int j = 0; j < D.Length; j++)
                    D[j] = (D[j] - MIN) / (MAX - MIN);
            }

            return Dataset;
        }

    }
}
