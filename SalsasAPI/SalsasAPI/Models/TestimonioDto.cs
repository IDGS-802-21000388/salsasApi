using System.ComponentModel.DataAnnotations;

namespace SalsasAPI.Models
{
    public class TestimonioDto
    {

        public int IdUsuario { get; set; }
        public int IdProducto { get; set; }
        public string Comentario { get; set; }

        [Range(1, 5, ErrorMessage = "La calificación debe estar entre 1 y 5.")]
        public int Calificacion { get; set; }  // Nueva propiedad

    }
}
