using MediatR;
using Microsoft.AspNetCore.Mvc;
using Optimizer.Services.BusinessLogic;
using Optimizer.Services.BusinessLogic.Commands;
using Optimizer.Services.Commands;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Optimizer.Api.Controllers
{
    [ApiController]
    [Route("/Optimizer")]
    public class CasOptController : ControllerBase
    {
        private readonly IOptimizerLogic _OptimizerLogic;
        private readonly IMediator _mediator;

        public CasOptController(
            IOptimizerLogic OptimizerLogic,
            IMediator mediator
        )
        {
            _OptimizerLogic = OptimizerLogic;
            _mediator = mediator;
        }


        [HttpPost]
        public async Task<ActionResult<CasOptLogic>> Create(CasOptLogic command)
        {
            return await _OptimizerLogic.OptLogic(command);
        }

        [HttpPost("Logic")]
        public async Task<ActionResult<CasOptLogic>> Logic(DictionaryModel command)
        {
            return await _OptimizerLogic.Dictionary(command);
        }


        [HttpGet]
        public string Get()
        {
            return " Logica para Optimizador ";
        }
    }
}
