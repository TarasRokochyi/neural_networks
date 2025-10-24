using MultiLevelNeurons;
using MultiLevelNeurons.Data;

namespace MultiLevelNeuronsApi.Services
{
    public interface IMedicalDatasetService
    {
        Network network { get; set; }
        List<IData> Dataset { get; set; }
    }
}
