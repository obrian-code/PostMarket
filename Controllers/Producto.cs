using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using wenAPIProducto.Data;
using wenAPIProducto.Helpers;
using wenAPIProducto.Models;

namespace wenAPIProducto.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductoController : ControllerBase
    {
        private readonly DataContext _db;

        public ProductoController(DataContext db)
        {
            _db = db;
        }

        // GET: api/producto
        [HttpGet]
        public async Task<IActionResult> GetProductos()
        {
            var productos = await _db.Productos.ToListAsync();
            return Ok(productos);
        }

        // GET: api/producto/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProducto(int id)
        {
            var producto = await _db.Productos.FindAsync(id);
            if (producto == null)
            {
                return ApiResponse.Errors("NotFound", "Producto no encontrado");
            }
            return Ok(producto);
        }

        // POST: api/producto
        [HttpPost]
        public async Task<IActionResult> CreateProducto(Producto producto)
        {
            // Configurar fechas de creación y actualización
            producto.CreatedAt = DateTime.UtcNow;
            producto.UpdatedAt = DateTime.UtcNow;

            // Guardar el nuevo producto en la base de datos
            _db.Productos.Add(producto);
            await _db.SaveChangesAsync();

            // Retornar el producto recién creado
            return ApiResponse.Created(nameof(GetProducto), producto.IdProducto, producto);
        }

        // PUT: api/producto/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProducto(int id, Producto productoActualizado)
        {
            var producto = await _db.Productos.FindAsync(id);

            if (producto == null)
            {
                return ApiResponse.Errors("NotFound", "Producto no encontrado");
            }

            // Actualizar los campos del producto
            producto.Codigo = productoActualizado.Codigo;
            producto.Descripcion = productoActualizado.Descripcion;
            producto.Precio = productoActualizado.Precio;
            producto.Activo = productoActualizado.Activo;
            producto.UpdatedAt = DateTime.UtcNow;

            // Guardar los cambios en la base de datos
            await _db.SaveChangesAsync();

            // Retornar el producto actualizado
            return Ok(producto);
        }

        // DELETE: api/producto/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProducto(int id)
        {
            var producto = await _db.Productos.FindAsync(id);

            if (producto == null)
            {
                return ApiResponse.Errors("NotFound", "Producto no encontrado");
            }

            // Eliminar el producto
            _db.Productos.Remove(producto);
            await _db.SaveChangesAsync();

            // Retornar una respuesta vacía indicando que la eliminación fue exitosa
            return ApiResponse.Delete();
        }
    }
}
