using System;
using System.Collections.Generic;

namespace SalsasAPI.Models;

public partial class DetalleRecetum
{
    public int IdDetalleReceta { get; set; }

    public int CantidadMateriaPrima { get; set; }
    public int MedidaIngrediente { get; set; }

    public int? IdMateriaPrima { get; set; }

    public int? IdReceta { get; set; }

    public virtual MateriaPrima? IdMateriaPrimaNavigation { get; set; }

    public virtual Recetum? IdRecetaNavigation { get; set; }
}
