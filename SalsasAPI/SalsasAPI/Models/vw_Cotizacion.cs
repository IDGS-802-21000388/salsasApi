namespace SalsasAPI.Models
{
    public class vw_Cotizacion
    {
        public int IdCotizacion { get; set; }
        public int IdUsuario { get; set; }
        public int Atendida { get; set; } // 0 = No atendida, 1 = Atendida
        public string EmailCliente { get; set; }
        public DateTime FechaCreacion { get; set; }
        public decimal Subtotal { get; set; }
        public decimal Iva { get; set; }
        public decimal TotalCotizacion { get; set; }
        public int IdDetalle { get; set; }
        public string Descripcion { get; set; }
        public int Cantidad { get; set; }
        public decimal PrecioUnitario { get; set; }
        public decimal TotalDetalle { get; set; }
    }

}
