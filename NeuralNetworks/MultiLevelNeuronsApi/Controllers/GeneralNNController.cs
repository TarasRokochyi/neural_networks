using Microsoft.AspNetCore.Mvc;
using MultiLevelNeuronsApi.Services;
using GeneralNN;

namespace MultiLevelNeuronsApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GeneralNNController : ControllerBase
    {
        private readonly IGeneralNNService _generalNNService;

        public GeneralNNController(IGeneralNNService generalNNService)
        {
            _generalNNService = generalNNService;
        }

        [HttpGet]
        public async Task<ActionResult<int>> Learntest()
        {
            var statement = 1;
            return Ok();
        }

        [HttpPost("learn")]
        public async Task<ActionResult<int>> Learn(LearnModel learnModel)
        {
            var instances = _generalNNService.Dataset;
            var normalizer = new Normalizer(instances);
            _generalNNService.network.learn(normalizer, normalizer.Normalize(instances), learnModel.TypeOfLearning, learnModel.Accuracy, learnModel.Iterations);

            return Ok();
        }

        [HttpPost("create")]
        public async Task<ActionResult> Create(CreateGeneralNNModel createModel)
        {
            _generalNNService.network = new Network(createModel.inputType, createModel.hiddenType, createModel.outputType, createModel.layers);
            _generalNNService.Dataset = MultiplierDataset.dataset;

            return Ok();
        }

        [HttpPost("verify")]
        public async Task<ActionResult<double[]>> Verify(Instance data)
        {
            var norm_data = _generalNNService.network.Normalizer.Norm(data);
            var result = _generalNNService.network.getResult(norm_data);
            result = _generalNNService.network.Normalizer.Denormalize(result);
            return Ok(result);
        }

        [HttpPost("addtodataset")]
        public async Task<ActionResult> AddDataToDataset(List<Instance> data)
        {
            _generalNNService.Dataset.AddRange(data);

            return Ok();
        }

    }
}
