using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using L01_NUMEROS_CARNETS.Models;

namespace L01_NUMEROS_CARNETS.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ComentariosController : ControllerBase
    {
        private readonly BlogDBContext _context;
        private readonly ILogger<ComentariosController> _logger;

        public ComentariosController(BlogDBContext context, ILogger<ComentariosController> logger)
        {
            _context = context;
            _logger = logger;
        }

       
        [HttpGet]
        public IActionResult GetComentarios()
        {
            var coments = _context.Comentarios.ToList();
            return Ok(coments);
        }

        [HttpGet("{id}")]
        public IActionResult GetComentario(int id)
        {
            var coment = _context.Comentarios.FirstOrDefault(c => c.cometarioId == id);
            if (coment == null)
                return NotFound("No se encontro el comentario");
            return Ok(coment);
        }

        
        [HttpPost]
        public IActionResult AddComentario([FromBody] Comentario comentEntity)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _context.Comentarios.Add(comentEntity);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetComentario), new { id = comentEntity.cometarioId }, comentEntity);
        }

        
        [HttpPut("{id}")]
        public IActionResult UpdateComentario(int id, [FromBody] Comentario comentEntity)
        {
            var comentDB = _context.Comentarios.FirstOrDefault(c => c.cometarioId == id);
            if (comentDB == null)
                return NotFound("Comentario no encontrado");

        
            comentDB.comentario = comentEntity.comentario;
            comentDB.publicacionId = comentEntity.publicacionId;
            comentDB.usuarioId = comentEntity.usuarioId;

            _context.SaveChanges();
            return Ok("Comentario actualizado");
        }

        
        [HttpDelete("{id}")]
        public IActionResult DeleteComentario(int id)
        {
            var comentDB = _context.Comentarios.FirstOrDefault(c => c.cometarioId == id);
            if (comentDB == null)
                return NotFound("Comentario no existe");

            _context.Comentarios.Remove(comentDB);
            _context.SaveChanges();
            return Ok("Comentario eliminado");
        }

        
        [HttpGet("filtroPorPublicacion")]
        public IActionResult FiltrarPorPublicacion(int publicacionId)
        {
            var comentsFiltrados = _context.Comentarios
                                    .Where(c => c.publicacionId == publicacionId)
                                    .ToList();
            return Ok(comentsFiltrados);
        }
    }
}
