namespace SalsasAPI.Models
{
    public class vw_Producto_Detalle
    {
        public int IdProducto { get; set; }
        public string NombreProducto { get; set; } = null!;
        public int Cantidad { get; set; } 
        public string TipoMedida { get; set; } = null!;
        public string Fotografia { get; set; } = null!;
    }
}
