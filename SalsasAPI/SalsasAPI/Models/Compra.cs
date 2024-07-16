using System;
using System.Collections.Generic;

namespace SalsasAPI.Models;

public partial class Compra
{
    public int IdCompra { get; set; }

    public int? IdMateriaPrima { get; set; }

    public int? IdDetalleMateriaPrima { get; set; }

    public double CantidadExistentes { get; set; }

    public virtual DetalleMateriaPrima? IdDetalleMateriaPrimaNavigation { get; set; }

    public virtual MateriaPrima? IdMateriaPrimaNavigation { get; set; }
}
