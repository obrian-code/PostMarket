using Microsoft.EntityFrameworkCore;


namespace wenAPIProducto.Helpers
{
    public static class Pagination
    {
        public static async Task<object> GetPaginatedData<T>(
            IQueryable<T> query,
            int pageNumber,
            int pageSize,
            Func<IQueryable<T>, Task<object>> selectFunc
        )
        {
            // Calcular el skip para la paginación
            var skip = (pageNumber - 1) * pageSize;

            // Obtener los items con la paginación aplicada
            var items = await selectFunc(query.Skip(skip).Take(pageSize));

            // Obtener el total de elementos sin paginar
            var totalItems = await query.CountAsync();
            var totalPages = (int)Math.Ceiling((double)totalItems / pageSize);

            // Construir la respuesta con los detalles de la paginación
            return new
            {
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalPages = totalPages,
                TotalItems = totalItems,
                Items = items
            };
        }
    }
}
