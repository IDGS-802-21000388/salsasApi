using System;
using System.Collections.Generic;

namespace SalsasAPI.Models;

public partial class DetalleVentum
{
    public int IdDetalleVenta { get; set; }

    public double Cantidad { get; set; }

    public double Subtotal { get; set; }

    public int? IdVenta { get; set; }

    public int? IdProducto { get; set; }

    public int? IdMedida { get; set; }

    public virtual Medidum? IdMedidaNavigation { get; set; }

    public virtual Producto? IdProductoNavigation { get; set; }

    public virtual Ventum? IdVentaNavigation { get; set; }
}
