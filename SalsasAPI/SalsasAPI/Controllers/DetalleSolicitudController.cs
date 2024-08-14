using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SalsasAPI.Models;

namespace SalsasAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DetalleSolicitudController : ControllerBase
    {
        private readonly SalsaContext _context;
        public DetalleSolicitudController(SalsaContext context)
        {
            _context = context;
        }

        [HttpPut("{id}/paso")]
        public async Task<IActionResult> UpdateDetalleSolicitudPaso(int id, [FromBody] int paso)
        {
            var detalleSolicitud = await _context.DetalleSolicituds.FindAsync(id);
            if (detalleSolicitud == null)
            {
                return NotFound();
            }

            detalleSolicitud.NumeroPaso = paso;
            _context.Entry(detalleSolicitud).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<DetalleSolicitud>>> GetDetalleSolicitud()
        {
            return await _context.DetalleSolicituds.ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult<DetalleSolicitud>> PostDetalleSolicitud(DetalleSolicitud detalleSolicitud)
        {
            _context.DetalleSolicituds.Add(detalleSolicitud);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetDetalleSolicitud), new { id = detalleSolicitud.IdDetalleSolicitud }, detalleSolicitud);
        }

        [HttpPut("{id}/usuario")]
        public async Task<IActionResult> UpdateDetalleSolicitudMateria(int id, [FromBody] int usuario)
        {
            var detalleSolicitud = await _context.DetalleSolicituds.FindAsync(id);
            if (detalleSolicitud == null)
            {
                return NotFound();
            }

            detalleSolicitud.IdSolicitud = usuario;
            _context.Entry(detalleSolicitud).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
