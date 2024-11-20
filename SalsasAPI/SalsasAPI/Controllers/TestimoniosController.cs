using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SalsasAPI.Models;

[Route("api/[controller]")]
[ApiController]
public class TestimoniosController : ControllerBase
{
    private readonly SalsaContext _context;

    public TestimoniosController(SalsaContext context)
    {
        _context = context;
    }

    // GET: api/Testimonios
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Testimonio>>> GetTestimonios()
    {
        return await _context.Testimonios.ToListAsync();
    }

    // GET: api/Testimonios/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Testimonio>> GetTestimonio(int id)
    {
        var testimonio = await _context.Testimonios.FindAsync(id);

        if (testimonio == null)
        {
            return NotFound();
        }

        return testimonio;
    }

    
        // Crear testimonio
        [HttpPost]
        public async Task<IActionResult> CrearTestimonio([FromBody] TestimonioDto testimonioDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var testimonio = new Testimonio
            {
                IdUsuario = testimonioDto.IdUsuario,
                IdProducto = testimonioDto.IdProducto,
                Comentario = testimonioDto.Comentario,
                Calificacion = testimonioDto.Calificacion,  // Procesar la calificación
                FechaTestimonio = DateTime.Now,
                Estatus = 1 // Valor predeterminado para el estado
            };

            _context.Testimonios.Add(testimonio);
            await _context.SaveChangesAsync();

            return Ok(testimonio);
        }

        // Otros métodos como obtener testimonios pueden ir aquí



    // PUT: api/Testimonios/5
    [HttpPut("{id}")]
    public async Task<IActionResult> PutTestimonio(int id, Testimonio testimonio)
    {
        if (id != testimonio.IdTestimonio)
        {
            return BadRequest();
        }

        _context.Entry(testimonio).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!TestimonioExists(id))
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

    // DELETE: api/Testimonios/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTestimonio(int id)
    {
        var testimonio = await _context.Testimonios.FindAsync(id);
        if (testimonio == null)
        {
            return NotFound();
        }

        _context.Testimonios.Remove(testimonio);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool TestimonioExists(int id)
    {
        return _context.Testimonios.Any(e => e.IdTestimonio == id);
    }



    [HttpGet("producto/{idProducto}")]
    public async Task<ActionResult<IEnumerable<Testimonio>>> GetTestimoniosByProducto(int idProducto)
    {
        var testimonios = await _context.Testimonios
                                         .Where(t => t.IdProducto == idProducto)
                                         .ToListAsync();

        if (testimonios == null || !testimonios.Any())
        {
            return NotFound();
        }

        return Ok(testimonios);
    }


}
