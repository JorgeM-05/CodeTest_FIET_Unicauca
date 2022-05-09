using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Generator;
using Generator.Api.Models;

namespace Generator.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CasBusController : ControllerBase
    {
        private readonly BusApiContext _context;

        public CasBusController(BusApiContext context)
        {
            _context = context;
        }

        // GET: api/CasBus
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CasBus>>> GetCasBus()
        {
            return await _context.CasBus.ToListAsync();
        }

        // GET: api/CasBus/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CasBus>> GetCasBus(int id)
        {
            var casBus = await _context.CasBus.FindAsync(id);

            if (casBus == null)
            {
                return NotFound();
            }

            return casBus;
        }

        // PUT: api/CasBus/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCasBus(int id, CasBus casBus)
        {
            if (id != casBus.CAID)
            {
                return BadRequest();
            }

            _context.Entry(casBus).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CasBusExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/CasBus
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<CasBus>> PostCasBus(CasBus casBus)
        {
            _context.CasBus.Add(casBus);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCasBus", new { id = casBus.CAID }, casBus);
        }

        // DELETE: api/CasBus/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCasBus(int id)
        {
            var casBus = await _context.CasBus.FindAsync(id);
            if (casBus == null)
            {
                return NotFound();
            }

            _context.CasBus.Remove(casBus);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CasBusExists(int id)
        {
            return _context.CasBus.Any(e => e.CAID == id);
        }

        //[HttpPost("/rabbitMQ")]
        //public async Task<ActionResult<CasBus>> PostCasBus2(CasBus casBus)
        //{
        //    _context.CasBus.Add(casBus);
        //    await _context.SaveChangesAsync();

        //    return CreatedAtAction("GetCasBus", new { id = casBus.CAID }, casBus);
        //}


    }

 
  

}
