using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using L01_NUMEROS_CARNETS.Models;

namespace L01_NUMEROS_CARNETS.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EstadisticasController : ControllerBase
    {
        private readonly BlogDBContext _context;
        private readonly ILogger<EstadisticasController> _logger;

        public EstadisticasController(BlogDBContext context, ILogger<EstadisticasController> logger)
        {
            _context = context;
            _logger = logger;
        }

        // 
        [HttpGet("topPublicaciones")]
        public IActionResult TopPublicaciones(int top)
        {
            // sse arma la consulta para cada publicacin se cuenta la cantidad de comentartios
            var topPubs = _context.Publicaciones
                .Select(pub => new
                {
                    publicacion = pub,
                    cantidadComent = _context.Comentarios.Count(c => c.publicacionId == pub.publicacionId)
                })
                .OrderByDescending(x => x.cantidadComent)
                .Take(top)
                .ToList();

            return Ok(topPubs);
        }
    }
}
