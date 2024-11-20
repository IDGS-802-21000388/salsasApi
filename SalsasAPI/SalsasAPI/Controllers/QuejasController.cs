using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SalsasAPI.Models;
using SalsasAPI.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalsasAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuejasController : ControllerBase
    {
        private readonly SalsaContext _context;
        private readonly EmailService _emailService;
        private readonly ILogger<QuejasController> _logger;

        public QuejasController(SalsaContext context, EmailService emailService, ILogger<QuejasController> logger)
        {
            _context = context;
            _emailService = emailService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<QuejaDTO>>> GetQuejas()
        {
            try
            {
                var quejas = await _context.Quejas
                    .Include(q => q.Usuario)
                    .Select(q => new QuejaDTO
                    {
                        Id = q.Id,
                        Contenido = q.Contenido,
                        FechaCreacion = q.FechaCreacion,
                        Estado = q.Estado,
                        IdUsuario = q.IdUsuario,
                        UsuarioNombre = q.Usuario.NombreUsuario,
                        UsuarioCorreo = q.Usuario.Correo,
                        Respuesta = q.Respuesta,
                        FechaRespuesta = q.FechaRespuesta
                    })
                    .ToListAsync();

                return Ok(quejas);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener las quejas");
                return StatusCode(500, "Ocurrió un error al procesar tu solicitud.");
            }
        }


        [HttpPost("{id}/respuesta")]
        public async Task<IActionResult> ResponderQueja(int id, [FromBody] string respuesta)
        {
            var queja = await _context.Quejas.Include(q => q.Usuario).FirstOrDefaultAsync(q => q.Id == id);
            if (queja == null)
            {
                return NotFound();
            }

            queja.Respuesta = respuesta;
            queja.FechaRespuesta = DateTime.Now;
            queja.Estado = "Resuelta"; // Cambia el estado a Resuelta

            await _context.SaveChangesAsync();

            // Enviar correo al usuario
            if (queja.Usuario?.Correo == null)
            {
                return BadRequest("El usuario no tiene un correo asociado.");
            }

            var destinatarios = new List<string> { queja.Usuario.Correo };
            var mensaje = $"Estimado/a {queja.Usuario.Nombre},\n\nSu queja ha sido respondida:\n{respuesta}\n\nSaludos,\nSalsas Reni";

            await _emailService.EnviarCorreo(destinatarios, mensaje);

            return NoContent();
        }
    }
}
