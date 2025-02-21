using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using L01_NUMEROS_CARNETS.Models;

namespace L01_NUMEROS_CARNETS.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PublicacionesController : ControllerBase
    {
        private readonly BlogDBContext _context;
        private readonly ILogger<PublicacionesController> _logger;

        public PublicacionesController(BlogDBContext context, ILogger<PublicacionesController> logger)
        {
            _context = context;
            _logger = logger;
        }

        
        [HttpGet]
        public IActionResult GetPublicaciones()
        {
            var pubs = _context.Publicaciones.ToList();
            return Ok(pubs);
        }

       
        [HttpGet("{id}")]
        public IActionResult GetPublicacion(int id)
        {
            var pub = _context.Publicaciones.FirstOrDefault(p => p.publicacionId == id);
            if (pub == null)
                return NotFound("No se encontro la publicacion");
            return Ok(pub);
        }

      
        [HttpPost]
        public IActionResult AddPublicacion([FromBody] Publicacion pubEntity)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _context.Publicaciones.Add(pubEntity);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetPublicacion), new { id = pubEntity.publicacionId }, pubEntity);
        }

        
        [HttpPut("{id}")]
        public IActionResult UpdatePublicacion(int id, [FromBody] Publicacion pubEntity)
        {
            var pubDB = _context.Publicaciones.FirstOrDefault(p => p.publicacionId == id);
            if (pubDB == null)
                return NotFound("Publicacion no encontrada");

          
            pubDB.titulo = pubEntity.titulo;
            pubDB.descripcion = pubEntity.descripcion;
            pubDB.usuarioId = pubEntity.usuarioId;

            _context.SaveChanges();
            return Ok("Publicacion actualizada");
        }

       
        [HttpDelete("{id}")]
        public IActionResult DeletePublicacion(int id)
        {
            var pubDB = _context.Publicaciones.FirstOrDefault(p => p.publicacionId == id);
            if (pubDB == null)
                return NotFound("Publicacion no existe");

            _context.Publicaciones.Remove(pubDB);
            _context.SaveChanges();
            return Ok("Publicacion eliminada");
        }

       
        [HttpGet("filtroPorUsuario")]
        public IActionResult FiltrarPorUsuario(int usuarioId)
        {
            var pubsFiltradas = _context.Publicaciones
                                   .Where(p => p.usuarioId == usuarioId)
                                   .ToList();
            return Ok(pubsFiltradas);
        }
    }
}
