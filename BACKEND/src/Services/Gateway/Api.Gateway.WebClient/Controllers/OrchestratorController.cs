using Api.Gateway.Models.seeker.Commands;
using Api.Gateway.Models.seeker.DTOs;
using Api.Gateway.Proxies;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Gateway.WebClient.Controllers
{
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ApiController]
    [Route("Orchestrator")]
    public class OrchestratorController : ControllerBase
    {
        private readonly IOrchestratorProxy _OrchestratorProxy;

        public OrchestratorController(
            IOrchestratorProxy orchestratorProxy
        )
        {
            _OrchestratorProxy = orchestratorProxy;
        }

        [HttpPost("Seeker/")]
        public async Task<List<CasCreateCommand>> GetAll(CasSeekerCommandProxies command)
        {
            return await _OrchestratorProxy.GetOnlySeeker(command);
        }

        [HttpGet("{id}")]
        public async Task<CaRepositoryDto> Get(int id)
        {
            return await _OrchestratorProxy.GetAsync(id);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CasCreateCommand command)
        {
            await _OrchestratorProxy.CreateAsync(command);
            return Ok();
        }

        [HttpPost("Logic")]
        public async Task<ActionResult<CasCreateCommand>> Logic(CasSeekerCommandProxies command)
        {

            return await _OrchestratorProxy.CAsLogic(command);
        }

        [HttpPost("Api_Logic/Gateway")]
        public async Task<ActionResult<CasCreateCommand>> Logics(DataRequest command)
        {
            string alph = "";
            var fuerza = command.DataProject.strength;
            var Columnas = command.Variables.Count;
            int[] alfabeto = new int[Columnas];
            var list = command.Variables.ToArray();
            string alfabeto_user = "";
            for (var i = 0; i < list.Length; i++)
            {
                var val = list[i].valores;
                alfabeto_user = alfabeto_user + val;
                if (i < list.Length - 1) alfabeto_user = alfabeto_user + "/";
                var valuesdiv2 = val.Split(',');
                alfabeto[i] = valuesdiv2.Length;
                alph = alph+ valuesdiv2.Length;
                if (i < list.Length-1) alph = alph + ","; 
            }

            CasSeekerCommandProxies cas = new CasSeekerCommandProxies();


            cas.Alphabet = alph;
            cas.Strength = Int32.Parse(fuerza);
            cas.Columns = Columnas;
            cas.Alphabet_user = alfabeto_user;

            //await _OrchestratorProxy.CAsLogic(cas);

            return await _OrchestratorProxy.CAsLogic(cas);
            //return null;
        }

       
    }
}
