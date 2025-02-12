using wenAPIProducto.Models;

namespace wenAPIProducto
{
    public class Inventario
    {


        public int IdInventario { get; set; }
        public int IdProducto { get; set; }

        public int Cantidad { get; set; }
        public string Ubicacion { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.Now; // Establecer la fecha actual
        public DateTime UpdatedAt { get; set; } = DateTime.Now;

        // Propiedad de navegación hacia Producto
        public Producto? Producto { get; set; }  // Relación con Producto
    }
}