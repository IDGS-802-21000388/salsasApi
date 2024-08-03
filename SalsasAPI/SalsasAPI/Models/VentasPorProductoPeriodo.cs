namespace SalsasAPI.Models
{
    public partial class VentasPorProductoPeriodo
    {
        public int Id { get; set; }
        public int ProductoId { get; set; }
        public string NombreProducto { get; set; } = null!;
        public DateTime PeriodoInicio { get; set; }
        public DateTime PeriodoFin { get; set; }
        public int NumeroVentas { get; set; }
        public double CantidadVendida { get; set; }
        public double TotalRecaudado { get; set; }
        public string IndicadorGlobal { get; set; } = null!;

        public virtual Producto Producto { get; set; } = null!;
    }
}