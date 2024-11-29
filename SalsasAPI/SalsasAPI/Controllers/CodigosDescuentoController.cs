using MailKit.Security;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MimeKit;
using SalsasAPI.Models;
using MailKit.Net.Smtp;

namespace SalsasAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CodigosDescuentoController : ControllerBase
    {
        private readonly SalsaContext _context;

        public CodigosDescuentoController(SalsaContext context)
        {
            _context = context;
        }

        // 1. Crear un nuevo código de descuento
        [HttpPost]
        public async Task<IActionResult> CrearCodigo([FromBody] CodigoDescuento codigoDescuento)
        {
            if (string.IsNullOrWhiteSpace(codigoDescuento.Codigo))
            {
                return BadRequest("El código de descuento es obligatorio.");
            }

            if (codigoDescuento.DescuentoPorcentaje.HasValue && codigoDescuento.DescuentoMonto.HasValue)
            {
                return BadRequest("Solo puedes definir un porcentaje o un monto, no ambos.");
            }

            var existeCodigo = await _context.CodigosDescuento
                .AnyAsync(c => c.Codigo == codigoDescuento.Codigo);

            if (existeCodigo)
            {
                return Conflict("Ya existe un código de descuento con el mismo valor.");
            }

            _context.CodigosDescuento.Add(codigoDescuento);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(ObtenerCodigo), new { id = codigoDescuento.IdCodigo }, codigoDescuento);
        }

        // 2. Obtener un código de descuento por ID
        [HttpGet("{id}")]
        public async Task<IActionResult> ObtenerCodigo(int id)
        {
            var codigo = await _context.CodigosDescuento.FindAsync(id);

            if (codigo == null)
                return NotFound("Código no encontrado.");

            return Ok(codigo);
        }

        // 3. Asignar un código a usuarios específicos y enviar correos
        [HttpPost("AsignarCodigo")]
        public async Task<IActionResult> AsignarCodigo([FromBody] List<int> usuariosIds, [FromQuery] int idCodigo)
        {
            var codigo = await _context.CodigosDescuento.FindAsync(idCodigo);

            if (codigo == null || !codigo.Estatus)
                return BadRequest("El código no existe o no está activo.");

            // Obtener los datos de los usuarios
            var usuarios = await _context.Usuarios
                .Where(u => usuariosIds.Contains(u.IdUsuario))
                .ToListAsync();

            foreach (var usuario in usuarios)
            {
                // Verificar si el usuario ya tiene el código asignado
                var existeAsignacion = await _context.UsuarioCodigoDescuento
                    .AnyAsync(ucd => ucd.IdUsuario == usuario.IdUsuario && ucd.IdCodigo == idCodigo);

                if (!existeAsignacion)
                {
                    var usuarioCodigo = new UsuarioCodigoDescuento
                    {
                        IdUsuario = usuario.IdUsuario,
                        IdCodigo = idCodigo,
                        FechaAsignacion = DateTime.Now
                    };

                    _context.UsuarioCodigoDescuento.Add(usuarioCodigo);

                    string mensajeHtml = GenerarMensajePromocionHtml(codigo, usuario);

                    try
                    {
                        var emailService = new EmailService();
                        await emailService.EnviarCorreoHtml(usuario.Correo, "Nueva Promoción Disponible", mensajeHtml, "images/logo_Salsa_Reni-removebg.png");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error al enviar correo a {usuario.Correo}: {ex.Message}");
                    }
                }
            }

            await _context.SaveChangesAsync();
            return Ok("Código asignado a los usuarios seleccionados y correos enviados.");
        }

        private string GenerarMensajePromocionHtml(CodigoDescuento codigo, Usuario usuario)
        {
            return $@"
            <html>
                <body style='font-family: Arial, sans-serif; line-height: 1.6; background-color: #f7f7f7; margin: 0; padding: 0;'>
                    <div style='max-width: 600px; margin: auto; background-color: #ffffff; padding: 20px; border: 1px solid #ddd; border-radius: 8px;'>
                        <div style='text-align: center; margin-bottom: 20px;'>
                            <img src='cid:logoReni' alt='Salsas Reni' style='width: 150px;' />
                        </div>
                        <h1 style='color: #c31a23; text-align: center;'>¡Hola {usuario.Nombre}!</h1>
                        <p style='text-align: center; color: #333;'>¡Tienes una nueva promoción disponible!</p>
                
                        <div style='background-color: #f9f9f9; margin-top: 20px; padding: 15px; border-radius: 8px; text-align: center;'>
                            <p style='font-weight: bold; color: #333;'>Descripción</p>
                            <p style='color: #555;'>{codigo.Descripcion}</p>
                        </div>

                        <div style='display: flex; justify-content: space-between; margin-top: 20px;'>
                            <div style='background-color: #ffe8e8; padding: 10px; border-radius: 8px; text-align: center; flex: 1; margin-right: 10px;'>
                                <p style='font-weight: bold; color: #c31a23;'>Código Promocional</p>
                                <p style='font-size: 18px; color: #333;'>{codigo.Codigo}</p>
                            </div>
                            <div style='background-color: #fff8e1; padding: 10px; border-radius: 8px; text-align: center; flex: 1; margin-left: 10px;'>
                                <p style='font-weight: bold; color: #c31a23;'>Descuento Especial</p>
                                <p style='font-size: 18px; color: #333;'>{(codigo.DescuentoPorcentaje.HasValue ? $"{codigo.DescuentoPorcentaje}% de descuento" : $"${codigo.DescuentoMonto} de descuento")}</p>
                            </div>
                        </div>

                        <div style='background-color: #f9f9f9; margin-top: 20px; padding: 15px; border-radius: 8px; text-align: center;'>
                            <p style='font-weight: bold; color: #333;'>Período de la Promoción</p>
                            <p style='color: #555;'>Válido desde: {codigo.FechaInicio:dd/MM/yyyy} hasta {codigo.FechaFin:dd/MM/yyyy}</p>
                        </div>

                        <div style='text-align: center; margin-top: 20px;'>
                            <a href='#' style='display: inline-block; background-color: #c31a23; color: #fff; text-decoration: none; padding: 10px 20px; border-radius: 8px; font-size: 16px; font-weight: bold;'>¡Aprovecha esta Oferta!</a>
                        </div>

                        <p style='text-align: center; color: #333; margin-top: 20px;'>¡No te lo pierdas!</p>
                        <p style='text-align: center; color: #555;'>Saludos,<br>El equipo de Salsas Reni</p>
                    </div>
                </body>
            </html>";
        }


        public class EmailService
        {
            public async Task EnviarCorreoHtml(string destinatario, string asunto, string mensajeHtml, string logoPath)
            {
                var email = new MimeMessage();
                email.From.Add(new MailboxAddress("Salsas Reni", "julian.rodriguez.villalobos@gmail.com"));
                email.To.Add(new MailboxAddress(destinatario, destinatario));
                email.Subject = asunto;

                var bodyBuilder = new BodyBuilder
                {
                    HtmlBody = mensajeHtml
                };

                var logo = bodyBuilder.LinkedResources.Add(logoPath);
                logo.ContentId = "logoReni";

                email.Body = bodyBuilder.ToMessageBody();

                using (var smtp = new SmtpClient())
                {
                    try
                    {
                        smtp.Connect("smtp.gmail.com", 587, SecureSocketOptions.StartTls);
                        smtp.Authenticate("julian.rodriguez.villalobos@gmail.com", "qyrp ddwh alft qiei");
                        await smtp.SendAsync(email);
                    }
                    finally
                    {
                        smtp.Disconnect(true);
                    }
                }
            }
        }


        // 4. Listar códigos asignados a un usuario
        [HttpGet("PorUsuario/{idUsuario}")]
        public async Task<IActionResult> ObtenerCodigosPorUsuario(int idUsuario)
        {
            var codigos = await _context.UsuarioCodigoDescuento
                .Include(ucd => ucd.CodigoDescuento)
                .Where(ucd => ucd.IdUsuario == idUsuario && !ucd.Usado)
                .Select(ucd => new
                {
                    ucd.IdUsuarioCodigo,
                    Codigo = ucd.CodigoDescuento.Codigo,
                    idCodigo = ucd.IdCodigo,
                    Descripcion = ucd.CodigoDescuento.Descripcion,
                    FechaInicio = ucd.CodigoDescuento.FechaInicio,
                    FechaFin = ucd.CodigoDescuento.FechaFin,
                    Usado = ucd.Usado
                })
                .ToListAsync();

            if (codigos.Count == 0)
                return NotFound("El usuario no tiene códigos asignados.");

            return Ok(codigos);
        }

        // 5. Validar si un código es válido para un usuario
        [HttpGet("ValidarCodigo")]
        public async Task<IActionResult> ValidarCodigo([FromQuery] int idUsuario, [FromQuery] string codigo)
        {
            var codigoDescuento = await _context.CodigosDescuento
                .FirstOrDefaultAsync(c => c.Codigo == codigo && c.Estatus &&
                                           c.FechaInicio <= DateTime.Now &&
                                           c.FechaFin >= DateTime.Now);

            if (codigoDescuento == null)
                return BadRequest("El código no existe o ha expirado.");

            var usuarioCodigo = await _context.UsuarioCodigoDescuento
                .FirstOrDefaultAsync(ucd => ucd.IdUsuario == idUsuario &&
                                             ucd.IdCodigo == codigoDescuento.IdCodigo &&
                                             !ucd.Usado);

            if (usuarioCodigo == null)
                return BadRequest("El usuario no tiene acceso a este código o ya lo ha usado.");

            return Ok(new
            {
                Valido = true,
                Descuento = codigoDescuento.DescuentoPorcentaje ?? codigoDescuento.DescuentoMonto
            });
        }

        // 6. Marcar un código como usado
        [HttpPost("MarcarUsado")]
        public async Task<IActionResult> MarcarCodigoUsado([FromQuery] int idUsuario, [FromQuery] int idCodigo)
        {
            var usuarioCodigo = await _context.UsuarioCodigoDescuento
                .FirstOrDefaultAsync(ucd => ucd.IdUsuario == idUsuario && ucd.IdCodigo == idCodigo);

            if (usuarioCodigo == null || usuarioCodigo.Usado)
                return BadRequest("El código ya fue usado o no está asignado a este usuario.");

            usuarioCodigo.Usado = true;
            await _context.SaveChangesAsync();

            return Ok("Código marcado como usado.");
        }

        // 7. Obtener todos los códigos
        [HttpGet]
        public async Task<IActionResult> ObtenerTodosCodigos()
        {
            var codigos = await _context.CodigosDescuento.ToListAsync();

            if (codigos == null || !codigos.Any())
                return NotFound("No hay códigos de descuento disponibles.");

            return Ok(codigos);
        }

        // 8. Actualizar o cambiar estatus de un código
        [HttpPut("{id}")]
        public async Task<IActionResult> ActualizarCodigo(int id, [FromBody] CodigoDescuento codigoDescuento)
        {
            if (id != codigoDescuento.IdCodigo)
            {
                return BadRequest("El ID del código no coincide.");
            }

            // Validar que solo uno de los dos campos esté configurado
            if (codigoDescuento.DescuentoPorcentaje.HasValue && codigoDescuento.DescuentoMonto.HasValue)
            {
                return BadRequest("Solo puedes definir un porcentaje o un monto, no ambos.");
            }

            var codigoExistente = await _context.CodigosDescuento.FindAsync(id);
            if (codigoExistente == null)
            {
                return NotFound("El código de descuento no existe.");
            }

            // Actualizar el código con los nuevos valores
            codigoExistente.Codigo = codigoDescuento.Codigo;
            codigoExistente.Descripcion = codigoDescuento.Descripcion;
            codigoExistente.DescuentoPorcentaje = codigoDescuento.DescuentoPorcentaje;
            codigoExistente.DescuentoMonto = codigoDescuento.DescuentoMonto;
            codigoExistente.FechaInicio = codigoDescuento.FechaInicio;
            codigoExistente.FechaFin = codigoDescuento.FechaFin;
            codigoExistente.Estatus = codigoDescuento.Estatus;

            _context.Entry(codigoExistente).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
                return Ok("Código actualizado correctamente.");
            }
            catch (DbUpdateConcurrencyException)
            {
                return StatusCode(500, "Error al actualizar el código.");
            }
        }

        // 9. Cambiar el estatus de un código (equivalente a eliminar)
        [HttpPatch("{id}/estatus")]
        public async Task<IActionResult> CambiarEstatusCodigo(int id, [FromQuery] bool estatus)
        {
            var codigoExistente = await _context.CodigosDescuento.FindAsync(id);
            if (codigoExistente == null)
            {
                return NotFound("El código de descuento no existe.");
            }

            codigoExistente.Estatus = estatus;
            _context.Entry(codigoExistente).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
                return Ok($"El estatus del código se ha cambiado a {(estatus ? "activo" : "inactivo")}.");
            }
            catch (DbUpdateConcurrencyException)
            {
                return StatusCode(500, "Error al cambiar el estatus del código.");
            }
        }

        // 10. Obtener usuarios con un código asignado
        [HttpGet("{idCodigo}/usuarios")]
        public async Task<IActionResult> ObtenerUsuariosConCodigo(int idCodigo)
        {
            var usuarios = await _context.UsuarioCodigoDescuento
                .Where(ucd => ucd.IdCodigo == idCodigo)
                .Select(ucd => ucd.IdUsuario)
                .ToListAsync();

            return Ok(usuarios);
        }

        //11. Obtener todos los datos de las tablas CodigosDescuento y UsuarioCodigoDescuento
        [HttpGet("ObtenerDatosTablas")]
        public async Task<IActionResult> ObtenerDatosTablas()
        {
            var codigos = await _context.CodigosDescuento.ToListAsync();
            var usuarioCodigos = await _context.UsuarioCodigoDescuento
                .Include(ucd => ucd.CodigoDescuento)
                .Include(ucd => ucd.Usuario)
                .ToListAsync();

            return Ok(new
            {
                CodigosDescuento = codigos,
                UsuarioCodigoDescuento = usuarioCodigos
            });
        }

        // 10. Modificar el estatus de un código o asignación
        [HttpPut("ModificarEstatus")]
        public async Task<IActionResult> ModificarEstatus([FromQuery] int idCodigo, [FromQuery] bool? nuevoEstatusCodigo, [FromQuery] int? idUsuarioCodigo, [FromQuery] bool? nuevoEstatusUsuarioCodigo)
        {
            if (idCodigo > 0 && nuevoEstatusCodigo.HasValue)
            {
                var codigo = await _context.CodigosDescuento.FindAsync(idCodigo);
                if (codigo == null)
                    return NotFound("Código de descuento no encontrado.");

                codigo.Estatus = nuevoEstatusCodigo.Value;
                _context.CodigosDescuento.Update(codigo);
            }

            if (idUsuarioCodigo > 0 && nuevoEstatusUsuarioCodigo.HasValue)
            {
                var usuarioCodigo = await _context.UsuarioCodigoDescuento.FindAsync(idUsuarioCodigo);
                if (usuarioCodigo == null)
                    return NotFound("Asignación de usuario no encontrada.");

                usuarioCodigo.Usado = nuevoEstatusUsuarioCodigo.Value;
                _context.UsuarioCodigoDescuento.Update(usuarioCodigo);
            }

            await _context.SaveChangesAsync();
            return Ok("Estatus actualizado correctamente.");
        }



    }
}
