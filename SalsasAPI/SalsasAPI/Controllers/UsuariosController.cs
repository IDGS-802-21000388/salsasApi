using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SalsasAPI.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BCrypt.Net;


namespace SalsasAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private readonly SalsaContext _context;

        public UsuariosController(SalsaContext context)
        {
            _context = context;
        }

        // GET: api/usuarios
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Usuario>>> GetUsuarios()
        {
            return await _context.Usuarios
                                 .Include(u => u.Direccion)
                                 .ToListAsync();
        }

        // GET: api/usuarios/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Usuario>> GetUsuario(int id)
        {
            var usuario = await _context.Usuarios
                                        .Include(u => u.Direccion)
                                        .FirstOrDefaultAsync(u => u.IdUsuario == id);

            if (usuario == null)
            {
                return NotFound();
            }

            return usuario;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutUsuario(int id, Usuario usuario)
        {
            if (id != usuario.IdUsuario)
            {
                return BadRequest();
            }

            var existingUser = await _context.Usuarios
                .Include(u => u.Direccion)
                .FirstOrDefaultAsync(u => u.IdUsuario == id);

            if (existingUser == null)
            {
                return NotFound();
            }

            // Preserve the existing dateLastToken value
            var existingDateLastToken = existingUser.DateLastToken;

            // Update existing user's properties
            existingUser.Nombre = usuario.Nombre;
            existingUser.NombreUsuario = usuario.NombreUsuario;
            existingUser.Correo = usuario.Correo;

            // Only hash the password if it's been provided
            if (!string.IsNullOrEmpty(usuario.Contrasenia))
            {
                existingUser.Contrasenia = HashPassword(usuario.Contrasenia);
            }

            existingUser.Rol = usuario.Rol;
            existingUser.Estatus = usuario.Estatus;
            existingUser.Telefono = usuario.Telefono;
            existingUser.Intentos = usuario.Intentos;

            // Restore the existing dateLastToken value if not provided
            existingUser.DateLastToken = usuario.DateLastToken ?? existingDateLastToken;

            // Update existing user's address properties
            existingUser.Direccion.Estado = usuario.Direccion.Estado;
            existingUser.Direccion.Municipio = usuario.Direccion.Municipio;
            existingUser.Direccion.CodigoPostal = usuario.Direccion.CodigoPostal;
            existingUser.Direccion.Colonia = usuario.Direccion.Colonia;
            existingUser.Direccion.Calle = usuario.Direccion.Calle;
            existingUser.Direccion.NumExt = usuario.Direccion.NumExt;
            existingUser.Direccion.NumInt = usuario.Direccion.NumInt;
            existingUser.Direccion.Referencia = usuario.Direccion.Referencia;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UsuarioExists(id))
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





        [HttpPost]
        public async Task<ActionResult<Usuario>> PostUsuario(Usuario usuario)
        {
            // Encrypt the password before saving it
            usuario.Contrasenia = HashPassword(usuario.Contrasenia);

            _context.Usuarios.Add(usuario);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetUsuario), new { id = usuario.IdUsuario }, usuario);
        }

        private string HashPassword(string password)
        {
            // Use a hashing function, e.g., BCrypt
            return BCrypt.Net.BCrypt.HashPassword(password);
        }


        // DELETE: api/usuarios/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUsuario(int id)
        {
            var usuario = await _context.Usuarios.FindAsync(id);
            if (usuario == null)
            {
                return NotFound();
            }

            usuario.Estatus = 0;
            _context.Entry(usuario).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // PUT: api/usuarios/activate/5
        [HttpPut("activate/{id}")]
        public async Task<IActionResult> ActivateUsuario(int id)
        {
            var usuario = await _context.Usuarios.FindAsync(id);
            if (usuario == null)
            {
                return NotFound();
            }

            usuario.Estatus = 1;
            _context.Entry(usuario).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UsuarioExists(int id)
        {
            return _context.Usuarios.Any(e => e.IdUsuario == id);
        }
    }
}