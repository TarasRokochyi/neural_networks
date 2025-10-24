using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiLevelNeurons.Data
{
    public class Letter : IData
    {
        public double[] X { get; set; }
        public char letter { get; set; }
        public double[]? D { get; set; }

        public Letter(double[] doubleArray, char letter)
        {
            this.letter = letter;
            X = doubleArray;

            D = new double[26];
            for (int i = 0; i < D.Length; i++)
            {
                D[i] = char.ToUpper(letter) - 65 == i ? 1 : 0;
            }
        }

        public Letter(double[] intArray)
        {
            X = intArray;
        }
        public Letter() { }
    }
}
