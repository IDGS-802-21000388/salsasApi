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
    public class MedidaController : ControllerBase
    {
        private readonly SalsaContext _context;

        public MedidaController(SalsaContext context)
        {
            _context = context;
        }

        // GET: api/medida
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Medidum>>> GetMedidas()
        {
            return await _context.Medida.ToListAsync();
        }

        // GET: api/medida/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Medidum>> GetMedida(int id)
        {
            var medida = await _context.Medida.FindAsync(id);

            if (medida == null)
            {
                return NotFound();
            }

            return medida;
        }

        // PUT: api/medida/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMedida(int id, Medidum medida)
        {
            if (id != medida.IdMedida)
            {
                return BadRequest();
            }

            _context.Entry(medida).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MedidaExists(id))
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

        // POST: api/medida
        [HttpPost]
        public async Task<ActionResult<Medidum>> PostMedida(Medidum medida)
        {
            _context.Medida.Add(medida);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetMedida), new { id = medida.IdMedida }, medida);
        }


        private bool MedidaExists(int id)
        {
            return _context.Medida.Any(e => e.IdMedida == id);
        }
    }
}
