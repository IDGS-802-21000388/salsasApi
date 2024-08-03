using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SalsasAPI.Models;
using System.Threading.Tasks;

namespace SalsasAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ETLController : ControllerBase
    {
        private readonly IServiceProvider _serviceProvider;

        public ETLController(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }
    }
}
