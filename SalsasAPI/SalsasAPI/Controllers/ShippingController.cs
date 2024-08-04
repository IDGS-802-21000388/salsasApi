using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SalsasAPI.Models;
using System.Linq;
using System.Threading.Tasks;

namespace SalsasAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShippingController : Controller
    {
        private readonly SalsaContext _context;

        public ShippingController(SalsaContext context)
        {
            _context = context;
        }

        [HttpGet("getShippingApi")]
        public async Task<ActionResult<EnvioDetalle>> GetShippingApi()
        {
            var envioDetalle = await _context.EnvioDetalles
                                             .Where(e => e.EstatusEnvio.ToLower() == "en tránsito")
                                             .ToListAsync();

            if (envioDetalle == null)
            {
                return NotFound();
            }

            return Ok(envioDetalle);
        }

        [HttpGet]
        public async Task<IActionResult> GetEnvioDetalles()
        {
            var detalles = await _context.EnvioDetallesWeb.ToListAsync();
            return Ok(detalles);
        }

        [HttpPut("updateStatus/{id}")]
        public async Task<IActionResult> UpdateStatus(int id, [FromBody] UpdateStatusRequest request)
        {
            var envio = await _context.Envios.FindAsync(id);

            if (envio == null)
            {
                return NotFound();
            }

            envio.Estatus = request.Estatus;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        public class UpdateStatusRequest
        {
            public string Estatus { get; set; } = null!;
        }
    }
}
