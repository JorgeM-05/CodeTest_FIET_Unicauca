using API.Seeker.Data;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Seeker.Services.Send.Commands;
using System.Threading.Tasks;

namespace Api.Seeker.Controllers
{
    [ApiController]
    [Route("Cas/Opt")]
    public class CasOptimizer : ControllerBase
    {
        private readonly APIContext _context;
        private readonly IMediator _mediator;
        public CasOptimizer(
            IMediator mediator,
            APIContext context)
        {
            _context = context;
            _mediator = mediator;
        }

        [HttpGet]
        public string Get()
        {
            return "TestCase Controlador de Optimizador";
        }

        [HttpPost]
        public async Task<IActionResult> Create(CasOptimizerCommand command)
        {
            await _mediator.Publish(command);
            return Ok();
        }
    }

}