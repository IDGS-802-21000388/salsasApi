using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SalsasAPI.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SalsasAPI.Models;

namespace TuApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmpresaController : ControllerBase
    {
        private readonly SalsaContext _context;

        public EmpresaController(SalsaContext context)
        {
            _context = context;
        }

        // GET: api/Empresa
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Empresa>>> GetEmpresas()
        {
            var empresas = await _context.Empresa
                                         .Include(e => e.Usuario)    // Incluye los datos de Usuario relacionados con la Empresa
                                         .ThenInclude(u => u.Direccion)  // Incluye la Dirección relacionada con el Usuario
                                         .Include(e => e.Direccion)  // Incluye los datos de Direccion relacionados con la Empresa
                                         .ToListAsync();  // Recupera todas las empresas con los datos relacionados

            return Ok(empresas);  // Devuelve el resultado como una lista
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<Empresa>> GetEmpresa(int id)
        {
            var empresa = await _context.Empresa
                .Include(e => e.Usuario)  // Incluye el Usuario relacionado con la Empresa
                    .ThenInclude(u => u.Direccion)  // Incluye la Dirección del Usuario
                .Include(e => e.Direccion)  // Incluye la Dirección de la Empresa
                .FirstOrDefaultAsync(e => e.IdEmpresa == id);  // Filtra por el Id de la Empresa

            if (empresa == null)
            {
                return NotFound();
            }

            return Ok(empresa);  // Retorna la empresa con sus datos incluidos
        }



        [HttpPost]
        public async Task<ActionResult<Empresa>> PostEmpresa([FromBody] EmpresaDto empresaDto)
        {
            if (empresaDto == null)
            {
                return BadRequest("Datos de la empresa no válidos");
            }

            // Crear una nueva dirección a partir del DTO de la dirección
            var direccion = new Direccion
            {
                Estado = empresaDto.Direccion.Estado,
                Municipio = empresaDto.Direccion.Municipio,
                CodigoPostal = empresaDto.Direccion.CodigoPostal,
                Colonia = empresaDto.Direccion.Colonia,
                Calle = empresaDto.Direccion.Calle,
                NumExt = empresaDto.Direccion.NumExt,
                NumInt = empresaDto.Direccion.NumInt,
                Referencia = empresaDto.Direccion.Referencia
            };

            // Agregar la dirección al contexto
            _context.Direccion.Add(direccion);
            await _context.SaveChangesAsync(); // Guardar la dirección para obtener el Id

            // Crear la empresa con el IdUsuario y el IdDireccion recién creado
            var empresa = new Empresa
            {
                Nombre = empresaDto.Nombre,
                Telefono = empresaDto.Telefono,
                IdUsuario = empresaDto.IdUsuario,  // Solo asignamos el idUsuario
                IdDireccion = direccion.IdDireccion // Asignamos el IdDireccion de la nueva dirección
            };

            // Agregar la empresa al contexto de la base de datos
            _context.Empresa.Add(empresa);
            await _context.SaveChangesAsync();

            // Devuelve la empresa recién creada con su Id
            return CreatedAtAction("GetEmpresa", new { id = empresa.IdEmpresa }, empresa);
        }




        // PUT: api/Empresa/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEmpresa(int id, Empresa empresa)
        {
            if (id != empresa.IdEmpresa)
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
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // DELETE: api/Empresa/{id}
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

        // Método auxiliar para verificar si una empresa existe
        private bool EmpresaExists(int id)
        {
            return _context.Empresa.Any(e => e.IdEmpresa == id);
        }
    }
}
