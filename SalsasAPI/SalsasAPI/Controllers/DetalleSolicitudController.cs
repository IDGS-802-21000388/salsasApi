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
    }
}
