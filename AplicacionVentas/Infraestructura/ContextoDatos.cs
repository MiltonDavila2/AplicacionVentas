using AplicacionVentas.Models;
using Microsoft.EntityFrameworkCore;

namespace AplicacionVentas.Infraestructura
{
    public class ContextoDatos : DbContext
    {
        public ContextoDatos(DbContextOptions<ContextoDatos> options) : base(options) { }
        
            public DbSet<Producto> Productos { get; set; }
            public DbSet<Categoria> Categorias { get; set; }
        
    }
}
