using System;
using System.Collections.Generic;

namespace SalsasAPI.Models;

public partial class MateriaPrima
{
    public int IdMateriaPrima { get; set; }

    public string NombreMateria { get; set; } = null!;

    public double PrecioCompra { get; set; }

    public double Cantidad { get; set; }

    public int? IdMedida { get; set; }

    public int? IdProveedor { get; set; }

    public virtual ICollection<Compra> Compras { get; set; } = new List<Compra>();

    public virtual ICollection<DetalleMateriaPrima> DetalleMateriaPrimas { get; set; } = new List<DetalleMateriaPrima>();

    public virtual ICollection<DetalleRecetum> DetalleReceta { get; set; } = new List<DetalleRecetum>();

    public virtual Medidum? IdMedidaNavigation { get; set; }

    public virtual Proveedor? IdProveedorNavigation { get; set; }

    public virtual ICollection<MermaInventario> MermaInventarios { get; set; } = new List<MermaInventario>();

    public virtual ICollection<Movimiento> Movimientos { get; set; } = new List<Movimiento>();
}
