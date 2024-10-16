using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace SalsasAPI.Models
{
    public partial class EncuestaSatisfaccion
    {
        public int IdEncuesta { get; set; }

        public int IdUsuario { get; set; }

        public int IdVenta { get; set; }

        public byte ProcesoCompra { get; set; }

        public byte SaborProducto { get; set; }

        public byte EntregaProducto { get; set; }

        public byte PresentacionProducto { get; set; }

        public byte FacilidadUsoPagina { get; set; }

        public DateTime FechaEncuesta { get; set; } = DateTime.Now;

        [JsonIgnore]
        public virtual Usuario Usuario { get; set; } = null!;

        [JsonIgnore]
        public virtual Ventum Venta { get; set; } = null!;
    }
}
