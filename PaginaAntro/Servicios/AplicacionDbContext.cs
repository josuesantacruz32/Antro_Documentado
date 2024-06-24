using Microsoft.EntityFrameworkCore;
using PaginaAntro.Modelos;

namespace PaginaAntro.Servicios
   // Representa el contexto de base de datos para la aplicación
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {

        }

        //Obtiene un conjunto de datos de productos en la base de datos.

        public DbSet<Producto> Producto { get; set; }
    }
}
