using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace SalsasAPI.Models
{
    public partial class Ventum
    {
        public int IdVenta { get; set; }

        public DateTime FechaVenta { get; set; }

        public double Total { get; set; }
        public int? IdUsuario { get; set; }

        [JsonIgnore]
    public virtual ICollection<DetalleVentum> DetalleVenta { get; set; } = new List<DetalleVentum>();
    [JsonIgnore]
    public virtual ICollection<Envio> Envios { get; set; } = new List<Envio>();
    [JsonIgnore]
    public virtual ICollection<Movimiento> Movimientos { get; set; } = new List<Movimiento>();
    [JsonIgnore]
    public virtual ICollection<Pago> Pagos { get; set; } = new List<Pago>();
    [JsonIgnore]
    public virtual Usuario? IdUsuarioNavigation { get; set; }
    [JsonIgnore]
    public virtual ICollection<SolicitudProduccion> SolicitudProduccions { get; set; } = new List<SolicitudProduccion>();
    }
}
    