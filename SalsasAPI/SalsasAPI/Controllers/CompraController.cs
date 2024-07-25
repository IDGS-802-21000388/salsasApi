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
    public class CompraController : ControllerBase
    {
        private readonly SalsaContext _context;

        public CompraController(SalsaContext context)
        {
            _context = context;
        }

        // GET: api/compra
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Compra>>> GetCompras()
        {
            return await _context.Compras.ToListAsync();
        }

        // GET: api/compra/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Compra>> GetCompra(int id)
        {
            var compra = await _context.Compras.FindAsync(id);

            if (compra == null)
            {
                return NotFound();
            }

            return compra;
        }

        // PUT: api/compra/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCompra(int id, Compra compra)
        {
            if (id != compra.IdCompra)
            {
                return BadRequest();
            }

            _context.Entry(compra).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CompraExists(id))
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

        // POST: api/compra
        [HttpPost]
        public async Task<ActionResult<Compra>> PostCompra(Compra compra)
        {
            _context.Compras.Add(compra);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetCompra), new { id = compra.IdCompra }, compra);
        }

        private bool CompraExists(int id)
        {
            return _context.Compras.Any(e => e.IdCompra == id);
        }
    }
}
