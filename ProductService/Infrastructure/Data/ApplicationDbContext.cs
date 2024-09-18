using Microsoft.EntityFrameworkCore;
using ProductService.Domain.Entities;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace ProductService.Infrastructure.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
        public DbSet<Producto> Productos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // Aquí agregar más configuraciones
        }
    }
}
