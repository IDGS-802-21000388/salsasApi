using System.Text.Json.Serialization;

namespace SalsasAPI.Models;

public partial class UsuarioCodigoDescuento
{
    public int IdUsuarioCodigo { get; set; }

    public int IdUsuario { get; set; } // ID del usuario que recibe el código

    public int IdCodigo { get; set; } // ID del código de descuento asignado

    public DateTime FechaAsignacion { get; set; } = DateTime.Now; // Fecha de asignación del código

    public bool Usado { get; set; } = false; // Indicador de si el código fue usado

    // Relación con Usuario
    [JsonIgnore]
    public virtual Usuario Usuario { get; set; } = null!;

    // Relación con CodigoDescuento
    [JsonIgnore]
    public virtual CodigoDescuento CodigoDescuento { get; set; } = null!;
}
