using API.Seeker.Data;
using API.Seeker.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Seeker.Services.Switch.Commands;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;



namespace Api.Seeker.Controllers
{
    [ApiController]
    [Route("/")]
    public class CasSekeer : ControllerBase
    {

        private readonly APIContext _context;
        private readonly IMediator _mediator;
        public CasSekeer(
            IMediator mediator,
            APIContext context)
        {
            _context = context;
            _mediator = mediator;
        }

        [HttpGet]
        public string Get()
        {
            return "TestCase Controlador de Busqueda Especializada";
        }
        
        [HttpPost]
        public async Task<ActionResult<IEnumerable<CaRepository>>> GetCaRepositorybaseoncondicion(CasSeekerSwitchCommand command)
        {
            String Alphabet = strConstruct(command.Alphabet);
            var par1 = new SqlParameter("@par1", Alphabet);
            par1.SqlDbType = SqlDbType.VarChar;
            par1.Size = 300;
            var par2 = new SqlParameter("@par2", command.Columns);
            var par3 = new SqlParameter("@par3", command.Strength);

            string sql = "listarCAs @par1, @par2, @par3";

            return  _context.CaRepository.FromSqlRaw(sql, par1, par2, par3).ToList();
        }

        public string strConstruct(String Alphabet)
        {

            String[] Variables = Alphabet.Split(',');
            int[] num_Variables = new int[Variables.Length];
            String concatenado = "";

            for (int i = 0; i < Variables.Length; i++)
            {
                num_Variables[i] = Int32.Parse(Variables[i]);
                concatenado = concatenado + "[";
                for (int j = num_Variables[i]; j <= 9; j++)
                {
                    concatenado = concatenado + j;
                }
                if (i < Variables.Length - 1)
                {
                    concatenado = concatenado + "],";
                }
            }
            concatenado = concatenado + "]%";
            return concatenado;

        }

    }
}
