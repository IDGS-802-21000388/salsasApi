using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SalsasAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MailKit.Net.Smtp;
using MimeKit;

namespace SalsasAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PromoPorTipoController : Controller
    {
        private readonly SalsaContext _context;

        public PromoPorTipoController(SalsaContext context)
        {
            _context = context;
        }

        // Endpoint para enviar correos promocionales y guardar en la base de datos
        [HttpPost("enviar-promocion")]
        public async Task<IActionResult> EnviarPromocion([FromBody] PromoRequest promoRequest)
        {
            var emailService = new EmailService();
            await emailService.EnviarCorreo(promoRequest.Emails, promoRequest.Mensaje);

            // Guardar cada correo en la base de datos
            foreach (var destinatario in promoRequest.Emails)
            {
                var emailMessage = new EmailMessage
                {
                    Email = destinatario,
                    Mensaje = promoRequest.Mensaje,
                    FechaCreacion = DateTime.Now
                };

                _context.EmailMessages.Add(emailMessage);
            }

            await _context.SaveChangesAsync();

            return Ok(new { Message = "Promociones enviadas y guardadas correctamente" });
        }

        [HttpPost("enviar-ticket")]
        public async Task<IActionResult> EnviarTicket([FromBody] PromoRequest promoRequest)
        {
            var emailService = new EmailService();
            await emailService.EnviarCorreo(promoRequest.Emails, promoRequest.Mensaje);

            // Guardar cada correo en la base de datos
            foreach (var destinatario in promoRequest.Emails)
            {
                var emailMessage = new EmailMessage
                {
                    Email = destinatario,
                    Mensaje = "Ticket enviado a " + destinatario,
                    FechaCreacion = DateTime.Now
                };

                _context.EmailMessages.Add(emailMessage);
            }

            await _context.SaveChangesAsync();

            return Ok(new { Message = "Promociones enviadas y guardadas correctamente" });
        }

        // Obtener todos los registros de EmailMessage
        [HttpGet("getContactClient")]
        public async Task<IActionResult> GetContactClient()
        {
            var contacts = await _context.EmailMessages.ToListAsync();
            return Ok(contacts);
        }

        // Obtener registros de EmailMessage filtrados por email
        [HttpGet("getContactClientByEmail")]
        public async Task<IActionResult> GetContactClientByEmail([FromQuery] string email)
        {
            var contacts = await _context.EmailMessages
                                         .Where(e => e.Email == email)
                                         .ToListAsync();
            return Ok(contacts);
        }

        // Clase para el envío de correos
        public class EmailService
        {
            public async Task EnviarCorreo(List<string> destinatarios, string mensaje)
            {
                var email = new MimeMessage();
                email.From.Add(new MailboxAddress("Salsas Reni", "tuemail@gmail.com"));

                foreach (var destinatario in destinatarios)
                {
                    email.To.Add(new MailboxAddress(destinatario, destinatario));
                }

                email.Subject = "Promociones de Salsas Reni";
                email.Body = new TextPart("html")
                {
                    Text = mensaje
                };

                using (var smtp = new MailKit.Net.Smtp.SmtpClient())
                {
                    smtp.Connect("smtp.gmail.com", 587, false);
                    smtp.Authenticate("angeltovar308@gmail.com", "aywh ijrz pmzg mkto"); // Usa la contraseña de aplicación si es necesario
                    await smtp.SendAsync(email);
                    smtp.Disconnect(true);
                }
            }
        }

        // Clase que representa la solicitud desde el Frontend
        public class PromoRequest
        {
            public List<string> Emails { get; set; } = new List<string>();
            public string Mensaje { get; set; } = string.Empty;
        }
    }
}
