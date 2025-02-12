
using Microsoft.AspNetCore.Mvc;


namespace wenAPIProducto.Helpers
{
    public static class ApiResponse
    {

        private static readonly Dictionary<string, Func<string, ActionResult>> errorMappings = new()
    {
        { "NotFound", message => new NotFoundObjectResult(new { Message = message }) },
        { "BadRequest", message => new BadRequestObjectResult(new { Message = message }) },

    };

        public static CreatedAtActionResult Created(string action, object routeValues, object value)
        {
            return new CreatedAtActionResult(action, null, new { id = routeValues }, value);
        }

        public static NoContentResult Delete()
        {
            return new NoContentResult();
        }

        public static ActionResult Errors(string type, string message)
        {

            return errorMappings.ContainsKey(type)
                ? errorMappings[type](message)
                : new BadRequestObjectResult(new { Message = "Ocurrió un error inesperado. Por favor, intente nuevamente más tarde." });
        }
    }
}