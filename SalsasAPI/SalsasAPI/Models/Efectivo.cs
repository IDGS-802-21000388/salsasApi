using System;
using System.Collections.Generic;

namespace SalsasAPI.Models;

public partial class Efectivo
{
    public int IdEfectivo { get; set; }

    public double MontoRecibido { get; set; }

    public double CambioDevuelto { get; set; }

    public int? IdPago { get; set; }

    public virtual Pago? IdPagoNavigation { get; set; }
}
