using System;
using System.Text.Json.Serialization;

namespace SalsasAPI.Models
{
    public partial class EmailMessage
    {
        public int Id { get; set; }

        public string Email { get; set; } = null!;

        public string Mensaje { get; set; } = null!;

        public DateTime FechaCreacion { get; set; } = DateTime.Now;
    }
}
