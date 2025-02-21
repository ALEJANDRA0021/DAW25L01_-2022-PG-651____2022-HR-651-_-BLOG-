using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using L01_NUMEROS_CARNETS.Models;

namespace L01_NUMEROS_CARNETS.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsuariosController : ControllerBase
    {
        private readonly BlogDBContext _context;
        private readonly ILogger<UsuariosController> _logger;

        public UsuariosController(BlogDBContext context, ILogger<UsuariosController> logger)
        {
            _context = context;
            _logger = logger;
        }

     
        [HttpGet]
        public IActionResult GetUsuarios()
        {
            var usus = _context.Usuarios.ToList();
            return Ok(usus);
        }

       
        [HttpGet("{id}")]
        public IActionResult GetUsuario(int id)
        {
            var usu = _context.Usuarios.FirstOrDefault(u => u.usuarioId == id);
            if (usu == null)
            {
                return NotFound("No se encontro el usuario");
            }
            return Ok(usu);
        }

       
        [HttpPost]
        public IActionResult AddUsuario([FromBody] Usuario usuEntity)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _context.Usuarios.Add(usuEntity);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetUsuario), new { id = usuEntity.usuarioId }, usuEntity);
        }

       
        [HttpPut("{id}")]
        public IActionResult UpdateUsuario(int id, [FromBody] Usuario usuEntity)
        {
            var usuDB = _context.Usuarios.FirstOrDefault(u => u.usuarioId == id);
            if (usuDB == null)
                return NotFound("Usuario no encontrado");

         
            usuDB.nombreUsuario = usuEntity.nombreUsuario;
            usuDB.clave = usuEntity.clave;
            usuDB.nombre = usuEntity.nombre;
            usuDB.apellido = usuEntity.apellido;
            usuDB.rolId = usuEntity.rolId;

            _context.SaveChanges();
            return Ok("Usuario actualizado");
        }

       
        [HttpDelete("{id}")]
        public IActionResult DeleteUsuario(int id)
        {
            var usuDB = _context.Usuarios.FirstOrDefault(u => u.usuarioId == id);
            if (usuDB == null)
                return NotFound("Usuario no existe");

            _context.Usuarios.Remove(usuDB);
            _context.SaveChanges();
            return Ok("Usuario eliminado");
        }

       
        [HttpGet("filtro")]
        public IActionResult FiltrarPorNombreApellido(string nombre, string apellido)
        {
            //  usuarios que contengan el nombre y apellido (pueden ser parciales)
            var ususFiltrados = _context.Usuarios
                                .Where(u => u.nombre.Contains(nombre) && u.apellido.Contains(apellido))
                                .ToList();
            return Ok(ususFiltrados);
        }

        
        [HttpGet("filtroRol")]
        public IActionResult FiltrarPorRol(int rolId)
        {
            var ususFiltrados = _context.Usuarios
                                .Where(u => u.rolId == rolId)
                                .ToList();
            return Ok(ususFiltrados);
        }
    }
}
