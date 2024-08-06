using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SalsasAPI.Models;
using System.Threading.Tasks;

namespace SalsasAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ETLController : ControllerBase
    {
        private readonly IServiceProvider _serviceProvider;


        private readonly SalsaContext _context;

        public ETLController(SalsaContext context)
        {
            _context = context;
        }

        [HttpGet("getVentasPorProductoPeriodos")]
        public async Task<ActionResult<IEnumerable<VentasPorProductoPeriodo>>> GetVentasPorProductoPeriodos()
        {
            // Incluye la relación de navegación con la entidad Producto
            return await _context.VentasPorProductoPeriodos
                                 .Include(v => v.Producto) // Asegúrate de que el nombre de la propiedad de navegación sea correcto
                                 .ToListAsync();
        }


    }
}
