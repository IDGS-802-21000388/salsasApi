namespace SalsasAPI.Models
{
    public class Cotizaciones
    {
        public int IdCotizacion { get; set; }
        public int IdUsuario { get; set; }
        public string emailCliente { get; set; }
        public DateTime FechaCreacion { get; set; } = DateTime.Now;
        public decimal Subtotal { get; set; }
        public decimal Iva { get; set; }
        public decimal Total { get; set; }
        public int Atendida { get; set; } // 0 = No atendida, 1 = Atendida
    }
}
