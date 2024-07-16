using System;
using System.Collections.Generic;

namespace SalsasAPI.Models;

public partial class Movimiento
{
    public int IdMovimiento { get; set; }

    public DateTime FechaMovimiento { get; set; }

    public string TipoMovimiento { get; set; } = null!;

    public double Monto { get; set; }

    public int? IdVenta { get; set; }

    public int? IdMateriaPrima { get; set; }

    public virtual MateriaPrima? IdMateriaPrimaNavigation { get; set; }

    public virtual Ventum? IdVentaNavigation { get; set; }
}
