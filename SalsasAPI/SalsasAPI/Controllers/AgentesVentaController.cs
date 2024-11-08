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

        [HttpGet]
        public async Task<ActionResult<IEnumerable<object>>> GetAgentesVenta()
        {
            var agentesVenta = await _context.AgentesVenta
                .Include(av => av.Agente) // Incluir el agente
                .ThenInclude(a => a.Direccion) // Incluir la dirección del agente
                .Include(av => av.Cliente) // Incluir el cliente
                .ThenInclude(c => c.Direccion) // Incluir la dirección del cliente
                .Select(av => new
                {
                    av.IdAgentesVenta,
                    // Información del Agente
                    IdUsuarioAgente = av.Agente.IdUsuario, // Incluir el idUsuario del agente
                    NombreAgente = av.Agente.Nombre,
                    CorreoAgente = av.Agente.Correo,
                    TelefonoAgente = av.Agente.Telefono,
                    DireccionAgente = av.Agente.Direccion.Calle + " " + av.Agente.Direccion.NumExt +
                                      (av.Agente.Direccion.NumInt != null ? " Int " + av.Agente.Direccion.NumInt : "") + ", " +
                                      av.Agente.Direccion.Colonia + ", " +
                                      av.Agente.Direccion.Municipio + ", " +
                                      av.Agente.Direccion.Estado + ", C.P. " + av.Agente.Direccion.CodigoPostal,
                    // Información del Cliente
                    NombreCliente = av.Cliente.Nombre,
                    CorreoCliente = av.Cliente.Correo,
                    TelefonoCliente = av.Cliente.Telefono,
                    DireccionCliente = av.Cliente.Direccion.Calle + " " + av.Cliente.Direccion.NumExt +
                                      (av.Cliente.Direccion.NumInt != null ? " Int " + av.Cliente.Direccion.NumInt : "") + ", " +
                                      av.Cliente.Direccion.Colonia + ", " +
                                      av.Cliente.Direccion.Municipio + ", " +
                                      av.Cliente.Direccion.Estado + ", C.P. " + av.Cliente.Direccion.CodigoPostal
                })
                .ToListAsync();

            return Ok(agentesVenta);
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
