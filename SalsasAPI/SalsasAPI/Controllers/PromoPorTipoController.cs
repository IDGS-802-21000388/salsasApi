using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SalsasAPI.Models;
using System;
using System.Collections.Generic;
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

        // Endpoint para enviar correos promocionales
        [HttpPost("enviar-promocion")]
        public async Task<IActionResult> EnviarPromocion([FromBody] PromoRequest promoRequest)
        {
            var emailService = new EmailService();
            await emailService.EnviarCorreo(promoRequest.Emails, promoRequest.Mensaje);
            return Ok(new { Message = "Promociones enviadas correctamente" });
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
                email.Body = new TextPart("plain")
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
