using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace SalsasAPI.Models;

public partial class DetalleMateriaPrima
{
    public int idDetalleMateriaPrima { get; set; }

    public DateTime fechaCompra { get; set; }

    public DateTime? fechaVencimiento { get; set; }

    public double cantidadExistentes { get; set; }

    public int estatus { get; set; }

    public int? idMateriaPrima { get; set; }

    public int porcentaje { get; set; }
    [JsonIgnore]
    public virtual ICollection<Compra> Compras { get; set; } = new List<Compra>();
    [JsonIgnore]
    public virtual MateriaPrima? IdMateriaPrimaNavigation { get; set; }
    [JsonIgnore]
    public virtual ICollection<MermaInventario> MermaInventarios { get; set; } = new List<MermaInventario>();
}
