using SalsasAPI.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

public class Testimonio
{
    public int IdTestimonio { get; set; }

    [Required]
    public int IdUsuario { get; set; }

    [Required]
    public int IdProducto { get; set; }

    [Required]
    public string Comentario { get; set; }

    [Required]
    [Range(1, 5, ErrorMessage = "La calificación debe estar entre 1 y 5.")]
    public int Calificacion { get; set; }  // Nueva propiedad de calificación

    public DateTime FechaTestimonio { get; set; }

    public int Estatus { get; set; }

    // Relación con Usuario y Producto
    [JsonIgnore]
    [ForeignKey("IdUsuario")]
    public virtual Usuario Usuario { get; set; }

    [JsonIgnore]
    [ForeignKey("IdProducto")]
    public virtual Producto Producto { get; set; }
}
