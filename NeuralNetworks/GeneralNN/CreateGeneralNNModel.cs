using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneralNN
{
    public class CreateGeneralNNModel
    {
        public ActivationType inputType { get; set; }
        public ActivationType hiddenType { get; set; }
        public ActivationType outputType { get; set; }
        public int[] layers { get; set; }

        public CreateGeneralNNModel()
        {

        }

        public CreateGeneralNNModel(ActivationType inputType, ActivationType hiddenType, ActivationType outputType, int[] layers)
        {
            this.inputType = inputType;
            this.hiddenType = hiddenType;
            this.outputType = outputType;
            this.layers = layers;
        }
    }
}
