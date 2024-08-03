using Microsoft.EntityFrameworkCore;
using SalsasAPI.Models;
using System;
using System.Linq;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace SalsasAPI.Services
{
    public class ETLService : IHostedService, IDisposable
    {
        private readonly IServiceProvider _serviceProvider;
        private Timer _timer;

        public ETLService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            // Configurar el temporizador para que se ejecute cada día a la medianoche
            _timer = new Timer(DoWork, null, TimeSpan.Zero, TimeSpan.FromDays(1));
            return Task.CompletedTask;
        }

        private void DoWork(object state)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<SalsaContext>();

                try
                {
                    // Eliminar registros anteriores
                    context.InventarioReporte.RemoveRange(context.InventarioReporte);
                    context.RankingClientes.RemoveRange(context.RankingClientes);
                    context.VentasPorProductoPeriodos.RemoveRange(context.VentasPorProductoPeriodos);
                    context.SaveChanges();

                    // Extracción de datos de materias primas
                    var materiasPrimas = context.MateriaPrimas
                        .Include(mp => mp.DetalleMateriaPrimas)
                        .Select(mp => new InventarioReporte
                        {
                            Tipo = "Materia Prima",
                            Nombre = mp.NombreMateria,
                            Cantidad = mp.DetalleMateriaPrimas.Sum(dmp => dmp.CantidadExistentes),
                            UltimaActualizacion = DateTime.Now
                        }).ToList();

                    // Extracción de datos de productos
                    var productos = context.Productos
                        .Include(p => p.DetalleProductos)
                        .Select(p => new InventarioReporte
                        {
                            Tipo = "Producto",
                            Nombre = p.NombreProducto,
                            Cantidad = p.DetalleProductos.Sum(dp => dp.CantidadExistentes),
                            UltimaActualizacion = DateTime.Now
                        }).ToList();

                    // Cargar datos en la tabla de reporte de inventarios
                    context.InventarioReporte.AddRange(materiasPrimas);
                    context.InventarioReporte.AddRange(productos);
                    context.SaveChanges();

                    // Definir el período de análisis (por ejemplo, el último mes)
                    var periodoInicio = DateTime.Now.AddMonths(-1).Date;
                    var periodoFin = DateTime.Now.Date;

                    // Extracción de datos de ventas
                    var ventas = context.Venta
                        .Include(v => v.DetalleVenta)
                        .ThenInclude(dv => dv.IdProductoNavigation)
                        .Include(v => v.IdUsuarioNavigation)
                        .Where(v => v.FechaVenta >= periodoInicio && v.FechaVenta <= periodoFin)
                        .ToList();

                    // Transformación y carga de datos de RankingClientes
                    var rankingClientes = ventas
                        .GroupBy(v => new { v.IdUsuario, NombreUsuario = v.IdUsuarioNavigation.NombreUsuario })
                        .Select(g => new RankingClientes
                        {
                            IdUsuario = g.Key.IdUsuario ?? 0,
                            NombreUsuario = g.Key.NombreUsuario,
                            ComprasTotales = g.Sum(v => v.Total),
                            ProductosComprados = JsonSerializer.Serialize(g.SelectMany(v => v.DetalleVenta)
                                                            .Select(dv => new { dv.IdProductoNavigation.NombreProducto, dv.Cantidad })),
                            UltimaActualizacion = DateTime.Now
                        })
                        .ToList();

                    context.RankingClientes.AddRange(rankingClientes);
                    context.SaveChanges();

                    // Transformación y carga de datos de VentasPorProductoPeriodo
                    var ventasPorProductoPeriodo = ventas
                        .SelectMany(v => v.DetalleVenta)
                        .GroupBy(dv => new { dv.IdProducto, dv.IdProductoNavigation.NombreProducto })
                        .Select(g => new VentasPorProductoPeriodo
                        {
                            ProductoId = g.Key.IdProducto ?? 0,
                            NombreProducto = g.Key.NombreProducto,
                            PeriodoInicio = periodoInicio,
                            PeriodoFin = periodoFin,
                            NumeroVentas = g.Count(),
                            CantidadVendida = g.Sum(dv => dv.Cantidad),
                            TotalRecaudado = g.Sum(dv => dv.Subtotal),
                            IndicadorGlobal = CalculateIndicadorGlobal(g.Count(), g.Sum(dv => dv.Subtotal)) // Ajustar aquí
                        })
                        .ToList();

                    // Cargar datos en la tabla de VentasPorProductoPeriodo
                    context.VentasPorProductoPeriodos.AddRange(ventasPorProductoPeriodo);
                    context.SaveChanges();
                }
                catch (DbUpdateException ex)
                {
                    Console.WriteLine($"Error actualizando la base de datos: {ex.InnerException?.Message ?? ex.Message}");
                }
            }
        }

        private string CalculateIndicadorGlobal(int numeroVentas, double totalRecaudado)
        {
            // Ejemplo simple de cálculo del indicador global basado en número de ventas y total recaudado
            if (numeroVentas > 50 || totalRecaudado > 1000)
            {
                return "Alto";
            }
            else if (numeroVentas > 20 || totalRecaudado > 500)
            {
                return "Medio";
            }
            else
            {
                return "Bajo";
            }
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _timer?.Change(Timeout.Infinite, 0);
            return Task.CompletedTask;
        }

        public void Dispose()
        {
            _timer?.Dispose();
        }
    }
}
