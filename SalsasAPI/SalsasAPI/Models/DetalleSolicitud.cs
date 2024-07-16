using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace SalsasAPI.Models;

public partial class DetalleSolicitud
{
    public int IdDetalleSolicitud { get; set; }

    public int? IdSolicitud { get; set; }

    public DateTime FechaInicio { get; set; }

    public DateTime? FechaFin { get; set; }

    public int? IdUsuario { get; set; }

    public bool Estatus { get; set; }

    public int NumeroPaso { get; set; }
    [JsonIgnore]
    public virtual SolicitudProduccion? IdSolicitudNavigation { get; set; }
    [JsonIgnore]
    public virtual Usuario? IdUsuarioNavigation { get; set; }
}
