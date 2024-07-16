using System;
using System.Collections.Generic;

namespace SalsasAPI.Models;

public partial class Envio
{
    public int IdEnvio { get; set; }

    public DateTime FechaEnvio { get; set; }

    public DateTime? FechaEntregaEstimada { get; set; }

    public DateTime? FechaEntregaReal { get; set; }

    public string Estatus { get; set; } = null!;

    public int? IdVenta { get; set; }

    public int? IdPaqueteria { get; set; }

    public virtual Paqueterium? IdPaqueteriaNavigation { get; set; }

    public virtual Ventum? IdVentaNavigation { get; set; }
}
