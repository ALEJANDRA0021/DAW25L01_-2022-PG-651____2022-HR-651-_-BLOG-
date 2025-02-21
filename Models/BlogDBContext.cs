using Microsoft.EntityFrameworkCore;

namespace L01_NUMEROS_CARNETS.Models
{
    public class BlogDBContext : DbContext
    {
        
        public BlogDBContext(DbContextOptions<BlogDBContext> optiones) : base(optiones)
        {
        }

        public DbSet<Rol> Roles { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Publicacion> Publicaciones { get; set; }
        public DbSet<Comentario> Comentarios { get; set; }
        public DbSet<Calificacion> Calificaciones { get; set; }
    }

    public class Rol
    {
        public int rolId { get; set; }
        public string rol { get; set; }
    }

    public class Usuario
    {
        public int usuarioId { get; set; }
        public int rolId { get; set; }
        public string nombreUsuario { get; set; }
        public string clave { get; set; }
        public string nombre { get; set; }
        public string apellido { get; set; }
    }

    public class Publicacion
    {
        public int publicacionId { get; set; }
        public string titulo { get; set; }
        public string descripcion { get; set; }
        public int usuarioId { get; set; }
    }

    public class Comentario
    {
        
        public int cometarioId { get; set; }
        public int publicacionId { get; set; }
        public string comentario { get; set; }
        public int usuarioId { get; set; }
    }

    public class Calificacion
    {
        public int calificacionId { get; set; }
        public int publicacionId { get; set; }
        public int usuarioId { get; set; }
        public int calificacion { get; set; }
    }
}
