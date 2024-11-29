using SalsasAPI.Models;

public class Empresa
{
    public int idEmpresa { get; set; }
    public string nombre { get; set; }
    public string telefono { get; set; }

    // Relación con Direccion
    public int idDireccion { get; set; }  // Asegúrate de que esta propiedad coincide con la columna en la base de datos
    public Direccion Direccion { get; set; }  // La propiedad de navegación a la entidad Direccion

}
