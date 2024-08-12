using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace SalsasAPI.Models;

public partial class Tarjetum
{
    public int IdTarjeta { get; set; }

    public string NumeroTarjeta { get; set; } = null!;

    public string NombreTitular { get; set; } = null!;

    public string FechaExpiracion { get; set; } = null!;

    public string Cvv { get; set; } = null!;

    public int? IdPago { get; set; }
    [JsonIgnore]

    public virtual Pago? IdPagoNavigation { get; set; }
}
