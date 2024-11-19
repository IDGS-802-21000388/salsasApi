namespace SalsasAPI.Models
{
    public class DetalleCotizacion
    {
        public int IdDetalle { get; set; }
        public int IdCotizacion { get; set; }
        public string Descripcion { get; set; }
        public int Cantidad { get; set; }
        public decimal PrecioUnitario { get; set; }
        public decimal Total { get; set; }
    }

}
