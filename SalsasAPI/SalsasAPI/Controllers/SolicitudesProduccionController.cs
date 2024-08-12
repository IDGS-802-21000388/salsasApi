using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SalsasAPI.Models;

namespace SalsasAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SolicitudesProduccionController : ControllerBase
    {
        private readonly SalsaContext _context;
        public SolicitudesProduccionController(SalsaContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<SolicitudProduccion>>> GetSolicitudesProduccion()
        {
            return await _context.SolicitudProduccions.ToListAsync();
        }
        [HttpGet("{id}")]
        public async Task<ActionResult> GetSolicitudProduccion(int id)
        {
            var solicitudProduccion = await _context.SolicitudProduccions
                .Where(sp => sp.IdSolicitud == id)
                .Select(sp => new
                {
                    sp.IdSolicitud,
                    sp.FechaSolicitud,
                    sp.Estatus,
                    sp.IdVenta,
                    sp.IdUsuario,
                    NombreUsuario = sp.IdUsuarioNavigation.Nombre,
                    FechaVenta = sp.IdVentaNavigation.FechaVenta,  // Información de la venta
                    TotalVenta = sp.IdVentaNavigation.Total,  // Información de la venta
                    DetalleSolicituds = sp.DetalleSolicituds.Select(ds => new
                    {
                        ds.IdDetalleSolicitud,
                        ds.FechaInicio,
                        ds.FechaFin,
                        ds.IdUsuario,
                        NombreUsuario = ds.IdUsuarioNavigation.Nombre,
                        ds.Estatus,
                        ds.NumeroPaso
                    }).ToList()
                })
                .FirstOrDefaultAsync();

            if (solicitudProduccion == null)
            {
                return NotFound();
            }

            return Ok(solicitudProduccion);
        }


        // POST: api/solicitudproduccion
        [HttpPost]
        public async Task<ActionResult<SolicitudProduccion>> PostSolicitudProduccion(SolicitudProduccion solicitudProduccion)
        {
            _context.SolicitudProduccions.Add(solicitudProduccion);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetSolicitudProduccion), new { id = solicitudProduccion.IdSolicitud }, solicitudProduccion);
        }


        [HttpPut("{id}/estatus")]
        public async Task<IActionResult> UpdateSolicitudProduccionEstatus(int id, [FromBody] int estatus)
        {
            var solicitudProduccion = await _context.SolicitudProduccions.FindAsync(id);
            if (solicitudProduccion == null)
            {
                return NotFound();
            }

            solicitudProduccion.Estatus = estatus;
            _context.Entry(solicitudProduccion).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpPut("venta/{idVenta}/estatus")]
        public async Task<IActionResult> UpdateEnvioEstatus(int idVenta, [FromBody] string nuevoEstatus)
        {
            var envio = await _context.Envios
                .FirstOrDefaultAsync(e => e.IdVenta == idVenta);

            if (envio == null)
            {
                return NotFound();
            }

            envio.Estatus = nuevoEstatus;
            _context.Entry(envio).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
