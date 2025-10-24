using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneralNN
{
    public class LearnModel
    {
        public TypeOfLearning TypeOfLearning { get; set; }
        public double Accuracy { get; set; }
        public int Iterations { get; set; }

        public LearnModel(TypeOfLearning typeOfLearning, double accuracy, int iterations)
        {
            TypeOfLearning = typeOfLearning;
            Accuracy = accuracy;
            Iterations = iterations;
        }
    }
}
