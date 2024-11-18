using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MailKit.Net.Smtp;
using MimeKit;
using MailKit.Security;
using SalsasAPI.Models;
using Microsoft.EntityFrameworkCore; // Asegúrate de que el namespace corresponda al contexto de tu base de datos

namespace SalsasAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CotizacionController : ControllerBase
    {
        private readonly SalsaContext _context;

        public CotizacionController(SalsaContext context)
        {
            _context = context;
        }

        // Endpoint para enviar la cotización por correo y guardar en la base de datos
        [HttpPost("enviar-cotizacion")]
        public async Task<IActionResult> EnviarCotizacion([FromBody] CotizacionRequest cotizacionRequest)
        {
            try
            {
                // Calcular subtotal e IVA
                decimal subtotal = 0;
                foreach (var item in cotizacionRequest.Items)
                {
                    subtotal += item.Cantidad * item.PrecioUnitario;
                }
                decimal iva = subtotal * 0.16m;

                // Usar el total con descuento recibido desde el frontend
                decimal totalConDescuento = cotizacionRequest.TotalConDescuento;

                // Crear y guardar la cotización en la base de datos
                var nuevaCotizacion = new Cotizaciones
                {
                    IdUsuario = cotizacionRequest.IdUsuario,
                    emailCliente = cotizacionRequest.Email,
                    FechaCreacion = DateTime.Now,
                    Subtotal = subtotal,
                    Iva = iva,
                    Total = totalConDescuento // Guardar el total con descuento
                };

                _context.Cotizaciones.Add(nuevaCotizacion);
                await _context.SaveChangesAsync(); // Guardar para obtener el ID de la cotización

                // Crear y guardar los detalles de la cotización
                foreach (var item in cotizacionRequest.Items)
                {
                    var detalleCotizacion = new DetalleCotizacion
                    {
                        IdCotizacion = nuevaCotizacion.IdCotizacion,
                        Descripcion = item.NombreProducto,
                        Cantidad = item.Cantidad,
                        PrecioUnitario = item.PrecioUnitario,
                        Total = item.Cantidad * item.PrecioUnitario
                    };
                    _context.DetalleCotizaciones.Add(detalleCotizacion);
                }
                await _context.SaveChangesAsync();

                // Enviar el correo
                string mensaje = GenerarMensajeCotizacion(cotizacionRequest, subtotal, iva, totalConDescuento);
                var emailService = new EmailService();
                await emailService.EnviarCorreo(cotizacionRequest.Email, mensaje);

                return Ok(new { Message = "Cotización enviada y guardada correctamente" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Error = $"Error al procesar la cotización: {ex.Message}" });
            }
        }

        [HttpGet("obtener-todas")]
        public async Task<IActionResult> ObtenerTodas()
        {
            try
            {
                var cotizaciones = await _context.VistaCotizaciones.ToListAsync();
                return Ok(cotizaciones);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Error = $"Error al obtener las cotizaciones: {ex.Message}" });
            }
        }
        [HttpGet("obtener-por-usuario/{idUsuario}")]
        public async Task<IActionResult> ObtenerPorUsuario(int idUsuario)
        {
            try
            {
                var cotizaciones = await _context.VistaCotizaciones
                    .Where(c => c.IdUsuario == idUsuario)
                    .ToListAsync();
                return Ok(cotizaciones);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Error = $"Error al obtener las cotizaciones para el usuario {idUsuario}: {ex.Message}" });
            }
        }

        [HttpPut("actualizar-atendida/{idCotizacion}")]
        public async Task<IActionResult> ActualizarAtendida(int idCotizacion)
        {
            try
            {
                // Buscar la cotización por ID
                var cotizacion = await _context.Cotizaciones.FindAsync(idCotizacion);
                if (cotizacion == null)
                {
                    return NotFound(new { Message = "Cotización no encontrada" });
                }

                // Actualizar el estado de "Atendida"
                cotizacion.Atendida = 1;
                await _context.SaveChangesAsync();

                return Ok(new { Message = "Estado de la cotización actualizado a 'Atendida'" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Error = $"Error al actualizar la cotización: {ex.Message}" });
            }
        }


        // Método para generar el cuerpo del mensaje
        private string GenerarMensajeCotizacion(CotizacionRequest cotizacionRequest, decimal subtotal, decimal iva, decimal totalConDescuento)
        {
            string mensaje = "Cotización de productos:\n\n";
            foreach (var item in cotizacionRequest.Items)
            {
                mensaje += $"{item.NombreProducto} - Cantidad: {item.Cantidad}, Precio Unitario: {item.PrecioUnitario:C}, Total: {item.Cantidad * item.PrecioUnitario:C}\n";
            }

            mensaje += $"\nSubtotal: {subtotal:C}\nIVA (16%): {iva:C}\nTotal (con descuento): {totalConDescuento:C}\n";
            mensaje += "\nGracias por su compra.";

            return mensaje;
        }

        // Clase para el servicio de envío de correos
        public class EmailService
        {
            public async Task EnviarCorreo(string destinatario, string mensaje)
            {
                var email = new MimeMessage();
                email.From.Add(new MailboxAddress("Salsas Reni", "julian.rodriguez.villalobos@gmail.com")); // Correo de envío
                email.To.Add(new MailboxAddress(destinatario, destinatario)); // Correo del destinatario
                email.Subject = "Cotización de Productos";
                email.Body = new TextPart("plain")
                {
                    Text = mensaje
                };

                using (var smtp = new SmtpClient())
                {
                    try
                    {
                        smtp.Connect("smtp.gmail.com", 587, SecureSocketOptions.StartTls);
                        smtp.Authenticate("julian.rodriguez.villalobos@gmail.com", "qyrp ddwh alft qiei"); // Contraseña de aplicación de Gmail
                        await smtp.SendAsync(email);
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("Error al enviar el correo: " + ex.Message);
                    }
                    finally
                    {
                        smtp.Disconnect(true);
                    }
                }
            }
        }

        // Clase que representa la solicitud de cotización desde el frontend
        public class CotizacionRequest
        {
            public int IdUsuario { get; set; } // Identificador del usuario
            public string Email { get; set; }
            public decimal TotalConDescuento { get; set; } // Nuevo campo para el total con descuento
            public List<CotizacionItem> Items { get; set; } = new List<CotizacionItem>();
        }

        // Clase que representa un producto en la cotización
        public class CotizacionItem
        {
            public string NombreProducto { get; set; }
            public decimal PrecioUnitario { get; set; }
            public int Cantidad { get; set; }
        }
    }
}
