using Microsoft.AspNetCore.Mvc;
using MultiLevelNeurons;
using MultiLevelNeurons.Data;
using MultiLevelNeuronsApi.Services;
using System.Data;

namespace MultiLevelNeuronsApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MedicalDiagnosticController : ControllerBase
    {
        private readonly IMedicalDatasetService medicalDatasetService;

        public MedicalDiagnosticController(IMedicalDatasetService medicalDatasetService)
        {
            this.medicalDatasetService = medicalDatasetService;
        }

        [HttpGet("learn")]
        public async Task<ActionResult<int>> Learn()
        {

            medicalDatasetService.Dataset = NetworkHelper.Normalize(medicalDatasetService.Dataset);
            for (int i = 0; i < medicalDatasetService.Dataset[0].X.Length; i++)
            {
                Console.WriteLine($"{medicalDatasetService.Dataset[0].X[i]}");
            }
            int numbOfIter = medicalDatasetService.network.learn(medicalDatasetService.Dataset);

            return Ok(numbOfIter);
        }

        [HttpPost("create")]
        public async Task<ActionResult> Create(CreateModel createModel)
        {
            medicalDatasetService.network = new Network(createModel.neuronCount, createModel.hiddenLayerCount, createModel.inputCount);
            medicalDatasetService.Dataset = new List<IData>();
            return Ok();
        }

        [HttpPost("verify")]
        public async Task<ActionResult<double[]>> Verify(MedicalCase data)
        {
            double[] result = medicalDatasetService.network.getResult(data);

            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult> AddDataToDataset(MedicalCase data)
        {
            medicalDatasetService.Dataset.Add(data);

            return Ok();
        }
    }
}
