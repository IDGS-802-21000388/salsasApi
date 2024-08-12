using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace SalsasAPI.Models;

public partial class Pago
{
    public int IdPago { get; set; }

    public DateTime FechaPago { get; set; }

    public double Monto { get; set; }

    public string MetodoPago { get; set; } = null!;

    public int? IdVenta { get; set; }
    [JsonIgnore]

    public virtual ICollection<Efectivo> Efectivos { get; set; } = new List<Efectivo>();
    [JsonIgnore]

    public virtual Ventum? IdVentaNavigation { get; set; }
    [JsonIgnore]

    public virtual ICollection<Tarjetum> Tarjeta { get; set; } = new List<Tarjetum>();
}
