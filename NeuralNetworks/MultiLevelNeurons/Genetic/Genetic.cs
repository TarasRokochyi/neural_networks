using MultiLevelNeurons.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiLevelNeurons.Genetic
{
    public class Genetic
    {
        double _crossoverProb;
        double _mutationProb;
        int _populationAmount;
        int chromosAmount;
        int genAmount;
        List<Person> people;
        List<Person> historyOfBestPerson;
        Network neuralNetwork;
        Random random = new Random();
        public Genetic(Network neuralNetwork, int populationAmount, double crossoverProb, double mutationProp)
        {
            this.neuralNetwork = neuralNetwork;
            _populationAmount = populationAmount;
            this.genAmount = 6;
            _crossoverProb = crossoverProb;
            _mutationProb = mutationProp;
            this.chromosAmount = 0;
            for(int i = 0; i < neuralNetwork.hidden_layer.Length; i++)
            {
                chromosAmount += neuralNetwork.hidden_layer[i].w.Length;
            }
            for(int i = 0; i < neuralNetwork.neurons.Length; i++)
            {
                chromosAmount += neuralNetwork.neurons[i].w.Length;
            }
            people = new List<Person>();
            for (int i = 0; i < populationAmount; i++)
            {
                Person p = new Person(chromosAmount, genAmount);

                for (int j = 0; j < chromosAmount; j++)
                {
                    p.XSet(j, (uint)random.Next(127));
                }
                people.Add(p);
            }
        }
        public List<Person> Evolution(List<IData> instances, int iter)
        {
            historyOfBestPerson = new List<Person>();
            int generation = 0;

            while (generation < iter)
            {
                // start loop  // check for 5 same best result
                double ySum = 0; // sum of all y's
                double yBigest = double.MaxValue;
                int iBigestPerson = 0;
                for (int i = 0; i < people.Count; i++)
                {
                    people[i].y = NNMAE(people[i], instances);
                    ySum += people[i].y;
                    if (people[i].y < yBigest)
                    {
                        yBigest = people[i].y;
                        iBigestPerson = i;
                    }
                }
                historyOfBestPerson.Add(people[iBigestPerson]);
                Console.WriteLine($"Generation {generation++}");
                //if (checkForСonvergent(historyOfBestPerson))
                //{
                //    GenerateNN(historyOfBestPerson[historyOfBestPerson.Count - 1]);
                //    break;
                //}// break if last 5 resultst the same
                double[] personProb = new double[_populationAmount]; // probability of each person
                for (int i = 0; i < _populationAmount; i++)
                {
                    personProb[i] = people[i].y / ySum;
                    personProb[i] = 1.0 - personProb[i];  //for minimizing
                }
                int[] selectionIndexes = new int[_populationAmount];
                for (int i = 0; i < _populationAmount; i++) // rulet selection
                {
                    double prob = random.NextDouble();
                    double sum = 0;
                    for (int j = 0; j < _populationAmount; j++)
                    {
                        sum += personProb[j];
                        if (sum > prob)
                        {
                            selectionIndexes[i] = j;
                            break;
                        }
                    }
                }
                List<Person> newPopulation = new List<Person>();
                for (int i = 1; i < _populationAmount; i += 2) // start crossover
                {
                    double prob = random.NextDouble();
                    if (prob < _crossoverProb)
                    {
                        Cross(newPopulation, people[selectionIndexes[i - 1]], people[selectionIndexes[i]]);
                    }
                    else
                    {
                        newPopulation.Add(people[selectionIndexes[i - 1]]);
                        newPopulation.Add(people[selectionIndexes[i]]);
                    }
                }
                for (int i = 0; i < _populationAmount; i++) // start mutation
                {
                    for (int j = 0; j < chromosAmount; j++)
                    {
                        uint mutationMask = 1;
                        for (int k = 0; k < genAmount; k++)
                        {
                            double prob = random.NextDouble();
                            if (prob < _mutationProb)
                                newPopulation[i].X[j] ^= mutationMask;
                            mutationMask <<= 1;
                        }
                    }
                }
                people = new List<Person>();
                foreach (var p in newPopulation)
                {
                    people.Add(p);
                }
            }


            return historyOfBestPerson;
        }
        public bool checkForСonvergent(List<Person> history)
        {
            if (history.Count < 5)
                return false;
            double check = history[history.Count - 1].y;
            for (int i = history.Count - 1; i >= history.Count - 5; i--)
            {
                if (history[i].y != check)
                {
                    history.RemoveAt(0);
                    return false;
                }
            }
            return true;
        }


        private void Cross(List<Person> newPopulation, Person p1, Person p2)
        {
            Person n1 = new Person(chromosAmount, genAmount);
            Person n2 = new Person(chromosAmount, genAmount);
            for (int i = 0; i < chromosAmount; i++)
            {
                int point = random.Next(1, genAmount);
                uint mask = p1.genMask;
                uint left = (mask >> point) << point;
                uint right = mask ^ left;
                uint val1 = (p1.X[i] & left) | (p2.X[i] & right);
                uint val2 = (p2.X[i] & left) | (p1.X[i] & right);
                n1.XSet(i, val1);
                n2.XSet(i, val2);
            }
            newPopulation.Add(n1);
            newPopulation.Add(n2);
        }

        public double func1(uint[] x)
        {
            return Math.Cos(x[0] / 23.0) * Math.Sin(x[0] / 50.0) + 2.0;
        }
        public double NNMAE(Person person, List<IData> instances)
        {
            GenerateNN(person);
            double MeanForAll = 0;
            for(int i = 0; i < instances.Count; i++)
            {
                double meanAverageError = 0;
                double[] res = neuralNetwork.getResult(instances[i]);
                for(int j = 0; j < res.Length; j++)
                {
                    meanAverageError += Math.Abs(res[j] - instances[i].D[j]);
                }
                meanAverageError /= res.Length;
                MeanForAll += meanAverageError; 
            }
            double ret = MeanForAll /= instances.Count;
            Console.WriteLine(ret);
            return ret;
        }
        public void GenerateNN(Person person)
        {
            int n = 0;
            for(int j = 0; j < neuralNetwork.hidden_layer.Length; j++)
            {
                for (int k = 0; k < neuralNetwork.hidden_layer[j].w.Length; k++)
                {
                    double x = ((int)person.X[n++]);
                    double w = ((x - 32) / 64);
                    neuralNetwork.hidden_layer[j].w[k] = w;
                }
            }

            for(int i = 0; i < neuralNetwork.neurons.Length; i++)
            {
                for(int k = 0; k < neuralNetwork.neurons[i].w.Length; k++)
                {
                        double x = ((int)person.X[n++]);
                        double w = ((x - 32) / 64);
                    neuralNetwork.neurons[i].w[k] = w; // ((((int)person.X[n++]) - 1000.0) / 100.0);
                }
            }
        }
    }
}
