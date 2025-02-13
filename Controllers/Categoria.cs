using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using wenAPIProducto.Data;
using wenAPIProducto.Helpers;
using wenAPIProducto.Models;

namespace wenAPIProducto.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoriaController : ControllerBase
    {
        private readonly DataContext _db;

        public CategoriaController(DataContext db)
        {
            _db = db;
        }

        [HttpGet]
        public async Task<IActionResult> GetCategorias()
        {
            var categorias = await _db.Categorias
            .Select(i => new
            {
                i.IdCategoria,
                i.Nombre,
                i.Descripcion,
                i.Activo

            })
            .ToListAsync();
            return Ok(categorias);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCategoria(int id)
        {
            var categoria = await _db.Categorias
            .Where(i => i.IdCategoria == id)
        .Select(i => new
        {
            i.IdCategoria,
            i.Nombre,
            i.Descripcion,
            i.Activo
        })
        .FirstOrDefaultAsync();
            if (categoria == null)
            {
                return ApiResponse.Errors("NotFound", "Categoria no encontrada");
            }
            return Ok(categoria);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCategoria(Categoria categoria)
        {
            categoria.CreatedAt = DateTime.UtcNow;
            categoria.UpdatedAt = DateTime.UtcNow;
            _db.Categorias.Add(categoria);
            await _db.SaveChangesAsync();
            return ApiResponse.Created(nameof(GetCategoria), categoria.IdCategoria, categoria);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCategoria(int id, Categoria categoriaActualizada)
        {
            var categoria = await _db.Categorias.FindAsync(id);

            if (categoria is null)
            {
                return ApiResponse.Errors("NotFound", "Categoria no encontrado");
            }
            categoria.Nombre = categoriaActualizada.Nombre;
            categoria.Descripcion = categoriaActualizada.Descripcion;
            categoria.Activo = categoriaActualizada.Activo;
            categoria.UpdatedAt = DateTime.UtcNow;

            await _db.SaveChangesAsync();
            return Ok(categoria);

        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategoria(int id)
        {
            var categoria = await _db.Categorias.FindAsync(id);
            if (categoria is null)
            {
                return ApiResponse.Errors("NotFound", "Categoria no encontrada");
            }

            _db.Categorias.Remove(categoria);
            await _db.SaveChangesAsync();
            return ApiResponse.Delete();
        }
    }
}