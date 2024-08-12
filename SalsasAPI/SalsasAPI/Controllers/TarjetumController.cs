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
    public class TarjetumController : ControllerBase
    {
        private readonly SalsaContext _context;

        public TarjetumController(SalsaContext context)
        {
            _context = context;
        }

        // GET: api/tarjetum
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Tarjetum>>> GetTarjetas()
        {
            return await _context.Tarjeta
                .Include(t => t.IdPagoNavigation)
                .ToListAsync();
        }

        // GET: api/tarjetum/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Tarjetum>> GetTarjetum(int id)
        {
            var tarjetum = await _context.Tarjeta
                .Include(t => t.IdPagoNavigation)
                .FirstOrDefaultAsync(t => t.IdTarjeta == id);

            if (tarjetum == null)
            {
                return NotFound();
            }

            return tarjetum;
        }

        // PUT: api/tarjetum/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTarjetum(int id, Tarjetum tarjetum)
        {
            if (id != tarjetum.IdTarjeta)
            {
                return BadRequest();
            }

            _context.Entry(tarjetum).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TarjetumExists(id))
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

        // POST: api/tarjetum
        [HttpPost]
        public async Task<ActionResult<Tarjetum>> PostTarjetum(Tarjetum tarjetum)
        {
            _context.Tarjeta.Add(tarjetum);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (TarjetumExists(tarjetum.IdTarjeta))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction(nameof(GetTarjetum), new { id = tarjetum.IdTarjeta }, tarjetum);
        }

        // DELETE: api/tarjetum/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTarjetum(int id)
        {
            var tarjetum = await _context.Tarjeta.FindAsync(id);
            if (tarjetum == null)
            {
                return NotFound();
            }

            _context.Tarjeta.Remove(tarjetum);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TarjetumExists(int id)
        {
            return _context.Tarjeta.Any(e => e.IdTarjeta == id);
        }
    }
}
