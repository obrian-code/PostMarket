using wenAPIProducto.Models;

namespace wenAPIProducto
{
    public class Categoria
    {
        public int IdCategoria { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Descripcion { get; set; } = string.Empty;
        public bool Activo { get; set; } = true;
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        // Relación con productos
        public ICollection<Producto>? Productos { get; set; }  // Una categoría puede tener varios productos

    }
}

