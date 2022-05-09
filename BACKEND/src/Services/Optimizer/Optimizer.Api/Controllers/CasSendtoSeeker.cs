using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Optimizer.Services.Commands;

namespace Optimizer.Api.Controllers
{
    [ApiController]
    [Route("/")]
    public class CasSendtoSeeker : ControllerBase
    {

        private readonly IMediator _mediator;
        public CasSendtoSeeker(
            IMediator mediator
            )
        {
            _mediator = mediator;
        }


        [HttpPost]
        public async Task<IActionResult> Create(CasOptCommand command)
        {
            await _mediator.Publish(command);
            return Ok();
        }


        [HttpGet]
        public string Get()
        {
            return "RUN ..... Optimizer ";
        }
    }
}
