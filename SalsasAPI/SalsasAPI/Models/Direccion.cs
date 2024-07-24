namespace SalsasAPI.Models
{
    public partial class Direccion
    {
        public int IdDireccion { get; set; }
        public string Estado { get; set; } = null!;
        public string Municipio { get; set; } = null!;
        public string CodigoPostal { get; set; } = null!;
        public string Colonia { get; set; } = null!;
        public string Calle { get; set; } = null!;
        public string NumExt { get; set; } = null!;
        public string? NumInt { get; set; }
        public string? Referencia { get; set; }
    }
}
