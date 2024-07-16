using System;
using System.Collections.Generic;

namespace SalsasAPI.Models;

public partial class Ventum
{
    public int IdVenta { get; set; }

    public DateTime FechaVenta { get; set; }

    public double Total { get; set; }

    public virtual ICollection<DetalleVentum> DetalleVenta { get; set; } = new List<DetalleVentum>();

    public virtual ICollection<Envio> Envios { get; set; } = new List<Envio>();

    public virtual ICollection<Movimiento> Movimientos { get; set; } = new List<Movimiento>();

    public virtual ICollection<Pago> Pagos { get; set; } = new List<Pago>();
}
