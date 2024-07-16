using System;
using System.Collections.Generic;

namespace SalsasAPI.Models;

public partial class Tarjetum
{
    public int IdTarjeta { get; set; }

    public string NumeroTarjeta { get; set; } = null!;

    public string NombreTitular { get; set; } = null!;

    public string FechaExpiracion { get; set; } = null!;

    public string Cvv { get; set; } = null!;

    public int? IdPago { get; set; }

    public virtual Pago? IdPagoNavigation { get; set; }
}
