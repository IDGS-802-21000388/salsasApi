﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SalsasAPI.Models;
using SalsasAPI.Services;

namespace SalsasAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SeguimientoQuejasController : ControllerBase
    {
        private readonly SalsaContext _context;
        private readonly EmailService _emailService;
        private readonly ILogger<SeguimientoQuejasController> _logger;

        public SeguimientoQuejasController(SalsaContext context, EmailService emailService, ILogger<SeguimientoQuejasController> logger)
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
                var quejas = await _context.QuejasV2
                    .Include(q => q.Usuario)
                    .Include(q => q.Seguimientos)
                    .Select(q => new QuejaDTO
                    {
                        Id = q.Id,
                        Contenido = q.Contenido,
                        FechaCreacion = q.FechaCreacion,
                        Estado = q.Estado,
                        IdUsuario = q.IdUsuario,
                        UsuarioNombre = q.Usuario.NombreUsuario,
                        UsuarioCorreo = q.Usuario.Correo,
                        UsuarioTelefono = q.Usuario.Telefono,
                        UsuarioN=q.Usuario.Nombre,
                        Seguimientos = q.Seguimientos.Select(s => new SeguimientoDTO
                        {
                            Id = s.IdSeguimiento,
                            IdQueja = s.IdQueja,
                            Accion = s.Accion,
                            Comentario = s.Comentario,
                            FechaAccion = s.FechaAccion,
                        }).OrderByDescending(s => s.FechaAccion).ToList()
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

        [HttpPost("{id}/seguimiento")]
        public async Task<IActionResult> AgregarSeguimiento(int id, [FromBody] SeguimientoDTO seguimientoDTO)
        {
            // Buscar la queja con sus seguimientos y el usuario
            var queja = await _context.QuejasV2
                .Include(q => q.Seguimientos)
                .Include(q => q.Usuario)  // Incluye el usuario para asegurarse de que el correo esté cargado
                .FirstOrDefaultAsync(q => q.Id == id);

            if (queja == null)
            {
                return NotFound("No se encontró la queja.");
            }

            // Verificar que el usuario tenga correo asociado
            if (queja.Usuario?.Correo == null)
            {
                return BadRequest("El usuario no tiene un correo asociado.");
            }

            // Crear el nuevo seguimiento
            var nuevoSeguimiento = new SeguimientoQueja
            {
                IdQueja = id,
                Accion = seguimientoDTO.Accion,
                Comentario = seguimientoDTO.Comentario,
                FechaAccion = DateTime.Now,
            };

            _context.SeguimientoQueja.Add(nuevoSeguimiento);

            // Cambiar el estado de la queja a "En Proceso"
            queja.Estado = "En Proceso";

            // Guardar los cambios
            await _context.SaveChangesAsync();

            // Enviar correo al usuario con los detalles del seguimiento
            var destinatarios = new List<string> { queja.Usuario.Correo };
            var mensaje = $"Estimado/a {queja.Usuario.Nombre},\n\nSe ha agregado un nuevo seguimiento a su queja o comentario:\nAcción: {seguimientoDTO.Accion}\nComentario: {seguimientoDTO.Comentario}\n\nSaludos,\nSalsas Reni";

            await _emailService.EnviarCorreo(destinatarios, mensaje);

            return Ok(nuevoSeguimiento);
        }






        [HttpPost("{id}/respuesta")]
        public async Task<IActionResult> ResponderQueja(int id, [FromBody] string respuesta)
        {
            var queja = await _context.QuejasV2.Include(q => q.Usuario).FirstOrDefaultAsync(q => q.Id == id);
            if (queja == null) return NotFound();

            
            queja.Estado = "Resuelta";

            var nuevoSeguimiento = new SeguimientoQueja
            {
                IdQueja = id,
                Accion = "Respuesta enviada",
                Comentario = respuesta,
                FechaAccion = DateTime.Now
            };
            _context.SeguimientoQueja.Add(nuevoSeguimiento);

            await _context.SaveChangesAsync();

            if (queja.Usuario?.Correo == null) return BadRequest("El usuario no tiene un correo asociado.");

            var destinatarios = new List<string> { queja.Usuario.Correo };
            var mensaje = $"Estimado/a {queja.Usuario.Nombre},\n\nSu queja ha sido respondida:\n{respuesta}\n\nSaludos,\nSalsas Reni";

            await _emailService.EnviarCorreo(destinatarios, mensaje);

            return NoContent();
        }


        [HttpPost("{id}/finalizar")]
        public async Task<IActionResult> FinalizarQueja(int id)
        {
            var queja = await _context.QuejasV2.FirstOrDefaultAsync(q => q.Id == id);
            if (queja == null) return NotFound("No se encontró la queja.");

            if (queja.Estado == "Resuelta")
                return BadRequest("La queja ya está marcada como Resuelta.");

            queja.Estado = "Resuelta";

            await _context.SaveChangesAsync();
            return Ok(new { Message = "La queja ha sido marcada como Resuelta." });
        }

    }
}