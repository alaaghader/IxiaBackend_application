using ixiaBackend_application.Models.ModelsView;
using Microsoft.AspNetCore.Mvc;

namespace ixiaBackend_application.Helpers
{
    public static class ActionResultExtensions
    {
        public static IActionResult ToActionResult<T>(this Result<T> result) => new ObjectResult(result)
        {
            StatusCode = result.Status
        };
    }
}
