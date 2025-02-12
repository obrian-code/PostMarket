using Microsoft.EntityFrameworkCore;
using wenAPIProducto.Models;

namespace wenAPIProducto.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        public DbSet<Producto> Productos { get; set; }
        public DbSet<Inventario> Inventarios { get; set; }

        public DbSet<Categoria> Categorias { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configuración de la relación entre Producto y Categoria
            modelBuilder.Entity<Producto>()
                .HasKey(p => p.IdProducto); // Establecer la clave primaria para Producto

            // Establecer que IdProducto se generará automáticamente
            modelBuilder.Entity<Producto>()
                .Property(p => p.IdProducto)
                .ValueGeneratedOnAdd();  // El valor se genera al agregar un nuevo registro

            // Configurar la relación de uno a muchos entre Producto y Categoria
            modelBuilder.Entity<Producto>()
                .HasOne(p => p.Categoria)           // Un Producto tiene una sola Categoria
                .WithMany(c => c.Productos)         // Una Categoria puede tener muchos Productos
                .HasForeignKey(p => p.IdCategoria) // La clave foránea en Producto es IdCategoria
                .OnDelete(DeleteBehavior.SetNull); // Si se elimina una Categoria, los Productos no se eliminan, solo se establece el IdCategoria como null

            // Configuración de la clave primaria para Categoria
            modelBuilder.Entity<Categoria>()
                .HasKey(c => c.IdCategoria);  // Clave primaria para Categoria

            modelBuilder.Entity<Categoria>()
                .Property(c => c.IdCategoria)
                .ValueGeneratedOnAdd();  // IdCategoria se genera automáticamente al agregar una Categoria
                                         // Configuración de la relación entre Inventarios y Producto
            modelBuilder.Entity<Inventario>()
                .HasKey(i => i.IdInventario);  // Clave primaria para Inventarios

            modelBuilder.Entity<Inventario>()
                .Property(i => i.IdInventario)
                .ValueGeneratedOnAdd();  // IdInventario se genera automáticamente al agregar un Inventario

            // Configuración de la relación entre Inventarios y Producto
            modelBuilder.Entity<Inventario>()
                .HasOne(i => i.Producto)               // Un Inventario está relacionado con un Producto
                .WithMany(p => p.Inventario)          // Un Producto puede tener muchos Inventarios
                .HasForeignKey(i => i.IdProducto)      // La clave foránea en Inventarios es IdProducto
                .OnDelete(DeleteBehavior.Cascade);     // Si se elimina un Producto, también se eliminan los Inventarios asociados
        }
    }
}
