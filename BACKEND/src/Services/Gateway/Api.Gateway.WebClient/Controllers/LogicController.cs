using Api.Gateway.Models.seeker.Commands;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Gateway.WebClient.Controllers
{
    [Route("api/")]
    [ApiController]
    public class LogicController : ControllerBase
    {
        //private readonly IOrchestratorProxy _OrchestratorProxy;

        //public OrchestratorController(
        //    IOrchestratorProxy orchestratorProxy
        //)
        //{
        //    _OrchestratorProxy = orchestratorProxy;
        //}
        [HttpGet]
        public string Index()
        {
            return "Runningn....ApiGateway";
        }


        [HttpPost("example/")]
        public async Task<List<CasCreateCommand>> GetAll(DataRequest command)
        {
            var fuerza = command.DataProject.strength;
            var Columnas = command.Variables.Count;
            int[] alfabeto = new int[Columnas];
            var list = command.Variables.ToArray();
            for(var i = 0; i < list.Length; i++)
            {
                var val = list[i].valores;
                var valuesdiv2 = val.Split(',');
                alfabeto[i] = valuesdiv2.Length;
            }

            //CasSeekerCommandProxies cas = new CasSeekerCommandProxies();


            //cas.Alphabet = alfabeto.ToString();
            //cas.Strength = Int32.Parse(fuerza);
            //cas.Columns = Columnas;


            //return await _OrchestratorProxy.GetOnlySeeker(cas);
            return null;
        }
    }
}
