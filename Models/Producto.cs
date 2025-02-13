namespace wenAPIProducto.Models
{
    public class Producto
    {
        public int IdProducto { get; set; }  // Renombrado a PascalCase (según convención)
        public string Codigo { get; set; } = string.Empty;
        public string Descripcion { get; set; } = string.Empty;
        public decimal Precio { get; set; }  // Descomentado
        public bool Activo { get; set; }    // Descomentado
        public DateTime CreatedAt { get; set; } = DateTime.Now; // Establecer la fecha actual
        public DateTime UpdatedAt { get; set; } = DateTime.Now;

        // Relación con la tabla Categorías (si un producto tiene una categoría)
        public int? IdCategoria { get; set; }  // Relación con Categorías (opcional, ya que un producto puede no tener categoría)
        public Categoria? Categoria { get; set; }  // Navegación hacia Categoría (nullable)

        // Relación con la tabla Inventarios
        public ICollection<Inventario>? Inventario { get; set; }  // Un producto puede estar en varios inventarios
    }
}
