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
    public class EncuestaSatisfaccionController : ControllerBase
    {
        private readonly SalsaContext _context;

        public EncuestaSatisfaccionController(SalsaContext context)
        {
            _context = context;
        }

        // GET: api/EncuestaSatisfaccion
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EncuestaSatisfaccion>>> GetEncuestas()
        {
            return await _context.EncuestaSatisfaccion.ToListAsync();
        }

        // GET: api/EncuestaSatisfaccion/5
        [HttpGet("{id}")]
        public async Task<ActionResult<EncuestaSatisfaccion>> GetEncuesta(int id)
        {
            var encuesta = await _context.EncuestaSatisfaccion.FirstOrDefaultAsync(e => e.IdEncuesta == id);

            if (encuesta == null)
            {
                return NotFound(new { message = "Encuesta no encontrada" });
            }

            return encuesta;
        }

        // PUT: api/EncuestaSatisfaccion/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEncuesta(int id, EncuestaSatisfaccion encuesta)
        {
            if (id != encuesta.IdEncuesta)
            {
                return BadRequest(new { message = "El ID de la encuesta no coincide" });
            }

            var existingEncuesta = await _context.EncuestaSatisfaccion.FindAsync(id);
            if (existingEncuesta == null)
            {
                return NotFound(new { message = "Encuesta no encontrada" });
            }

            // Actualizar las propiedades de la encuesta existente
            existingEncuesta.ProcesoCompra = encuesta.ProcesoCompra;
            existingEncuesta.SaborProducto = encuesta.SaborProducto;
            existingEncuesta.EntregaProducto = encuesta.EntregaProducto;
            existingEncuesta.PresentacionProducto = encuesta.PresentacionProducto;
            existingEncuesta.FacilidadUsoPagina = encuesta.FacilidadUsoPagina;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EncuestaExists(id))
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

        // POST: api/EncuestaSatisfaccion
        [HttpPost]
        public async Task<ActionResult<EncuestaSatisfaccion>> PostEncuesta(EncuestaSatisfaccion encuesta)
        {
            // Validación de campos obligatorios
            if (encuesta.IdUsuario <= 0)
            {
                return BadRequest(new { message = "El campo 'Usuario' es requerido y debe ser un ID válido." });
            }

            if (encuesta.IdVenta <= 0)
            {
                return BadRequest(new { message = "El campo 'Venta' es requerido y debe ser un ID válido." });
            }

            _context.EncuestaSatisfaccion.Add(encuesta);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetEncuesta), new { id = encuesta.IdEncuesta }, encuesta);
        }

        // DELETE: api/EncuestaSatisfaccion/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEncuesta(int id)
        {
            var encuesta = await _context.EncuestaSatisfaccion.FindAsync(id);
            if (encuesta == null)
            {
                return NotFound();
            }

            // Eliminar la encuesta
            _context.EncuestaSatisfaccion.Remove(encuesta);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool EncuestaExists(int id)
        {
            return _context.EncuestaSatisfaccion.Any(e => e.IdEncuesta == id);
        }
    }
}
