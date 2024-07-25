using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace SalsasAPI.Models;

public partial class Compra
{
    public int IdCompra { get; set; }

    public int? IdMateriaPrima { get; set; }

    public int? IdDetalleMateriaPrima { get; set; }

    public double CantidadExistentes { get; set; }
    [JsonIgnore]
    public virtual DetalleMateriaPrima? IdDetalleMateriaPrimaNavigation { get; set; }
    [JsonIgnore]
    public virtual MateriaPrima? IdMateriaPrimaNavigation { get; set; }
}
