using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace SalsasAPI.Models;

public partial class DetalleMateriaPrima
{
    public int IdDetalleMateriaPrima { get; set; }

    public DateTime FechaCompra { get; set; }

    public DateTime? FechaVencimiento { get; set; }

    public double CantidadExistentes { get; set; }

    public int Estatus { get; set; }

    public int? IdMateriaPrima { get; set; }

    public int Porcentaje { get; set; }
    [JsonIgnore]
    public virtual ICollection<Compra> Compras { get; set; } = new List<Compra>();
    [JsonIgnore]
    public virtual MateriaPrima? IdMateriaPrimaNavigation { get; set; }
    [JsonIgnore]
    public virtual ICollection<MermaInventario> MermaInventarios { get; set; } = new List<MermaInventario>();
}
