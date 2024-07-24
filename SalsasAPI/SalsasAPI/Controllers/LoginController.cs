using Microsoft.AspNetCore.Mvc;
using SalsasAPI.Models;
using System.Linq;

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
        public IActionResult Register([FromBody] RegisterRequest registerRequest)
        {
            var user = new Usuario
            {
                Nombre = registerRequest.Nombre,
                NombreUsuario = registerRequest.NombreUsuario,
                Correo = registerRequest.Correo,
                Contrasenia = registerRequest.Contrasenia,
                Telefono = registerRequest.Telefono,
                Rol = "cliente",
                Estatus = 1,
                Intentos = 0
            };

            _context.Usuarios.Add(user);
            _context.SaveChanges();

            return Ok(new { Message = "User registered successfully", User = user });
        }
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
    }
}
