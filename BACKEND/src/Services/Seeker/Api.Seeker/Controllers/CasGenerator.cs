using API.Seeker.Data;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Seeker.Services.Send.Commands;
//using Seeker.Services.Proxies.Generator.Commands;
using System.Threading.Tasks;

namespace Api.Seeker.Controllers
{
    [ApiController]
    [Route("Cas/Gen")]
    public class CasGenerator : ControllerBase
    {

        private readonly APIContext _context;
        private readonly IMediator _mediator;
        public CasGenerator(
            IMediator mediator,
            APIContext context)
        {
            _context = context;
            _mediator = mediator;
        }

        [HttpGet]
        public string Get()
        {
            return "TestCase Controlador de Generador";
        }

        [HttpPost]
        public async Task<IActionResult> Create(CasGeneratorCommand command)
        {
            await _mediator.Publish(command);
            return Ok();
        }


    }
}
