public class EmpresaDto
{
    public string Nombre { get; set; }
    public string Telefono { get; set; }
    public int IdUsuario { get; set; }  // Solo el id de Usuario
    public DireccionDto Direccion { get; set; } // Dirección completa
}

public class DireccionDto
{
    public string Estado { get; set; }
    public string Municipio { get; set; }
    public string CodigoPostal { get; set; }
    public string Colonia { get; set; }
    public string Calle { get; set; }
    public string NumExt { get; set; }
    public string NumInt { get; set; }
    public string Referencia { get; set; }
}
