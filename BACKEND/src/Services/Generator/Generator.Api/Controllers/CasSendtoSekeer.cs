using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Generator.Servies.SendtoSeeker;
using Generator.Servies.SendtoSeeker.Commands;

namespace Generator.Api.Controllers
{
    [ApiController]
    [Route("/")]
    public class CasSendtoSekeer : ControllerBase
    {
        private readonly IMediator _mediator;
        public CasSendtoSekeer(
            IMediator mediator
            )
        {
            _mediator = mediator;
        }


        [HttpPost]
        public async Task<IActionResult> Create(CasCreateCommand command)
        {
            
            await _mediator.Publish(command);
            return Ok();
        }

        

        //---------------
        [HttpGet]
        public string Get()
        {
            return "Estamos en Generador";
        }
    }
}
