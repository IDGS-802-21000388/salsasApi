using System;
using System.Collections.Generic;

namespace SalsasAPI.Models;

public partial class DetalleProducto
{
    public int IdDetalleProducto { get; set; }

    public DateTime? FechaVencimiento { get; set; }

    public int CantidadExistentes { get; set; }

    public bool Estatus { get; set; }

    public int? IdProducto { get; set; }

    public virtual Producto? IdProductoNavigation { get; set; }

    public virtual ICollection<Merma> Mermas { get; set; } = new List<Merma>();
}
