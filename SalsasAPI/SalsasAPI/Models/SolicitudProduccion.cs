  using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace SalsasAPI.Models;

public partial class SolicitudProduccion
{
    public int IdSolicitud { get; set; }

    public int CantidadProduccion { get; set; }

    public DateTime FechaSolicitud { get; set; }

    public int Estatus { get; set; }

    public int? IdProducto { get; set; }

    public int? IdUsuario { get; set; }
    [JsonIgnore]
    public virtual ICollection<DetalleSolicitud> DetalleSolicituds { get; set; } = new List<DetalleSolicitud>();
    [JsonIgnore]
    public virtual Producto? IdProductoNavigation { get; set; }
    [JsonIgnore]
    public virtual Usuario? IdUsuarioNavigation { get; set; }
}
