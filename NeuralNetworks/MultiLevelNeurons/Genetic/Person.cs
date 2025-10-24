using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiLevelNeurons.Genetic
{
    public class Person
    {
        public uint[] X;
        public double y;
        public uint genMask;
        public Person(int ChromosAmount, int genAmount)
        {
            Random rand = new Random();
            X = new uint[ChromosAmount];
            genMask = 0;
            for (int i = 0; i < genAmount; i++)
            {
                genMask = genMask << 1;
                genMask++;
            }
        }
        public void XSet(int index, uint value)
        {
            if (value > genMask) value = genMask;
            X[index] = value;
        }
    }
}
