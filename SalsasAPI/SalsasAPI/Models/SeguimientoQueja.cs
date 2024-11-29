using System.Text.Json.Serialization;

namespace SalsasAPI.Models
{
    public class SeguimientoQueja
    {
        public int IdSeguimiento { get; set; }
        public int IdQueja { get; set; }
        public DateTime FechaAccion { get; set; } = DateTime.Now;
        public string Accion { get; set; }
        public string? Comentario { get; set; }
        

        // Relaciones
        public QuejasV2 Queja { get; set; } // Relación con QuejasV2
       
    }


    public class SeguimientoDTO
    {
        public int Id { get; set; }
        public int IdQueja { get; set; }
        public string Accion { get; set; }
        public string ? Comentario { get; set; }
        public DateTime FechaAccion { get; set; }
 
    }
}
