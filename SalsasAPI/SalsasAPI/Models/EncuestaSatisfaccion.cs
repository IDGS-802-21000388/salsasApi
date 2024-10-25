using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace SalsasAPI.Models
{
    public partial class EncuestaSatisfaccion
    {
        [Key] // Define como clave primaria
        public int IdEncuesta { get; set; }

        [Required(ErrorMessage = "El IdUsuario es obligatorio.")]
        public int IdUsuario { get; set; }

        [Required(ErrorMessage = "El IdVenta es obligatorio.")]
        public int IdVenta { get; set; }

        [Range(1, 5, ErrorMessage = "La calificación del proceso de compra debe estar entre 1 y 5.")]
        public int ProcesoCompra { get; set; }

        [Range(1, 5, ErrorMessage = "La calificación del sabor del producto debe estar entre 1 y 5.")]
        public int SaborProducto { get; set; }

        [Range(1, 5, ErrorMessage = "La calificación de la entrega del producto debe estar entre 1 y 5.")]
        public int EntregaProducto { get; set; }

        [Range(1, 5, ErrorMessage = "La calificación de la presentación del producto debe estar entre 1 y 5.")]
        public int PresentacionProducto { get; set; }

        [Range(1, 5, ErrorMessage = "La calificación de la facilidad de uso de la página debe estar entre 1 y 5.")]
        public int FacilidadUsoPagina { get; set; }

        public DateTime? FechaEncuesta { get; set; } 

        // Propiedades de navegación
        [JsonIgnore] // Si no quieres que se serialicen en la respuesta JSON
        public virtual Usuario? Usuario { get; set; } // Relación con la entidad Usuario

        [JsonIgnore] // Si no quieres que se serialicen en la respuesta JSON
        public virtual Ventum? Venta { get; set; } // Relación con la entidad Venta
    }
}
