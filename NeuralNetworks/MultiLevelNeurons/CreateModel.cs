using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiLevelNeurons
{
    public class CreateModel
    {
        public int neuronCount { get; set; }
        public int hiddenLayerCount {  get; set; }
        public int inputCount { get; set; }

        public CreateModel()
        {
        }

        public CreateModel( int neuronCount, int hiddenLayerCount, int inputCount)
        {
            this.neuronCount = neuronCount;
            this.hiddenLayerCount = hiddenLayerCount;
            this.inputCount = inputCount;
        }
    }
}
