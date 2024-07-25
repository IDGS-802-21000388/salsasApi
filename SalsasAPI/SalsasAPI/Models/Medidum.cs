using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace SalsasAPI.Models;

public partial class Medidum
{
    public int IdMedida { get; set; }

    public string? TipoMedida { get; set; }
    [JsonIgnore]
    public virtual ICollection<DetalleVentum> DetalleVenta { get; set; } = new List<DetalleVentum>();
    [JsonIgnore]
    public virtual ICollection<MateriaPrima> MateriaPrimas { get; set; } = new List<MateriaPrima>();
    [JsonIgnore]
    public virtual ICollection<Producto> Productos { get; set; } = new List<Producto>();
    [JsonIgnore]
    public virtual ICollection<Recetum> Receta { get; set; } = new List<Recetum>();
}
