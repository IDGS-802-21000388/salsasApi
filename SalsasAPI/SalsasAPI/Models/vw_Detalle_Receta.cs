namespace SalsasAPI.Models
{
    public class vw_Detalle_Receta
    {
        public int IdProducto { get; set; }
        public int IdMateriaPrima { get; set; }
        public string NombreMateria { get; set; } = null!;
        public int Cantidad { get; set; }
        public int IdMedida { get; set; }
        public string TipoMedida { get; set; } = null!;
        public int IdReceta { get; set; }
        
    }
}
