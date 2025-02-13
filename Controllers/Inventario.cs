using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using wenAPIProducto.Data;
using wenAPIProducto.Helpers;

namespace wenAPIProducto.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class InventarioController : ControllerBase
    {
        private readonly DataContext _db;
        public InventarioController(DataContext db)
        {
            _db = db;
        }

        [HttpGet]
        public async Task<IActionResult> GetInventarios(int pageNumber = 1, int pageSize = 10)
        {

            var result = await Pagination.GetPaginatedData(
             _db.Inventarios
            .Include(i => i.Producto),
            pageNumber,
            pageSize,
            async query => await query
            .Select(i => new
            {
                i.IdInventario,
                i.Cantidad,
                i.Ubicacion,
                Producto = new
                {
                    Codigo = i.Producto != null ? i.Producto.Codigo : null,
                    Descripcion = i.Producto != null ? i.Producto.Descripcion : null,
                }
            })
            .ToListAsync()
            );

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetInventario(int id)
        {
            var inventario = await _db.Inventarios.FindAsync(id);
            if (inventario is null)
            {
                return ApiResponse.Errors("NotFound", "Inventario no encontrado");
            }
            return Ok(inventario);
        }

        [HttpPost]
        public async Task<IActionResult> CreateInventario(Inventario inventario)
        {
            inventario.CreatedAt = DateTime.UtcNow;
            inventario.UpdatedAt = DateTime.UtcNow;
            _db.Inventarios.Add(inventario);
            await _db.SaveChangesAsync();


            return ApiResponse.Created(nameof(GetInventario), new { id = inventario.IdInventario }, inventario);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateInventario(int id, Inventario inventarioActualizado)
        {
            var inventario = await _db.Inventarios.FindAsync(id);
            if (inventario is null)
            {
                return ApiResponse.Errors("NotFound", "Inventario no encontrado");
            }

            inventario.Cantidad = inventarioActualizado.Cantidad;
            inventario.IdProducto = inventario.IdProducto;
            inventario.Ubicacion = inventarioActualizado.Ubicacion;
            inventario.UpdatedAt = DateTime.UtcNow;
            await _db.SaveChangesAsync();
            return Ok(inventario);

        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteInventario(int id)
        {
            var inventario = await _db.Inventarios.FindAsync(id);
            if (inventario == null)
            {
                return ApiResponse.Errors("NoFound", "Inventario no encontrado");
            }
            _db.Inventarios.Remove(inventario);
            await _db.SaveChangesAsync();
            return ApiResponse.Delete();
        }
    }
}