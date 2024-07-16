using System;
using System.Collections.Generic;

namespace SalsasAPI.Models;

public partial class Paqueterium
{
    public int IdPaqueteria { get; set; }

    public string NombrePaqueteria { get; set; } = null!;

    public string Telefono { get; set; } = null!;

    public string Direccion { get; set; } = null!;

    public int Estatus { get; set; }

    public virtual ICollection<Envio> Envios { get; set; } = new List<Envio>();
}
