using Microsoft.AspNetCore.Mvc;
using System.Linq;
using SalsasAPI.Models;

[ApiController]
[Route("api/[controller]")]
public class DashboardController : ControllerBase
{
    private readonly SalsaContext _context;

    public DashboardController(SalsaContext context)
    {
        _context = context;
    }


    // Aquí puedes agregar métodos similares para inventarios y ranking de clientes si son necesarios
}
