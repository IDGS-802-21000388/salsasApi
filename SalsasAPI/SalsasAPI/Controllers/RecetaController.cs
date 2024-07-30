using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SalsasAPI.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace SalsasAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecetaController : Controller
    {
        private readonly SalsaContext _context;

        public RecetaController(SalsaContext context)
        {
            _context = context;
        }

        [HttpGet("getProducto")]
        public async Task<ActionResult<IEnumerable<vw_Producto_Detalle>>> GetProducto()
        {
            var productoDetalles = await _context.vw_Producto_Detalle.ToListAsync();

            if (productoDetalles == null || !productoDetalles.Any())
            {
                return NotFound();
            }

            return Ok(productoDetalles);
        }

        [HttpGet("getDetalleReceta/{idProducto}")]
        public async Task<ActionResult<IEnumerable<vw_Detalle_Receta>>> GetDetalleReceta(int idProducto)
        {
            var detalleReceta = await _context.vw_Detalle_Receta
                                              .Where(dr => dr.IdProducto == idProducto)
                                              .ToListAsync();

            if (detalleReceta == null || !detalleReceta.Any())
            {
                return NotFound();
            }

            return Ok(detalleReceta);
        }

        [HttpGet("getMedida")]
        public async Task<ActionResult<IEnumerable<Medidum>>> GetMedidas()
        {
            return await _context.Medida.ToListAsync();
        }

        [HttpGet("getMateriaPrimaDetalle")]
        public async Task<ActionResult<IEnumerable<vw_MateriaPrima_Detalle>>> GetMateriaPrimaDetalle()
        {
            var materiaPrimaDetalle = await _context.vw_MateriaPrima_Detalle.ToListAsync();

            if (materiaPrimaDetalle == null || !materiaPrimaDetalle.Any())
            {
                return NotFound();
            }

            return Ok(materiaPrimaDetalle);
        }

        [HttpPost("insertProductoConIngredientes")]
        public async Task<IActionResult> InsertProductoConIngredientes([FromBody] ProductoRequest request)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                // Insertar el producto
                var producto = new Producto
                {
                    NombreProducto = request.Producto.NombreProducto,
                    PrecioVenta = request.Producto.PrecioVenta,
                    PrecioProduccion = request.Producto.PrecioProduccion,
                    Cantidad = request.Producto.Cantidad,
                    IdMedida = request.Producto.Medida,
                    Fotografia = request.Producto.Fotografia,
                    Estatus = true
                };

                _context.Productos.Add(producto);
                await _context.SaveChangesAsync();

                // Obtener el idProducto generado
                int idProducto = producto.IdProducto;

                // Insertar la receta
                var receta = new Recetum
                {
                    IdProducto = idProducto,
                    IdMedida = request.Producto.Medida
                };

                _context.Receta.Add(receta);
                await _context.SaveChangesAsync();

                // Obtener el idReceta generado
                int idReceta = receta.IdReceta;

                // Insertar los ingredientes
                foreach (var ingrediente in request.Ingredientes)
                {
                    var detalleReceta = new DetalleRecetum
                    {
                        IdReceta = idReceta,
                        CantidadMateriaPrima = ingrediente.CantidadMateriaPrima,
                        MedidaIngrediente = ingrediente.MedidaIngrediente,
                        IdMateriaPrima = ingrediente.IdMateriaPrima
                    };

                    _context.DetalleReceta.Add(detalleReceta);
                }

                await _context.SaveChangesAsync();
                await transaction.CommitAsync();

                return Ok(new { Message = "Producto y ingredientes insertados correctamente" });
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                return StatusCode(500, new { Message = "Error al insertar producto e ingredientes", Details = ex.Message });
            }
        }

        public class ProductoRequest
        {
            public ProductoDto Producto { get; set; } = null!;
            public List<IngredienteDto> Ingredientes { get; set; } = new List<IngredienteDto>();
        }

        public class ProductoDto
        {
            public string NombreProducto { get; set; } = null!;
            public double PrecioVenta { get; set; }
            public double PrecioProduccion { get; set; }
            public int Cantidad { get; set; }
            public int Medida { get; set; }
            public string Fotografia { get; set; } = null!;
        }

        public class IngredienteDto
        {
            public int CantidadMateriaPrima { get; set; }
            public int MedidaIngrediente { get; set; }
            public int IdMateriaPrima { get; set; }
        }

    }
}
