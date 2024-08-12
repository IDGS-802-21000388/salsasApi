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
    public class VentumController : ControllerBase
    {
        private readonly SalsaContext _context;

        public VentumController(SalsaContext context)
        {
            _context = context;
        }

        // GET: api/ventum
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Ventum>>> GetVenta()
        {
            return await _context.Venta
                .Include(v => v.DetalleVenta)
                .Include(v => v.Envios)
                .Include(v => v.Movimientos)
                .Include(v => v.Pagos)
                .Include(v => v.IdUsuarioNavigation)
                .ToListAsync();
        }

        // GET: api/ventum/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Ventum>> GetVentum(int id)
        {
            var ventum = await _context.Venta
                .Include(v => v.DetalleVenta)
                .Include(v => v.Envios)
                .Include(v => v.Movimientos)
                .Include(v => v.Pagos)
                .Include(v => v.IdUsuarioNavigation)
                .FirstOrDefaultAsync(v => v.IdVenta == id);

            if (ventum == null)
            {
                return NotFound();
            }

            return ventum;
        }

        // PUT: api/ventum/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutVentum(int id, Ventum ventum)
        {
            if (id != ventum.IdVenta)
            {
                return BadRequest();
            }

            _context.Entry(ventum).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VentumExists(id))
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

        // POST: api/ventum
        [HttpPost]
        public async Task<ActionResult<Ventum>> PostVentum(Ventum ventum)
        {
            _context.Venta.Add(ventum);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (VentumExists(ventum.IdVenta))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction(nameof(GetVentum), new { id = ventum.IdVenta }, ventum);
        }

        // DELETE: api/ventum/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVentum(int id)
        {
            var ventum = await _context.Venta.FindAsync(id);
            if (ventum == null)
            {
                return NotFound();
            }

            _context.Venta.Remove(ventum);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool VentumExists(int id)
        {
            return _context.Venta.Any(e => e.IdVenta == id);
        }
    }
}
