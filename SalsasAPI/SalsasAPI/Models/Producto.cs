using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace SalsasAPI.Models;

public partial class Producto
{
    public int IdProducto { get; set; }

    public string NombreProducto { get; set; } = null!;

    public double PrecioVenta { get; set; }

    public double PrecioProduccion { get; set; }

    public int? IdMedida { get; set; }

    public string? Fotografia { get; set; }

    public bool Estatus { get; set; }

    [JsonIgnore]
    public virtual ICollection<DetalleProducto> DetalleProductos { get; set; } = new List<DetalleProducto>();
    [JsonIgnore]
    public virtual ICollection<DetalleVentum> DetalleVenta { get; set; } = new List<DetalleVentum>();
    [JsonIgnore]
    public virtual Medidum? IdMedidaNavigation { get; set; }
    [JsonIgnore]
    public virtual ICollection<Merma> Mermas { get; set; } = new List<Merma>();
    [JsonIgnore]
    public virtual ICollection<PasoReceta> PasoReceta { get; set; } = new List<PasoReceta>();

    public virtual ICollection<Recetum> Receta { get; set; } = new List<Recetum>();

    public virtual ICollection<SolicitudProduccion> SolicitudProduccions { get; set; } = new List<SolicitudProduccion>();
}
