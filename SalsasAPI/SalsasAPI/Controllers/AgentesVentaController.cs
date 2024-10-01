using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SalsasAPI.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalsasAPI.Controllers
{
    // endpoint: api/agentesventa

    [Route("api/[controller]")]
    [ApiController]
    public class AgentesVentaController : ControllerBase
    {
        private readonly SalsaContext _context;

        public AgentesVentaController(SalsaContext context)
        {
            _context = context;
        }

        // GET: api/agentesventa
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AgentesVenta>>> GetAgentesVenta()
        {
            return await _context.AgentesVenta.ToListAsync();
        }

        // GET: api/agentesventa/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AgentesVenta>> GetAgenteVenta(int id)
        {
            var agenteVenta = await _context.AgentesVenta.FindAsync(id);

            if (agenteVenta == null)
            {
                return NotFound();
            }

            return agenteVenta;
        }

        // POST: api/agentesventa
        [HttpPost]
        public async Task<ActionResult<AgentesVenta>> PostAgenteVenta(AgentesVenta agenteVenta)
        {
            _context.AgentesVenta.Add(agenteVenta);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetAgenteVenta), new { id = agenteVenta.IdAgentesVenta }, agenteVenta);
        }

        // DELETE: api/agentesventa/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAgenteVenta(int id)
        {
            var agenteVenta = await _context.AgentesVenta.FindAsync(id);
            if (agenteVenta == null)
            {
                return NotFound();
            }

            _context.AgentesVenta.Remove(agenteVenta);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // Método privado para verificar si existe el agente
        private bool AgenteVentaExists(int id)
        {
            return _context.AgentesVenta.Any(e => e.IdAgentesVenta == id);
        }
    }
}
