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
    public class InventarioReporteController : ControllerBase
    {
        private readonly SalsaContext _context;

        public InventarioReporteController(SalsaContext context)
        {
            _context = context;
        }

        // GET: api/inventarioreporte
        [HttpGet]
        public async Task<ActionResult<IEnumerable<InventarioReporte>>> GetInventarioReportes()
        {
            return await _context.InventarioReporte.ToListAsync();
        }

        // GET: api/inventarioreporte/5
        [HttpGet("{id}")]
        public async Task<ActionResult<InventarioReporte>> GetInventarioReporte(int id)
        {
            var inventarioReporte = await _context.InventarioReporte.FindAsync(id);

            if (inventarioReporte == null)
            {
                return NotFound();
            }

            return inventarioReporte;
        }

        // PUT: api/inventarioreporte/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutInventarioReporte(int id, InventarioReporte inventarioReporte)
        {
            if (id != inventarioReporte.Id)
            {
                return BadRequest();
            }

            _context.Entry(inventarioReporte).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!InventarioReporteExists(id))
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

        // POST: api/inventarioreporte
        [HttpPost]
        public async Task<ActionResult<InventarioReporte>> PostInventarioReporte(InventarioReporte inventarioReporte)
        {
            _context.InventarioReporte.Add(inventarioReporte);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetInventarioReporte), new { id = inventarioReporte.Id }, inventarioReporte);
        }

        // DELETE: api/inventarioreporte/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteInventarioReporte(int id)
        {
            var inventarioReporte = await _context.InventarioReporte.FindAsync(id);
            if (inventarioReporte == null)
            {
                return NotFound();
            }

            _context.InventarioReporte.Remove(inventarioReporte);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool InventarioReporteExists(int id)
        {
            return _context.InventarioReporte.Any(e => e.Id == id);
        }
    }
}
