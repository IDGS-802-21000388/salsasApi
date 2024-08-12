using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace SalsasAPI.Models;

public partial class DetalleVentum
{
    public int IdDetalleVenta { get; set; }

    public double Cantidad { get; set; }

    public double Subtotal { get; set; }

    public int? IdVenta { get; set; }

    public int? IdProducto { get; set; }
    [JsonIgnore]

    public virtual Producto? IdProductoNavigation { get; set; }
    [JsonIgnore]

    public virtual Ventum? IdVentaNavigation { get; set; }
}
