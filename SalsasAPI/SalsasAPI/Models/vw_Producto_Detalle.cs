namespace SalsasAPI.Models
{
    public class vw_Producto_Detalle
    {
        public int IdProducto { get; set; }
        public string NombreProducto { get; set; } = null!;
        public double PrecioVenta { get; set; }
        public double PrecioProduccion { get; set; }
        public double Cantidad { get; set; } 
        public string TipoMedida { get; set; } = null!;
        public string Fotografia { get; set; } = null!;
        public bool Estatus { get; set; }

    }
}
