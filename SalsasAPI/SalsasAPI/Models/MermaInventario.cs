using System;
using System.Collections.Generic;

namespace SalsasAPI.Models;

public partial class MermaInventario
{
    public int IdMerma { get; set; }

    public double CantidadMerma { get; set; }

    public DateTime FechaMerma { get; set; }

    public int? IdMateriaPrima { get; set; }

    public int? IdDetalleMateriaPrima { get; set; }

    public virtual DetalleMateriaPrima? IdDetalleMateriaPrimaNavigation { get; set; }

    public virtual MateriaPrima? IdMateriaPrimaNavigation { get; set; }
}
