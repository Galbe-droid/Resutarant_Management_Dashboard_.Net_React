using Microsoft.AspNetCore.Mvc;
using Template_restaurant_app.Application.Common;
using Template_restaurant_app.Domain.Enum;

namespace Template_restaurant_app.API.Extensions
{
    public static class ControllerExtentions
    {
        public static IActionResult FromResult<T>(this ControllerBase controller, Result<T> result)
        {
            if (result.Success)
                return controller.Ok(result);

            return result.Type switch
            {
                ResultType.Validation =>
                    controller.BadRequest(result),

                ResultType.NotFound =>
                    controller.NotFound(result),

                ResultType.Conflict =>
                    controller.Conflict(result),

                ResultType.Unauthorized =>
                    controller.Unauthorized(result),

                ResultType.Forbidden =>
                    controller.Forbid(),

                _ =>
                    controller.StatusCode(500, result)
            };
        }
    }
}
