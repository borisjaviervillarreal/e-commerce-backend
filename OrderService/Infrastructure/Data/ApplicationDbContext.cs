using Microsoft.EntityFrameworkCore;
using OrderService.Domain.Entities;

namespace OrderService.Infrastructure.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
        public DbSet<Pedido> Pedidos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configurar la relación entre Pedido y ProductoPedido
            modelBuilder.Entity<Pedido>()
                .OwnsMany(p => p.Productos, pp =>
                {
                    pp.WithOwner().HasForeignKey("PedidoId"); // Configurar la FK para ProductoPedido
                    pp.Property<int>("ProductoId"); // Definir la PK compuesta
                    pp.HasKey("ProductoId", "PedidoId"); // Definir la PK compuesta
                });
        }
    }
}
