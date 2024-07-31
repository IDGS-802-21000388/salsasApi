using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace SalsasAPI.Models;

public partial class Merma
{
    public int IdMerma { get; set; }

    public double CantidadMerma { get; set; }

    public DateTime? FechaMerma { get; set; }

    public string Descripcion { get; set; } = null!;

    public int? IdProducto { get; set; }

    public int? IdDetalleProducto { get; set; }
    [JsonIgnore]
    public virtual DetalleProducto? IdDetalleProductoNavigation { get; set; }
    [JsonIgnore]
    public virtual Producto? IdProductoNavigation { get; set; }
}
