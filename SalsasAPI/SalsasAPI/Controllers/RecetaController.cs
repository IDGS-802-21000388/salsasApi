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
            // Filtrar productos que estén en estatus 1
            var productoDetalles = await _context.vw_Producto_Detalle
                                                 .Where(p => p.Estatus == true)
                                                 .ToListAsync();

            if (productoDetalles == null || !productoDetalles.Any())
            {
                return NotFound("No products found with status 1.");
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
        [HttpPost("updateProductoAndReceta")]
        public async Task<IActionResult> UpdateProductoAndReceta([FromBody] UpdateProductoRecetaRequest request)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                // Actualizar el producto
                var producto = await _context.Productos.FindAsync(request.IdProducto);
                if (producto == null)
                {
                    return NotFound("Producto no encontrado.");
                }

                producto.NombreProducto = request.Producto.NombreProducto;
                producto.PrecioVenta = request.Producto.PrecioVenta;
                producto.PrecioProduccion = request.Producto.PrecioProduccion;
                producto.Cantidad = request.Producto.Cantidad;
                producto.IdMedida = request.Producto.Medida;
                producto.Fotografia = request.Producto.Fotografia;
                _context.Productos.Update(producto);
                await _context.SaveChangesAsync();

                // Actualizar la receta existente para setear IdProducto a NULL
                var recetas = _context.Receta.Where(r => r.IdProducto == request.IdProducto);
                foreach (var receta in recetas)
                {
                    receta.IdProducto = null;
                }
                await _context.SaveChangesAsync();

                // Insertar una nueva receta
                var nuevaReceta = new Recetum
                {
                    IdProducto = request.IdProducto,
                    IdMedida = request.Producto.Medida
                };
                _context.Receta.Add(nuevaReceta);
                await _context.SaveChangesAsync();

                // Obtener el idReceta generado
                int idReceta = nuevaReceta.IdReceta;

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

                return Ok(new { Message = "Producto actualizado y nueva receta con ingredientes insertados correctamente" });
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                return StatusCode(500, new { Message = "Error al actualizar el producto y al insertar nueva receta", Details = ex.Message });
            }
        }

        [HttpPut("updateProductoEstatus/{idProducto}")]
        public async Task<IActionResult> UpdateProductoEstatus(int idProducto)
        {
            var producto = await _context.Productos.FirstOrDefaultAsync(p => p.IdProducto == idProducto);
            if (producto == null)
            {
                return NotFound("Producto no encontrado.");
            }

            // Actualizar el estatus a false (0 en la base de datos)
            producto.Estatus = false;
            _context.Entry(producto).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.vw_Producto_Detalle.Any(p => p.IdProducto == idProducto))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok(new { Message = "Producto eliminado correctamente" });
        }

        [HttpPost("{idProducto}/agregarStock")]
        public async Task<IActionResult> AgregarStock(int idProducto, [FromBody] int cantidadAgregar)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                // Validar que el producto exista
                var producto = await _context.Productos
                    .Include(p => p.DetalleProductos)
                    .FirstOrDefaultAsync(p => p.IdProducto == idProducto);

                if (producto == null)
                {
                    return NotFound(new { text = "Producto no encontrado." });
                }

                // Obtener la receta del producto
                var receta = await _context.Receta
                    .Where(r => r.IdProducto == idProducto)
                    .FirstOrDefaultAsync();

                if (receta == null)
                {
                    return NotFound(new { text = "Receta no encontrada para el producto." });
                }

                // Verificar que hay suficiente materia prima para la cantidad a agregar
                var detallesReceta = await _context.DetalleReceta
                    .Where(dr => dr.IdReceta == receta.IdReceta)
                    .ToListAsync();

                foreach (var detalleReceta in detallesReceta)
                {
                    // Obtener la materia prima necesaria
                    var materiaPrima = await _context.DetalleMateriaPrimas
                        .Where(dmp => dmp.idMateriaPrima == detalleReceta.IdMateriaPrima && dmp.fechaVencimiento >= DateTime.Now)
                        .OrderBy(dmp => dmp.fechaVencimiento)
                        .FirstOrDefaultAsync();

                    if (materiaPrima == null)
                    {
                        await transaction.RollbackAsync();
                        return BadRequest(new { text = $"No hay suficiente materia prima disponible para {detalleReceta.IdMateriaPrima}." });
                    }

                    // Calcular la cantidad total de materia prima requerida
                    var cantidadTotalRequerida = cantidadAgregar * detalleReceta.CantidadMateriaPrima;
                    
                    if (cantidadTotalRequerida > materiaPrima.cantidadExistentes)
                    {
                        await transaction.RollbackAsync();
                        return BadRequest(new { text = $"No hay suficiente materia prima disponible para producir la cantidad solicitada." });
                    }

                    // Descontar la materia prima
                    materiaPrima.cantidadExistentes -= cantidadTotalRequerida;
                    _context.DetalleMateriaPrimas.Update(materiaPrima);
                }

                // Agregar la cantidad de producto al stock existente
                var detalleProducto = producto.DetalleProductos.FirstOrDefault();
                if (detalleProducto == null)
                {
                    detalleProducto = new DetalleProducto
                    {
                        IdProducto = idProducto,
                        CantidadExistentes = 0
                    };
                    _context.DetalleProductos.Add(detalleProducto);
                }

                detalleProducto.CantidadExistentes += cantidadAgregar;
                _context.DetalleProductos.Update(detalleProducto);

                // Guardar los cambios
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();

                return Ok(new { text = "Stock agregado y materia prima descontada correctamente." });
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                return StatusCode(500, new { text = $"Error interno del servidor: {ex.Message}" });
            }
        }

    

        public class UpdateProductoRecetaRequest
        {
            public int IdProducto { get; set; }
            public ProductoDto Producto { get; set; } = null!;
            public List<IngredienteDto> Ingredientes { get; set; } = new List<IngredienteDto>();
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
            public double Cantidad { get; set; }
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
