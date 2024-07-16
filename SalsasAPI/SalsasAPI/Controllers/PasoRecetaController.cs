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
    public class PasoRecetaController : ControllerBase
    {
        private readonly SalsaContext _context;

        public PasoRecetaController(SalsaContext context)
        {
            _context = context;
        }

        [HttpGet("producto/{idProducto}")]
        public async Task<ActionResult<IEnumerable<PasoReceta>>> GetPasosReceta(int idProducto)
        {
            var pasosReceta = await _context.PasoReceta
                .Where(pr => pr.IdProducto == idProducto)
                .OrderBy(pr => pr.Paso)
                .ToListAsync();

            return pasosReceta;
        }
    }
}
