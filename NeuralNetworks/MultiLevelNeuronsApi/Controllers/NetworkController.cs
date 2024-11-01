﻿using Microsoft.AspNetCore.Mvc;
using MultiLevelNeurons;

namespace MultiLevelNeuronsApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class NetworkController : ControllerBase
    {
        private Network network;
        private List<IData> dataset;


        public NetworkController(Network network)
        {
            this.network = network;
            dataset = Letters.set_of_letters;
        }

        [HttpGet("learn")]
        public async Task<ActionResult<int>> Learn()
        {
            int numbOfIter = network.learn(dataset);

            return Ok(numbOfIter);
        }

        [HttpPost("verify")]
        public async Task<ActionResult<double[]>> Verify(Letter data)
        {
            double[] result = network.getResult(data);

            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult> AddLetterToDataset(Letter data)
        {
            dataset.Add(data);

            return Ok();
        }
    }
}
