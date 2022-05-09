using API.Seeker.Data;
using API.Seeker.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Seeker.Services.Quieries;
using Service.Common.Collection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Seeker.Services.EventHandlers.Commands;


namespace Api.Seeker.Controllers
{
    [ApiController]
    [Route("Cas")]
    public class CasController : ControllerBase
    {
        private readonly ICaRepositoryQuerySerivices _caRepositoryQueryServices;
        //private readonly ILogger<APIContext> _logger;
        private readonly APIContext _logger;
        private readonly IMediator _mediator;

        public CasController(
            APIContext logger,
            ICaRepositoryQuerySerivices CaRepositoryQuerySerivices,
            IMediator mediator
            )
        {
            _logger = logger;
            _caRepositoryQueryServices = CaRepositoryQuerySerivices; // min15:34 parte 23
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<DataCollection<CaRepositoryDto>> GetAll(int page = 1, int take = 10, string ids = null)
        {
            IEnumerable<int> Cas = null;
            if (!String.IsNullOrEmpty(ids))
            {
                Cas = ids.Split(',').Select(x => Convert.ToInt32(x));
            }
            //return await _caRepositoryQueryServices.GetAllAsync(page, take, Cas);
            return null;
        }

        [HttpGet("{id}")]
        public async Task<CaRepositoryDto> Get(int id)
        {

            return await _caRepositoryQueryServices.GetAsync(id);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CasCreateCommand command)
        {
            await _mediator.Publish(command);
            return Ok();
        }
    }

}














//private readonly APIContext _context;

//public CasController(APIContext context)
//{
//    _context = context;
//}


//[HttpGet]
//public string Get()
//{
//    return "TestCase Universidad del cauca";
//}

//[HttpGet]
//public async Task<ActionResult<IEnumerable<CaRepository>>> GetCaRepositorybaseoncondicion(string condicion,
//    int totalcolum, int stregth)
//{
//    condicion = "[56789],[456789],[3456789],[23456789]%";
//    totalcolum = 4;
//    stregth = 2;
//    var par1 = new SqlParameter("@par1", condicion);
//    var par2 = new SqlParameter("@par2", totalcolum);
//    var par3 = new SqlParameter("@par3", stregth);

//    string sql = "listarCAs @par1, @par2, @par3";

//    return _context.CaRepository.FromSqlRaw(sql, par1, par2, par3).ToList();
//}// funcional 
//    }
//}
