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
    public class MateriaPrimaController : ControllerBase
    {
        private readonly SalsaContext _context;

        public MateriaPrimaController(SalsaContext context)
        {
            _context = context;
        }

        // GET: api/materiaprima
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MateriaPrima>>> GetMateriasPrimas()
        {
            return await _context.MateriaPrimas.ToListAsync();
        }

        // GET: api/materiaprima/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MateriaPrima>> GetMateriaPrima(int id)
        {
            var materiaPrima = await _context.MateriaPrimas.FindAsync(id);

            if (materiaPrima == null)
            {
                return NotFound();
            }

            return materiaPrima;
        }

        // PUT: api/materiaprima/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMateriaPrima(int id, MateriaPrima materiaPrima)
        {
            if (id != materiaPrima.IdMateriaPrima)
            {
                return BadRequest();
            }

            _context.Entry(materiaPrima).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MateriaPrimaExists(id))
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

        // POST: api/materiaprima
        [HttpPost]
        public async Task<ActionResult<MateriaPrima>> PostMateriaPrima(MateriaPrima materiaPrima)
        {
            _context.MateriaPrimas.Add(materiaPrima);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetMateriaPrima), new { id = materiaPrima.IdMateriaPrima }, materiaPrima);
        }

        [HttpPut("{id}/cantidad")]
        public async Task<IActionResult> UpdateDetalleSolicitudMateria(int id, [FromBody] int cantidad)
        {
            var detalleSolicitud = await _context.MateriaPrimas.FindAsync(id);
            if (detalleSolicitud == null)
            {
                return NotFound();
            }

            detalleSolicitud.Cantidad = cantidad;
            _context.Entry(detalleSolicitud).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }


        [HttpPost("{idEnvio}/descontarMateriaPrima")]
        public async Task<IActionResult> DescontarMateriaPrima(int idEnvio)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                // Obtener idVenta desde Envio
                var envio = await _context.Envios
                    .Where(e => e.IdEnvio == idEnvio)
                    .Select(e => e.IdVenta)
                    .FirstOrDefaultAsync();

                if (envio == 0)
                {
                    return NotFound(new { text = "Envío no encontrado." });
                }

                var detallesVenta = await _context.DetalleVenta
                    .Where(dv => dv.IdVenta == envio)
                    .ToListAsync();

                foreach (var detalle in detallesVenta)
                {
                    // Descontar el producto
                    var detalleProducto = await _context.DetalleProductos
                        .Where(dp => dp.IdProducto == detalle.IdProducto && dp.FechaVencimiento >= DateTime.Now)
                        .OrderBy(dp => dp.FechaVencimiento)
                        .FirstOrDefaultAsync();

                    if (detalleProducto == null)
                    {
                        await transaction.RollbackAsync();
                        return BadRequest(new { text = $"No hay productos disponibles para el ID {detalle.IdProducto}." });
                    }

                    // Verificar y descontar la cantidad del producto
                    if (detalleProducto.CantidadExistentes < detalle.Cantidad)
                    {
                        await transaction.RollbackAsync();
                        return BadRequest(new { text = $"No hay suficiente stock para el producto {detalle.IdProducto}." });
                    }

                    detalleProducto.CantidadExistentes -= (int)detalle.Cantidad;
                    _context.DetalleProductos.Update(detalleProducto);

                    // Obtener receta del producto
                    var receta = await _context.Receta
                        .Where(r => r.IdProducto == detalle.IdProducto)
                        .Select(r => r.IdReceta)
                        .FirstOrDefaultAsync();

                    if (receta == 0)
                    {
                        await transaction.RollbackAsync();
                        return BadRequest(new { text = $"Receta no encontrada para el producto {detalle.IdProducto}." });
                    }

                    var detallesReceta = await _context.DetalleReceta
                        .Where(dr => dr.IdReceta == receta)
                        .ToListAsync();

                    foreach (var detalleReceta in detallesReceta)
                    {
                        // Obtener materia prima
                        var materiaPrima = await _context.DetalleMateriaPrimas
                            .Where(dmp => dmp.idMateriaPrima == detalleReceta.IdMateriaPrima && dmp.fechaVencimiento >= DateTime.Now)
                            .OrderBy(dmp => dmp.fechaVencimiento)
                            .FirstOrDefaultAsync();

                        if (materiaPrima == null)
                        {
                            await transaction.RollbackAsync();
                            return BadRequest(new { text = "No hay suficiente materia prima disponible." });
                        }

                        var cantidadTotal = detalle.Cantidad * detalleReceta.CantidadMateriaPrima;

                        if (cantidadTotal > materiaPrima.cantidadExistentes)
                        {
                            await transaction.RollbackAsync();
                            return BadRequest(new { text = $"No hay suficiente materia prima disponible para el producto {detalle.IdProducto}." });
                        }

                        materiaPrima.cantidadExistentes -= cantidadTotal;
                        _context.DetalleMateriaPrimas.Update(materiaPrima);
                    }
                }

                await _context.SaveChangesAsync();
                await transaction.CommitAsync();
                return Ok(new { text = "Materia prima y productos descontados correctamente." });
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                return StatusCode(500, new { text = $"Error interno del servidor: {ex.Message}" });
            }
        }




        private bool MateriaPrimaExists(int id)
        {
            return _context.MateriaPrimas.Any(e => e.IdMateriaPrima == id);
        }
    }
}
