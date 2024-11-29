using System.Text.Json.Serialization;

namespace SalsasAPI.Models
{
    public class QuejaDTO
    {
        public int Id { get; set; }
        public string ? Contenido { get; set; }
        public DateTime FechaCreacion { get; set; }
        public string? Estado { get; set; }
        public int IdUsuario { get; set; }
        public string ? UsuarioNombre { get; set; }
        public string? UsuarioCorreo { get; set; }
        public string? UsuarioTelefono { get; set; }

        public string? UsuarioN { get; set; }

        public string ? Respuesta { get; set; }
        public DateTime? FechaRespuesta { get; set; }
        
        public List<SeguimientoDTO> Seguimientos { get; set; }
    }
}
