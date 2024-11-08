using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MailKit.Net.Smtp;
using MimeKit;
using MailKit.Security;

namespace SalsasAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CotizacionController : ControllerBase
    {
        // Endpoint para enviar la cotización por correo
        [HttpPost("enviar-cotizacion")]
        public async Task<IActionResult> EnviarCotizacion([FromBody] CotizacionRequest cotizacionRequest)
        {
            try
            {
                // Crear el cuerpo del mensaje con los detalles de la cotización
                string mensaje = GenerarMensajeCotizacion(cotizacionRequest);

                var emailService = new EmailService();
                await emailService.EnviarCorreo(cotizacionRequest.Email, mensaje);

                return Ok(new { Message = "Cotización enviada correctamente" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Error = $"Error al enviar el correo: {ex.Message}" });
            }
        }

        // Método para generar el cuerpo del mensaje
        private string GenerarMensajeCotizacion(CotizacionRequest cotizacionRequest)
        {
            string mensaje = "Cotización de productos:\n\n";
            decimal subtotal = 0;
            foreach (var item in cotizacionRequest.Items)
            {
                mensaje += $"{item.NombreProducto} - Cantidad: {item.Cantidad}, Precio Unitario: {item.PrecioUnitario:C}, Total: {item.Cantidad * item.PrecioUnitario:C}\n";
                subtotal += item.Cantidad * item.PrecioUnitario;
            }

            decimal iva = subtotal * 0.16m;
            decimal total = subtotal + iva;

            mensaje += $"\nSubtotal: {subtotal:C}\nIVA (16%): {iva:C}\nTotal: {total:C}\n";
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
            public string Email { get; set; }
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
