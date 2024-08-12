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
    public class DireccionController : ControllerBase
    {
        private readonly SalsaContext _context;

        public DireccionController(SalsaContext context)
        {
            _context = context;
        }

        // GET: api/direccion
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Direccion>>> GetDirecciones()
        {
            return await _context.Direccion.ToListAsync();
        }

        // GET: api/direccion/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Direccion>> GetDireccion(int id)
        {
            var direccion = await _context.Direccion.FindAsync(id);

            if (direccion == null)
            {
                return NotFound();
            }

            return direccion;
        }

        // PUT: api/direccion/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDireccion(int id, Direccion direccion)
        {
            if (id != direccion.IdDireccion)
            {
                return BadRequest();
            }

            _context.Entry(direccion).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DireccionExists(id))
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

        // POST: api/direccion
        [HttpPost]
        public async Task<ActionResult<Direccion>> PostDireccion(Direccion direccion)
        {
            _context.Direccion.Add(direccion);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetDireccion), new { id = direccion.IdDireccion }, direccion);
        }

        // DELETE: api/direccion/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDireccion(int id)
        {
            var direccion = await _context.Direccion.FindAsync(id);
            if (direccion == null)
            {
                return NotFound();
            }

            _context.Direccion.Remove(direccion);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool DireccionExists(int id)
        {
            return _context.Direccion.Any(e => e.IdDireccion == id);
        }
    }
}
