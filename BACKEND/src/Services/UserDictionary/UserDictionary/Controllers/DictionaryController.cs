using DictionaryService;
using DictionaryService.Commands;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Orchestrator.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DictionaryController : ControllerBase
    {
        //private readonly IDictionaryLogic _DictionaryLogic;

        //public DictionaryController(
        //    IDictionaryLogic dictionaryLogic
        //)
        //{
        //    _DictionaryLogic = dictionaryLogic;
        //}
        //[HttpPost]
        //public async Task<ActionResult<List<DictionaryModel>>> Create(DictionaryModelcommands command)
        //{
        //    return _DictionaryLogic.Logic(command);
        //}
    }
}
