using System.Text.Json.Serialization;
using System.Collections.Generic;

namespace SalsasAPI.Models
{
    public partial class CodigoDescuento
    {
        public int IdCodigo { get; set; }

        public string Codigo { get; set; } = null!; // Código único

        public string Descripcion { get; set; } = null!; // Descripción del código

        public int? DescuentoPorcentaje { get; set; } // Opcional, porcentaje de descuento

        public decimal? DescuentoMonto { get; set; } // Opcional, descuento fijo

        public DateTime FechaInicio { get; set; } // Inicio de validez

        public DateTime FechaFin { get; set; } // Fin de validez

        public int? CantidadMaxima { get; set; } // Número máximo de usos permitidos (0 = ilimitado)

        public int CantidadUsada { get; set; } // Número de veces que ha sido usado

        public bool Estatus { get; set; } = true; // Activo/Inactivo

        // Relación con UsuarioCodigoDescuento
        [JsonIgnore]
        public virtual ICollection<UsuarioCodigoDescuento> UsuarioCodigoDescuentos { get; set; } = new List<UsuarioCodigoDescuento>();
    }
}
