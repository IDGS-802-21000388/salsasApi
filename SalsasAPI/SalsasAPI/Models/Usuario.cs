using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace SalsasAPI.Models;

public partial class Usuario
{
    public int IdUsuario { get; set; }

    public string Nombre { get; set; } = null!;

    public string NombreUsuario { get; set; } = null!;
    public string Correo { get; set; } = null!;

    public string Contrasenia { get; set; } = null!;

    public string Rol { get; set; } = null!;

    public int Estatus { get; set; }

    public string Telefono { get; set; } = null!;

    public int Intentos { get; set; }

    public int IdDireccion { get; set; }

    public DateTime? DateLastToken { get; set; }

    public virtual Direccion Direccion { get; set; } = null!;
    [JsonIgnore]
    public virtual ICollection<DetalleSolicitud> DetalleSolicituds { get; set; } = new List<DetalleSolicitud>();
    [JsonIgnore]
    public virtual ICollection<SolicitudProduccion> SolicitudProduccions { get; set; } = new List<SolicitudProduccion>();
    [JsonIgnore]
    public virtual ICollection<Ventum> Ventas { get; set; } = new List<Ventum>();
    [JsonIgnore]
    public virtual ICollection<RankingClientes> RankingClientes { get; set; } = new List<RankingClientes>();
    [JsonIgnore]
    public virtual ICollection<Testimonio> Testimonios { get; set; } = new List<Testimonio>();

    [JsonIgnore]
    public ICollection<Queja> Quejas { get; set; } = new List<Queja>();

    [JsonIgnore]
    public virtual ICollection<UsuarioCodigoDescuento> UsuarioCodigoDescuentos { get; set; } = new List<UsuarioCodigoDescuento>();    

}
