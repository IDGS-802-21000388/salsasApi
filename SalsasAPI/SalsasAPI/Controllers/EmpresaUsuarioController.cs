using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SalsasAPI.Models;
using System.Threading.Tasks;

[Route("api/[controller]")]
[ApiController]
public class EmpresaUsuarioController : ControllerBase
{
    private readonly SalsaContext _context;

    public EmpresaUsuarioController(SalsaContext context)
    {
        _context = context;
    }

    // =======================
    // API para EmpresaUsuario
    // =======================

    // POST: api/empresaUsuario
    [HttpPost]
    public async Task<ActionResult<EmpresaUsuario>> CreateEmpresaUsuario([FromBody] EmpresaUsuario request)
    {
        // Verificar si la empresa y el usuario existen por sus ID
        var empresaExists = await _context.Empresa.AnyAsync(e => e.idEmpresa == request.IdEmpresa);
        var usuarioExists = await _context.Usuarios.AnyAsync(u => u.IdUsuario == request.IdUsuario);

        if (!empresaExists)
        {
            return BadRequest("La empresa especificada no existe.");
        }

        if (!usuarioExists)
        {
            return BadRequest("El usuario especificado no existe.");
        }

        // Verificar si la relación Empresa-Usuario ya existe
        if (await _context.EmpresaUsuario.AnyAsync(eu => eu.IdEmpresa == request.IdEmpresa && eu.IdUsuario == request.IdUsuario))
        {
            return BadRequest("La relación entre la empresa y el usuario ya existe.");
        }

        // Crear la nueva relación EmpresaUsuario solo con los IDs
        var empresaUsuario = new EmpresaUsuario
        {
            IdEmpresa = request.IdEmpresa,
            IdUsuario = request.IdUsuario
        };

        _context.EmpresaUsuario.Add(empresaUsuario);
        await _context.SaveChangesAsync();

        // Devolver la respuesta con el ID del nuevo registro
        return CreatedAtAction(nameof(GetEmpresaUsuario), new { id = empresaUsuario.IdEmpresaUsuario }, empresaUsuario);
    }

    // GET: api/empresaUsuario
    [HttpGet]
    public async Task<ActionResult<IEnumerable<EmpresaUsuario>>> GetEmpresaUsuarios()
    {
        var empresaUsuarios = await _context.EmpresaUsuario.ToListAsync();

        if (empresaUsuarios == null || !empresaUsuarios.Any())
        {
            return NotFound("No se encontraron registros de EmpresaUsuario.");
        }

        return empresaUsuarios;
    }


    // GET: api/empresaUsuario/{id}
    [HttpGet("{id}")]
    public async Task<ActionResult<EmpresaUsuario>> GetEmpresaUsuario(int id)
    {
        var empresaUsuario = await _context.EmpresaUsuario
            .FirstOrDefaultAsync(eu => eu.IdEmpresaUsuario == id);

        if (empresaUsuario == null)
        {
            return NotFound();
        }

        // Aquí, si necesitas las entidades relacionadas, puedes incluirlas con Include
        // Pero no es necesario si solo te interesa devolver los IDs
        return empresaUsuario;
    }
}
