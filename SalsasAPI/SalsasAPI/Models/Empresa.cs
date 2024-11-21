namespace SalsasAPI.Models
{
    public class Empresa
    {
        public int IdEmpresa { get; set; }
        public string Nombre { get; set; }
        public string Telefono { get; set; }
        public int IdUsuario { get; set; }
        public Usuario Usuario { get; set; }
        public int IdDireccion { get; set; }
        public Direccion Direccion { get; set; }
    }
}
