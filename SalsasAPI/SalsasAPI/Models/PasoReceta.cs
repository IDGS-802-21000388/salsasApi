using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace SalsasAPI.Models;

public partial class PasoReceta
{
    public int IdPasoReceta { get; set; }

    public int? Paso { get; set; }

    public string Descripcion { get; set; } = null!;

    public int? IdProducto { get; set; }
    [JsonIgnore]
    public virtual Producto? IdProductoNavigation { get; set; }
}
