using System;
using System.Collections.Generic;

namespace SalsasAPI.Models;

public partial class Proveedor
{
    public int IdProveedor { get; set; }

    public string NombreProveedor { get; set; } = null!;

    public string Direccion { get; set; } = null!;

    public string Telefono { get; set; } = null!;

    public string NombreAtiende { get; set; } = null!;

    public int Estatus { get; set; }

    public virtual ICollection<MateriaPrima> MateriaPrimas { get; set; } = new List<MateriaPrima>();
}
