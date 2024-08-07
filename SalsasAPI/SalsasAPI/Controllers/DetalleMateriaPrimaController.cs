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
    public class DetalleMateriaPrimaController : ControllerBase
    {
        private readonly SalsaContext _context;

        public DetalleMateriaPrimaController(SalsaContext context)
        {
            _context = context;
        }

        // GET: api/detallemateriaprima
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DetalleMateriaPrima>>> GetDetalleMateriasPrimas()
        {
            return await _context.DetalleMateriaPrimas.ToListAsync();
        }

        // GET: api/detallemateriaprima/5
        [HttpGet("{id}")]
        public async Task<ActionResult<DetalleMateriaPrima>> GetDetalleMateriaPrima(int id)
        {
            var detalleMateriaPrima = await _context.DetalleMateriaPrimas.FindAsync(id);

            if (detalleMateriaPrima == null)
            {
                return NotFound();
            }

            return detalleMateriaPrima;
        }

        // GET: api/detallemateriaprima/byMateriaPrima/5
        [HttpGet("byMateriaPrima/{idMateriaPrima}")]
        public async Task<ActionResult<DetalleMateriaPrima>> GetDetalleMateriaPrimaByMateriaPrimaId(int idMateriaPrima)
        {
            var detalleMateriaPrima = await _context.DetalleMateriaPrimas
                                                    .FirstOrDefaultAsync(d => d.idMateriaPrima == idMateriaPrima);

            if (detalleMateriaPrima == null)
            {
                return NotFound();
            }

            return detalleMateriaPrima;
        }

        // PUT: api/detallemateriaprima/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDetalleMateriaPrima(int id, DetalleMateriaPrima detalleMateriaPrima)
        {
            if (id != detalleMateriaPrima.idDetalleMateriaPrima)
            {
                return BadRequest();
            }

            _context.Entry(detalleMateriaPrima).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DetalleMateriaPrimaExists(id))
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

        [HttpPut("{id}/detalle")]
        public async Task<IActionResult> UpdateDetalleSolicitudMateria(int id, [FromBody] int detalle)
        {
            var detalleSolicitud = await _context.DetalleMateriaPrimas.FindAsync(id);
            if (detalleSolicitud == null)
            {
                return NotFound();
            }

            detalleSolicitud.cantidadExistentes = detalle;
            _context.Entry(detalleSolicitud).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // POST: api/detallemateriaprima
        [HttpPost]
        public async Task<ActionResult<DetalleMateriaPrima>> PostDetalleMateriaPrima(DetalleMateriaPrima detalleMateriaPrima)
        {
            _context.DetalleMateriaPrimas.Add(detalleMateriaPrima);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetDetalleMateriaPrima), new { id = detalleMateriaPrima.idDetalleMateriaPrima }, detalleMateriaPrima);
        }

        // DELETE: api/detallemateriaprima/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDetalleMateriaPrima(int id)
        {
            var detalleMateriaPrima = await _context.DetalleMateriaPrimas.FindAsync(id);
            if (detalleMateriaPrima == null)
            {
                return NotFound();
            }

            detalleMateriaPrima.estatus = 0;
            _context.Entry(detalleMateriaPrima).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // PUT: api/detallemateriaprima/activate/5
        [HttpPut("activate/{id}")]
        public async Task<IActionResult> ActivateDetalleMateriaPrima(int id)
        {
            var detalleMateriaPrima = await _context.DetalleMateriaPrimas.FindAsync(id);
            if (detalleMateriaPrima == null)
            {
                return NotFound();
            }

            detalleMateriaPrima.estatus = 1;
            _context.Entry(detalleMateriaPrima).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool DetalleMateriaPrimaExists(int id)
        {
            return _context.DetalleMateriaPrimas.Any(e => e.idDetalleMateriaPrima == id);
        }
    }
}
