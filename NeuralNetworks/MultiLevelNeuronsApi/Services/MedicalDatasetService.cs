using MultiLevelNeurons;

namespace MultiLevelNeuronsApi.Services
{
    public class MedicalDatasetService : IMedicalDatasetService
    {
        public Network network { get; set; }
        public List<IData> Dataset { get; set; }

    }
}
