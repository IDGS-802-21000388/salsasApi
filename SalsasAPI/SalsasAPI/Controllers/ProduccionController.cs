using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SalsasAPI.Models;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;

namespace SalsasAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProduccionController : ControllerBase
    {
        private readonly SalsaContext _context;
        private readonly ILogger<ProduccionController> _logger;

        public ProduccionController(SalsaContext context, ILogger<ProduccionController> logger)
        {
            _context = context;
            _logger = logger;
        }

        // Método para obtener pedidos filtrados por idUsuario
        [HttpGet("Pedidos/{idUsuario}")]
        public async Task<IActionResult> GetPedidos(int idUsuario)
        {
            var pedidos = await _context.SolicitudProduccions
                .Where(sp => sp.DetalleSolicituds.Any(ds => ds.IdUsuario == idUsuario) && (sp.Estatus == 1 || sp.Estatus == 2))
                .Include(sp => sp.DetalleSolicituds)
                    .ThenInclude(ds => ds.IdUsuarioNavigation)
                .Include(sp => sp.IdVentaNavigation)
                    .ThenInclude(v => v.DetalleVenta)
                    .ThenInclude(dv => dv.IdProductoNavigation)
                .Include(sp => sp.IdUsuarioNavigation)
                .ToListAsync();

            if (pedidos == null || !pedidos.Any())
            {
                return NotFound($"No se encontraron pedidos para el usuario con ID {idUsuario}.");
            }

            var result = pedidos.Select(pedido => new
            {
                idSolicitud = pedido.IdSolicitud,
                fechaSolicitud = pedido.FechaSolicitud,
                estatus = pedido.Estatus,
                venta = new
                {
                    idVenta = pedido.IdVentaNavigation?.IdVenta,
                    fechaVenta = pedido.IdVentaNavigation?.FechaVenta,
                    total = pedido.IdVentaNavigation?.Total,
                    detalleVenta = pedido.IdVentaNavigation?.DetalleVenta.Select(detalle => new
                    {
                        idDetalleVenta = detalle.IdDetalleVenta,
                        cantidad = detalle.Cantidad,
                        subtotal = detalle.Subtotal,
                        producto = new
                        {
                            idProducto = detalle.IdProductoNavigation?.IdProducto,
                            nombreProducto = detalle.IdProductoNavigation?.NombreProducto
                        }
                    })
                },
                usuarioCliente = pedido.IdUsuarioNavigation?.Nombre,
                detallesProduccion = pedido.DetalleSolicituds.Select(detalle => new
                {
                    idDetalleSolicitud = detalle.IdDetalleSolicitud,
                    fechaInicio = detalle.FechaInicio,
                    fechaFin = detalle.FechaFin,
                    estatus = detalle.Estatus,
                    numeroPaso = detalle.NumeroPaso,
                    usuarioProduccion = detalle.IdUsuarioNavigation?.Nombre
                })
            });

            return Ok(result);
        }

        // Nuevo método para obtener todos los pedidos sin filtrar por usuario
        [HttpGet("TodosLosPedidos")]
        public async Task<IActionResult> GetAllPedidos()
        {
            var pedidos = await _context.SolicitudProduccions
                .Where(sp => sp.Estatus == 1 || sp.Estatus == 2) // Opcional: filtrar por estatus si es necesario
                .Include(sp => sp.DetalleSolicituds)
                    .ThenInclude(ds => ds.IdUsuarioNavigation)
                .Include(sp => sp.IdVentaNavigation)
                    .ThenInclude(v => v.DetalleVenta)
                    .ThenInclude(dv => dv.IdProductoNavigation)
                .Include(sp => sp.IdUsuarioNavigation)
                .ToListAsync();

            if (pedidos == null || !pedidos.Any())
            {
                return NotFound("No se encontraron pedidos.");
            }

            var result = pedidos.Select(pedido => new
            {
                idSolicitud = pedido.IdSolicitud,
                fechaSolicitud = pedido.FechaSolicitud,
                estatus = pedido.Estatus,
                venta = new
                {
                    idVenta = pedido.IdVentaNavigation?.IdVenta,
                    fechaVenta = pedido.IdVentaNavigation?.FechaVenta,
                    total = pedido.IdVentaNavigation?.Total,
                    detalleVenta = pedido.IdVentaNavigation?.DetalleVenta.Select(detalle => new
                    {
                        idDetalleVenta = detalle.IdDetalleVenta,
                        cantidad = detalle.Cantidad,
                        subtotal = detalle.Subtotal,
                        producto = new
                        {
                            idProducto = detalle.IdProductoNavigation?.IdProducto,
                            nombreProducto = detalle.IdProductoNavigation?.NombreProducto
                        }
                    })
                },
                usuarioCliente = pedido.IdUsuarioNavigation?.Nombre,
                detallesProduccion = pedido.DetalleSolicituds.Select(detalle => new
                {
                    idDetalleSolicitud = detalle.IdDetalleSolicitud,
                    fechaInicio = detalle.FechaInicio,
                    fechaFin = detalle.FechaFin,
                    estatus = detalle.Estatus,
                    numeroPaso = detalle.NumeroPaso,
                    usuarioProduccion = detalle.IdUsuarioNavigation?.Nombre
                })
            });

            return Ok(result);
        }
    }
}
