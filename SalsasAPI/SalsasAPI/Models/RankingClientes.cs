namespace SalsasAPI.Models
{
    public class RankingClientes
    {
        public int Id { get; set; }
        public int IdUsuario { get; set; }
        public string NombreUsuario { get; set; } = null!;
        public double ComprasTotales { get; set; }
        public string ProductosComprados { get; set; } = null!;
        public DateTime UltimaActualizacion { get; set; }

        public virtual Usuario Usuario { get; set; } = null!;
    }
}
