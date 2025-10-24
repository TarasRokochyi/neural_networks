using MultiLevelNeurons;
using MultiLevelNeurons.Data;
using MultiLevelNeurons.Genetic;

namespace NetworkConsoleTest
{
    internal class Program
    {
        static void Main(string[] args)
        {
            
            Network network = new Network();

            List<IData> dataset = Letters.set_of_letters;

            Genetic gen = new Genetic(network, 20, 1, 0.3);
            gen.Evolution(dataset, 15);

            network.learn(dataset);
        }
    }
}
