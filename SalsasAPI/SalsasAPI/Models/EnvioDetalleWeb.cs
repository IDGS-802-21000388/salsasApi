namespace SalsasAPI.Models
{
    public class EnvioDetalleWeb
    {
        public int IdEnvio { get; set; }
        public int IdUsuario { get; set; }
        public string NombreCliente { get; set; }
        public string Domicilio { get; set; }
        public string EstatusEnvio { get; set; }
        public string FechaEnvio { get; set; }
        public string FechaEntregaEstimada { get; set; }
        public string Productos { get; set; }
        public double Total { get; set; }
    }
}
