using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SalsasAPI.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalsasAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaqueteriaController : ControllerBase
    {
        private readonly SalsaContext _context;

        public PaqueteriaController(SalsaContext context)
        {
            _context = context;
        }

        // GET: api/paqueteria
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Paqueterium>>> GetPaqueterias()
        {
            return await _context.Paqueteria
                .Include(p => p.Envios)
                .ToListAsync();
        }

        // GET: api/paqueteria/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Paqueterium>> GetPaqueterium(int id)
        {
            var paqueterium = await _context.Paqueteria
                .Include(p => p.Envios)
                .FirstOrDefaultAsync(p => p.IdPaqueteria == id);

            if (paqueterium == null)
            {
                return NotFound();
            }

            return paqueterium;
        }

        // PUT: api/paqueteria/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPaqueterium(int id, Paqueterium paqueterium)
        {
            if (id != paqueterium.IdPaqueteria)
            {
                return BadRequest();
            }

            _context.Entry(paqueterium).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PaqueteriumExists(id))
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

        // POST: api/paqueteria
        [HttpPost]
        public async Task<ActionResult<Paqueterium>> PostPaqueterium(Paqueterium paqueterium)
        {
            _context.Paqueteria.Add(paqueterium);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetPaqueterium), new { id = paqueterium.IdPaqueteria }, paqueterium);
        }

        // DELETE: api/paqueteria/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePaqueterium(int id)
        {
            var paqueterium = await _context.Paqueteria.FindAsync(id);
            if (paqueterium == null)
            {
                return NotFound();
            }

            _context.Paqueteria.Remove(paqueterium);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PaqueteriumExists(int id)
        {
            return _context.Paqueteria.Any(p => p.IdPaqueteria == id);
        }
    }
}
