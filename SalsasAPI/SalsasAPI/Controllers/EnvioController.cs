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
    public class EnvioController : ControllerBase
    {
        private readonly SalsaContext _context;

        public EnvioController(SalsaContext context)
        {
            _context = context;
        }

        // GET: api/envio
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Envio>>> GetEnvios()
        {
            return await _context.Envios
                .Include(e => e.IdPaqueteriaNavigation)
                .Include(e => e.IdVentaNavigation)
                .ToListAsync();
        }

        // GET: api/envio/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Envio>> GetEnvio(int id)
        {
            var envio = await _context.Envios
                .Include(e => e.IdPaqueteriaNavigation)
                .Include(e => e.IdVentaNavigation)
                .FirstOrDefaultAsync(e => e.IdEnvio == id);

            if (envio == null)
            {
                return NotFound();
            }

            return envio;
        }

        // PUT: api/envio/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEnvio(int id, Envio envio)
        {
            if (id != envio.IdEnvio)
            {
                return BadRequest();
            }

            _context.Entry(envio).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EnvioExists(id))
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

        // POST: api/envio
        [HttpPost]
        public async Task<ActionResult<Envio>> PostEnvio(Envio envio)
        {
            _context.Envios.Add(envio);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetEnvio), new { id = envio.IdEnvio }, envio);
        }

        // DELETE: api/envio/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEnvio(int id)
        {
            var envio = await _context.Envios.FindAsync(id);
            if (envio == null)
            {
                return NotFound();
            }

            _context.Envios.Remove(envio);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool EnvioExists(int id)
        {
            return _context.Envios.Any(e => e.IdEnvio == id);
        }
    }
}
