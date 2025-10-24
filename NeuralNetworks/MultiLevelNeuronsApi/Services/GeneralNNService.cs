using GeneralNN;

namespace MultiLevelNeuronsApi.Services
{
    public class GeneralNNService : IGeneralNNService
    {
        // network from GeneralNN project
        public Network network { get; set; }
        public List<Instance> Dataset { get; set; }
    }
}
