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
    public class MateriaPrimaController : ControllerBase
    {
        private readonly SalsaContext _context;

        public MateriaPrimaController(SalsaContext context)
        {
            _context = context;
        }

        // GET: api/materiaprima
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MateriaPrima>>> GetMateriasPrimas()
        {
            return await _context.MateriaPrimas.ToListAsync();
        }

        // GET: api/materiaprima/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MateriaPrima>> GetMateriaPrima(int id)
        {
            var materiaPrima = await _context.MateriaPrimas.FindAsync(id);

            if (materiaPrima == null)
            {
                return NotFound();
            }

            return materiaPrima;
        }

        // PUT: api/materiaprima/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMateriaPrima(int id, MateriaPrima materiaPrima)
        {
            if (id != materiaPrima.IdMateriaPrima)
            {
                return BadRequest();
            }

            _context.Entry(materiaPrima).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MateriaPrimaExists(id))
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

        // POST: api/materiaprima
        [HttpPost]
        public async Task<ActionResult<MateriaPrima>> PostMateriaPrima(MateriaPrima materiaPrima)
        {
            _context.MateriaPrimas.Add(materiaPrima);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetMateriaPrima), new { id = materiaPrima.IdMateriaPrima }, materiaPrima);
        }

        

        private bool MateriaPrimaExists(int id)
        {
            return _context.MateriaPrimas.Any(e => e.IdMateriaPrima == id);
        }
    }
}
