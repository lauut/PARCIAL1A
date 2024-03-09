using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;
namespace PARCIAL1A.Models
{
    public class PARCIAL1AContext : DbContext
    {
        public PARCIAL1AContext(DbContextOptions<PARCIAL1AContext> options) : base(options) 
        {

        }
        public DbSet<Autores> Autores { get; set; }
        public DbSet<AutorLibro> AutorLibro { get; set;}
        public DbSet<Libros> Libros { get; set; }
        public DbSet<Posts> Posts { get; set; }



    }
}
