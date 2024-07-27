    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using SalsasAPI.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    namespace SalsasAPI.Controllers
    {
        [Route("api/[controller]")]
        [ApiController]
        public class ReportsController : ControllerBase
        {
            private readonly SalsaContext _context;

            public ReportsController(SalsaContext context)
            {
                _context = context;
            }

            [HttpGet("total-sales")]
            public async Task<ActionResult<double>> GetTotalSales([FromQuery] DateTime? startDate, [FromQuery] DateTime? endDate)
            {
                var query = _context.Venta.AsQueryable();

                if (startDate.HasValue)
                    query = query.Where(v => v.FechaVenta >= startDate.Value);
                if (endDate.HasValue)
                    query = query.Where(v => v.FechaVenta <= endDate.Value);

                var totalSales = await query.SumAsync(v => v.Total);

                return Ok(totalSales);
            }

            [HttpGet("total-purchases")]
            public async Task<ActionResult<double>> GetTotalPurchases([FromQuery] DateTime? startDate, [FromQuery] DateTime? endDate)
            {
                var query = _context.Compras.Include(c => c.IdDetalleMateriaPrimaNavigation).AsQueryable();

                if (startDate.HasValue)
                    query = query.Where(c => c.IdDetalleMateriaPrimaNavigation.FechaCompra >= startDate.Value);
                if (endDate.HasValue)
                    query = query.Where(c => c.IdDetalleMateriaPrimaNavigation.FechaCompra <= endDate.Value);

                var totalPurchases = await query.SumAsync(c => c.cantidadComprada * c.IdMateriaPrimaNavigation.PrecioCompra);

                return Ok(totalPurchases);
            }

            [HttpGet("monthly-sales")]
            public async Task<ActionResult<IEnumerable<object>>> GetMonthlySales([FromQuery] int year)
            {
                var monthlySales = await _context.Venta
                    .Where(v => v.FechaVenta.Year == year)
                    .GroupBy(v => v.FechaVenta.Month)
                    .Select(g => new
                    {
                        Month = g.Key,
                        Total = g.Sum(v => v.Total)
                    })
                    .ToListAsync();

                return Ok(monthlySales);
            }

            [HttpGet("monthly-purchases")]
            public async Task<ActionResult<IEnumerable<object>>> GetMonthlyPurchases([FromQuery] int year)
            {
                var monthlyPurchases = await _context.Compras
                    .Include(c => c.IdDetalleMateriaPrimaNavigation)
                    .Where(c => c.IdDetalleMateriaPrimaNavigation.FechaCompra.Year == year)
                    .GroupBy(c => c.IdDetalleMateriaPrimaNavigation.FechaCompra.Month)
                    .Select(g => new
                    {
                        Month = g.Key,
                        Total = g.Sum(c => c.cantidadComprada * c.IdMateriaPrimaNavigation.PrecioCompra)
                    })
                    .ToListAsync();

                return Ok(monthlyPurchases);
            }
        }
    }
