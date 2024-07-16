using System;
using System.Collections.Generic;

namespace SalsasAPI.Models;

public partial class Recetum
{
    public int IdReceta { get; set; }

    public int? IdMedida { get; set; }

    public int? IdProducto { get; set; }

    public virtual ICollection<DetalleRecetum> DetalleReceta { get; set; } = new List<DetalleRecetum>();

    public virtual Medidum? IdMedidaNavigation { get; set; }

    public virtual Producto? IdProductoNavigation { get; set; }
}
