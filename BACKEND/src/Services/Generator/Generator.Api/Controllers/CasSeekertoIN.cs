
using Generator.Services.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Generator.Api.Controllers
{
    [ApiController]
    [Route("/Generator")]
    public class CasSeekertoIN : ControllerBase
    {
        private readonly IMediator _mediator;
        public CasSeekertoIN(
            IMediator mediator
            )
        {
            _mediator = mediator;
        }


        [HttpPost]
        public async Task<IActionResult> Create(CasSeekertoGeneratorlogic command)
        {
            await _mediator.Publish(command);
            return Ok();
        }
        //---------------
        [HttpGet]
        public string Get()
        {
            return "Estamos en Receptor de Seeker o Api gateway";
        }
    }
}
