using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace SalsasAPI.Models
{
    public class QuejasV2
    {
        
        public int Id { get; set; }
        public string Contenido { get; set; }
        public DateTime FechaCreacion { get; set; }
        public string Estado { get; set; }

        // Relación con Usuario
        public int IdUsuario { get; set; }
        [JsonIgnore]
        public Usuario ? Usuario { get; set; }

        // Relación con SeguimientoQueja
        [JsonIgnore]
        public ICollection<SeguimientoQueja> Seguimientos { get; set; } = new List<SeguimientoQueja>();
    }
}
