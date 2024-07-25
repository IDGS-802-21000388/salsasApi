using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SalsasAPI.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace SalsasAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : Controller
    {
        private readonly SalsaContext _context;

        public LoginController(SalsaContext context)
        {
            _context = context;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequest loginRequest)
        {
            var user = _context.Usuarios
                               .FirstOrDefault(u => u.Correo == loginRequest.Correo && u.Contrasenia == loginRequest.Contrasenia);

            if (user == null)
            {
                return Unauthorized("Invalid credentials");
            }

            // Registrar el log del usuario
            var logEntry = new LogsUser
            {
                Procedimiento = "Login",
                LastDate = DateTime.Now,
                IdUsuario = user.IdUsuario
            };
            _context.LogsUsers.Add(logEntry);
            _context.SaveChanges();

            return Ok(new { Message = "Login successful", User = user });
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest registerRequest)
        {
            // Crear y guardar la dirección
            var direccion = new Direccion
            {
                Estado = registerRequest.Direccion.Estado,
                Municipio = registerRequest.Direccion.Municipio,
                CodigoPostal = registerRequest.Direccion.CodigoPostal,
                Colonia = registerRequest.Direccion.Colonia,
                Calle = registerRequest.Direccion.Calle,
                NumExt = registerRequest.Direccion.NumExt,
                NumInt = registerRequest.Direccion.NumInt,
                Referencia = registerRequest.Direccion.Referencia
            };

            _context.Direccion.Add(direccion);
            await _context.SaveChangesAsync();

            // Crear y guardar el usuario con el ID de la dirección
            var user = new Usuario
            {
                Nombre = registerRequest.Nombre,
                NombreUsuario = registerRequest.NombreUsuario,
                Correo = registerRequest.Correo,
                Contrasenia = registerRequest.Contrasenia,
                Telefono = registerRequest.Telefono,
                Rol = "cliente",
                Estatus = 1,
                Intentos = 0,
                IdDireccion = direccion.IdDireccion
            };

            _context.Usuarios.Add(user);
            await _context.SaveChangesAsync();

            return Ok(new { Message = "User registered successfully", User = user });
        }

        public class LoginRequest
        {
            public string Correo { get; set; } = null!;
            public string Contrasenia { get; set; } = null!;
        }

        public class RegisterRequest
        {
            public string Nombre { get; set; } = null!;
            public string NombreUsuario { get; set; } = null!;
            public string Correo { get; set; } = null!;
            public string Contrasenia { get; set; } = null!;
            public string Telefono { get; set; } = null!;
            public DireccionRequest Direccion { get; set; } = null!;
        }

        public class DireccionRequest
        {
            public string Estado { get; set; } = null!;
            public string Municipio { get; set; } = null!;
            public string CodigoPostal { get; set; } = null!;
            public string Colonia { get; set; } = null!;
            public string Calle { get; set; } = null!;
            public string NumExt { get; set; } = null!;
            public string? NumInt { get; set; }
            public string? Referencia { get; set; }
        }
    }
}
