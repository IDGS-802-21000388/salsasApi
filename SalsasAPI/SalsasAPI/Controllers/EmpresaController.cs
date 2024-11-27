using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SalsasAPI.Models;

[Route("api/[controller]")]
[ApiController]
public class EmpresaController : ControllerBase
{
    private readonly SalsaContext _context;

    public EmpresaController(SalsaContext context)
    {
        _context = context;
    }

    // GET: api/empresa
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Empresa>>> GetEmpresas()
    {
        var empresas = await _context.Empresa
            .Include(e => e.Direccion) // Incluir la dirección de la empresa
            .ToListAsync();

        return empresas;
    }

    // GET: api/empresa/{id}
    [HttpGet("{id}")]
    public async Task<ActionResult<Empresa>> GetEmpresa(int id)
    {
        var empresa = await _context.Empresa
            .Include(e => e.Direccion) // Incluir la dirección de la empresa
            .FirstOrDefaultAsync(e => e.idEmpresa == id);

        if (empresa == null)
        {
            return NotFound();
        }

        return empresa;
    }



    // POST: api/empresa
    [HttpPost]
    public async Task<ActionResult<Empresa>> CreateEmpresa(Empresa empresa)
    {
        // Verificar si la empresa ya existe
        if (_context.Empresa.Any(e => e.nombre == empresa.nombre))
        {
            return BadRequest("La empresa ya existe.");
        }

        // Agregar la empresa
        _context.Empresa.Add(empresa);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetEmpresa), new { id = empresa.idEmpresa }, empresa);
    }

    // PUT: api/empresa/{id}
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateEmpresa(int id, Empresa empresa)
    {
        if (id != empresa.idEmpresa)
        {
            return BadRequest();
        }

        _context.Entry(empresa).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!EmpresaExists(id))
            {
                return NotFound();
            }
            throw;
        }

        return NoContent();
    }

    // DELETE: api/empresa/{id}
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteEmpresa(int id)
    {
        var empresa = await _context.Empresa.FindAsync(id);
        if (empresa == null)
        {
            return NotFound();
        }

        _context.Empresa.Remove(empresa);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    [HttpGet]
    [Route("api/empresa-con-usuarios")]
    public async Task<IActionResult> GetEmpresasConUsuarios()
    {
        try
        {
            // Obtener las empresas e incluir la dirección
            var empresas = await _context.Empresa
                .Include(e => e.Direccion) // Incluir la dirección de cada empresa
                .ToListAsync();

            // Crear una lista para el resultado
            var resultado = new List<object>();

            foreach (var empresa in empresas)
            {
                // Obtener los usuarios relacionados a través de EmpresaUsuario
                var usuarios = await _context.EmpresaUsuario
                    .Where(eu => eu.IdEmpresa == empresa.idEmpresa)
                    .Join(_context.Usuarios,
                          eu => eu.IdUsuario,
                          u => u.IdUsuario,
                          (eu, u) => new
                          {
                              u.IdUsuario,
                              u.Nombre,
                              u.Correo,
                              u.Telefono
                          })
                    .ToListAsync();

                // Agregar la empresa, su dirección y sus usuarios al resultado
                resultado.Add(new
                {
                    empresa.idEmpresa,
                    empresa.nombre,
                    empresa.telefono,
                    Direccion = empresa.Direccion == null ? null : new
                    {
                        empresa.Direccion.IdDireccion,
                        empresa.Direccion.Estado,
                        empresa.Direccion.Municipio,
                        empresa.Direccion.CodigoPostal,
                        empresa.Direccion.Colonia,
                        empresa.Direccion.Calle,
                        empresa.Direccion.NumExt,
                        empresa.Direccion.NumInt,
                        empresa.Direccion.Referencia
                    },
                    Usuarios = usuarios
                });
            }

            return Ok(resultado);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Error al obtener datos: {ex.Message}");
        }
    }




    private bool EmpresaExists(int id)
    {
        return _context.Empresa.Any(e => e.idEmpresa == id);
    }

   
}
