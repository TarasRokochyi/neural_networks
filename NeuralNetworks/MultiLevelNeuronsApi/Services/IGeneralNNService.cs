using GeneralNN;

namespace MultiLevelNeuronsApi.Services
{
    public interface IGeneralNNService
    {
        Network network { get; set; }
        List<Instance> Dataset { get; set; }
    }
}
