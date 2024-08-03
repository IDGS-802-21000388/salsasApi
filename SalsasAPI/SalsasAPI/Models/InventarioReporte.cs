namespace SalsasAPI.Models
{
    public partial class InventarioReporte
    {
        public int Id { get; set; }
        public string Tipo { get; set; } = null!;
        public string Nombre { get; set; } = null!;
        public double Cantidad { get; set; }
        public DateTime UltimaActualizacion { get; set; }
    }
}
