// Services/EmailService.cs
using MailKit.Net.Smtp;
using MimeKit;
using System.Threading.Tasks;

// Clase para el envío de correos
public class EmailService
{
    public async Task EnviarCorreo(List<string> destinatarios, string mensaje)
    {
        var email = new MimeMessage();
        email.From.Add(new MailboxAddress("Salsas Reni","julian.rodriguez.villalobos@gmail.com"));

        foreach (var destinatario in destinatarios)
        {
            email.To.Add(new MailboxAddress(destinatario, destinatario));
        }

        email.Subject = "Respuesta a su queja";
        email.Body = new TextPart("plain")
        {
            Text = mensaje
        };

        using (var cliente = new SmtpClient())
        {
            await cliente.ConnectAsync("smtp.gmail.com", 587, MailKit.Security.SecureSocketOptions.StartTls);
            await cliente.AuthenticateAsync("julian.rodriguez.villalobos@gmail.com", "qyrp ddwh alft qiei"); // Usa la contraseña de aplicación si es necesario
            await cliente.SendAsync(email);
            await cliente.DisconnectAsync(true);
        }
    }
}
