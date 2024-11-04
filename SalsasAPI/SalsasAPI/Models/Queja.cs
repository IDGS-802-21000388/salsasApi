using System;
using System.Text.Json.Serialization;

namespace SalsasAPI.Models
{
    public class Queja
    {
        public int Id { get; set; }
        public string ? Contenido { get; set; }
        public DateTime FechaCreacion { get; set; }
        public string ?Estado { get; set; } // Nueva, En proceso, Resuelta
        public int IdUsuario { get; set; }
        [JsonIgnore]
        public Usuario ? Usuario { get; set; } // Relación con el usuario
        public string ? Respuesta { get; set; } // Respuesta del administrador
        public DateTime? FechaRespuesta { get; set; } // Fecha de la respuesta
    }
}