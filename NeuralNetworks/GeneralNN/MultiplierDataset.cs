using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneralNN
{
    public static class MultiplierDataset
    {
        public static List<Instance> dataset = new List<Instance>();

        static MultiplierDataset()
        {
            Instance[] default_set = new Instance[]
            {
                //new Instance(new double[] {2, 1 }, new double[] {2}),
                //new Instance(new double[] {2, 2 }, new double[] {4}),
                //new Instance(new double[] {2, 3 }, new double[] {6}),
                //new Instance(new double[] {2, 4 }, new double[] {8}),
                //new Instance(new double[] {2, 6 }, new double[] {12}),
                //new Instance(new double[] {2, 7 }, new double[] {14}),
                //new Instance(new double[] {2, 8 }, new double[] {16}),
                //new Instance(new double[] {2, 9 }, new double[] {18}),
                //new Instance(new double[] {3, 3 }, new double[] {9}),
                //new Instance(new double[] {3, 4 }, new double[] {12}),
                //new Instance(new double[] {3, 5 }, new double[] {15}),
                //new Instance(new double[] {3, 6 }, new double[] {18}),
                //new Instance(new double[] {3, 7 }, new double[] {21}),
            new Instance( new double[2] { 2, 1 }, new double[1] { 2 }),
            new Instance(new double[2] { 2, 2 }, new double[1] { 4 }),
            new Instance(new double[2] { 2, 3 }, new double[1] { 6 }),
            new Instance(new double[2] { 2, 4 }, new double[1] { 8 }),

            new Instance(new double[2] { 2, 6 }, new double[1] { 12 }),
            new Instance(new double[2] { 2, 7 }, new double[1] { 14 }),
            new Instance(new double[2] { 2, 8 }, new double[1] { 16 }),
            new Instance(new double[2] { 2, 9 }, new double[1] { 18 }),

            new Instance(new double[2] { 3, 1 }, new double[1] { 3 }),
            new Instance(new double[2] { 3, 2 }, new double[1] { 6 }),
            new Instance(new double[2] { 3, 3 }, new double[1] { 9 }),
            new Instance(new double[2] { 3, 4 }, new double[1] { 12 }),
            new Instance(new double[2] { 3, 5 }, new double[1] { 15 }),
            new Instance(new double[2] { 3, 6 }, new double[1] { 18 }),
            new Instance(new double[2] { 3, 7 }, new double[1] { 21 }),
            new Instance(new double[2] { 3, 8 }, new double[1] { 24 }),
            new Instance(new double[2] { 3, 9 }, new double[1] { 27 })

        };

            dataset.AddRange(default_set);
        }
    }
}
