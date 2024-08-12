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
    public class DetalleVentumController : ControllerBase
    {
        private readonly SalsaContext _context;

        public DetalleVentumController(SalsaContext context)
        {
            _context = context;
        }

        // GET: api/detalleventum
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DetalleVentum>>> GetDetallesVenta()
        {
            return await _context.DetalleVenta
                .Include(dv => dv.IdProductoNavigation)
                .Include(dv => dv.IdVentaNavigation)
                .ToListAsync();
        }

        // GET: api/detalleventum/5
        [HttpGet("{id}")]
        public async Task<ActionResult<DetalleVentum>> GetDetalleVentum(int id)
        {
            var detalleVentum = await _context.DetalleVenta
                .Include(dv => dv.IdProductoNavigation)
                .Include(dv => dv.IdVentaNavigation)
                .FirstOrDefaultAsync(dv => dv.IdDetalleVenta == id);

            if (detalleVentum == null)
            {
                return NotFound();
            }

            return detalleVentum;
        }

        // PUT: api/detalleventum/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDetalleVentum(int id, DetalleVentum detalleVentum)
        {
            if (id != detalleVentum.IdDetalleVenta)
            {
                return BadRequest();
            }

            _context.Entry(detalleVentum).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DetalleVentumExists(id))
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

        // POST: api/detalleventum
        [HttpPost]
        public async Task<ActionResult<DetalleVentum>> PostDetalleVentum(DetalleVentum detalleVentum)
        {
            _context.DetalleVenta.Add(detalleVentum);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (DetalleVentumExists(detalleVentum.IdDetalleVenta))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction(nameof(GetDetalleVentum), new { id = detalleVentum.IdDetalleVenta }, detalleVentum);
        }

        // DELETE: api/detalleventum/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDetalleVentum(int id)
        {
            var detalleVentum = await _context.DetalleVenta.FindAsync(id);
            if (detalleVentum == null)
            {
                return NotFound();
            }

            _context.DetalleVenta.Remove(detalleVentum);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool DetalleVentumExists(int id)
        {
            return _context.DetalleVenta.Any(e => e.IdDetalleVenta == id);
        }
    }
}
