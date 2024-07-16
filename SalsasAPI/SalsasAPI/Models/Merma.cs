using System;
using System.Collections.Generic;

namespace SalsasAPI.Models;

public partial class Merma
{
    public int IdMerma { get; set; }

    public double CantidadMerma { get; set; }

    public DateTime? FechaMerma { get; set; }

    public string Descripcion { get; set; } = null!;

    public int? IdProducto { get; set; }

    public int? IdDetalleProducto { get; set; }

    public virtual DetalleProducto? IdDetalleProductoNavigation { get; set; }

    public virtual Producto? IdProductoNavigation { get; set; }
}
