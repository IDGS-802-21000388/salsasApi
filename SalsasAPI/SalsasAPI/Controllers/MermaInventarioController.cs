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
    public class MermaInventarioController : ControllerBase
    {
        private readonly SalsaContext _context;

        public MermaInventarioController(SalsaContext context)
        {
            _context = context;
        }

        // GET: api/mermainventario
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MermaInventario>>> GetMermasInventario()
        {
            return await _context.MermaInventarios
                .Include(m => m.IdMateriaPrimaNavigation)
                .Include(m => m.IdDetalleMateriaPrimaNavigation)
                .ToListAsync();
        }

        // GET: api/mermainventario/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MermaInventario>> GetMermaInventario(int id)
        {
            var mermaInventario = await _context.MermaInventarios
                .Include(m => m.IdMateriaPrimaNavigation)
                .Include(m => m.IdDetalleMateriaPrimaNavigation)
                .FirstOrDefaultAsync(m => m.IdMerma == id);

            if (mermaInventario == null)
            {
                return NotFound();
            }

            return mermaInventario;
        }

        // PUT: api/mermainventario/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMermaInventario(int id, MermaInventario mermaInventario)
        {
            if (id != mermaInventario.IdMerma)
            {
                return BadRequest();
            }

            _context.Entry(mermaInventario).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MermaInventarioExists(id))
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

        // POST: api/mermainventario
        [HttpPost]
        public async Task<ActionResult<MermaInventario>> PostMermaInventario(MermaInventario mermaInventario)
        {
            _context.MermaInventarios.Add(mermaInventario);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (MermaInventarioExists(mermaInventario.IdMerma))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction(nameof(GetMermaInventario), new { id = mermaInventario.IdMerma }, mermaInventario);
        }

        // DELETE: api/mermainventario/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMermaInventario(int id)
        {
            var mermaInventario = await _context.MermaInventarios.FindAsync(id);
            if (mermaInventario == null)
            {
                return NotFound();
            }

            _context.MermaInventarios.Remove(mermaInventario);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool MermaInventarioExists(int id)
        {
            return _context.MermaInventarios.Any(e => e.IdMerma == id);
        }
    }
}
