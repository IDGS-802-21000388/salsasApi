using System;
using System.Collections.Generic;

namespace SalsasAPI.Models;
public class EnvioDetalle
{
    public int IdEnvio { get; set; }
    public int EstatusPedido { get; set; }
    public string FechaEnvio { get; set; } = null!;
    public string FechaEntregaEstimada { get; set; } = null!;
    public string EstatusEnvio { get; set; } = null!;
    public string NombrePaqueteria { get; set; } = null!;
    public string NombreCliente { get; set; } = null!;
    public string NombreProducto { get; set; } = null!;
    public string Domicilio { get; set; } = null!;
    public double Total { get; set; }
}

