namespace SalsasAPI.Models
{
    public class vw_Detalle_Receta
    {
        public int IdProducto { get; set; }
        public double PrecioVenta { get; set; }
        public double PrecioProduccion { get; set; }
        public string MedidaProducto { get; set; } = null!;
        public int IdReceta { get; set; }
        public string NombreMateria { get; set; } = null!;
    }
}
