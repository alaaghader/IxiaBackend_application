using Microsoft.AspNetCore.Authorization;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Linq;

namespace ixiaBackend_application.Helpers
{
    public class AuthorizeCheckOperationFilter : IOperationFilter
    {
        private readonly OpenApiSecurityRequirement requirement;

        public AuthorizeCheckOperationFilter(OpenApiSecurityRequirement requirement)
        {
            this.requirement = requirement;
        }

        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            if (context.ApiDescription.CustomAttributes().OfType<AuthorizeAttribute>().Any())
            {
                operation.Security.Add(requirement);
            }
        }
    }
}
