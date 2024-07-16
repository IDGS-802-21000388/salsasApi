using System;
using System.Collections.Generic;

namespace SalsasAPI.Models;

public partial class Medidum
{
    public int IdMedida { get; set; }

    public string? TipoMedida { get; set; }

    public virtual ICollection<DetalleVentum> DetalleVenta { get; set; } = new List<DetalleVentum>();

    public virtual ICollection<MateriaPrima> MateriaPrimas { get; set; } = new List<MateriaPrima>();

    public virtual ICollection<Producto> Productos { get; set; } = new List<Producto>();

    public virtual ICollection<Recetum> Receta { get; set; } = new List<Recetum>();
}
