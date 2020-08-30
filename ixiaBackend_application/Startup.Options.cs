using ixiaBackend_application.Helpers;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.IO;
using System.Reflection;

namespace ixiaBackend_application
{
    public partial class Startup
    {
        private Action<SwaggerGenOptions> SwaggerOptions => options =>
        {
            options.SwaggerDoc(swaggerOptions.Version, new OpenApiInfo
            {
                Title = swaggerOptions.Description,
                Version = swaggerOptions.Version
            });

            var securityScheme = new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference { Id = "Bearer", Type = ReferenceType.SecurityScheme },
                Type = SecuritySchemeType.ApiKey,
                Scheme = "oaut2",
                Description = "JWT token",
                In = ParameterLocation.Header,
                Name = "Authorization",
                BearerFormat = "JWT"
            };

            var requirement = new OpenApiSecurityRequirement { [securityScheme] = new string[] { } };
            options.AddSecurityDefinition("Bearer", securityScheme);
            options.OperationFilter<AuthorizeCheckOperationFilter>(requirement);

            var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.XML";
            var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
            options.IncludeXmlComments(xmlPath);
        };

        private Action<JwtBearerOptions> JwtOptions => options =>
        {
            options.TokenValidationParameters.IssuerSigningKey = securityOptions.SecurityKey;
            options.TokenValidationParameters.ValidIssuer = securityOptions.Issuer;
            options.TokenValidationParameters.ValidAudience = securityOptions.Audiance;
            options.TokenValidationParameters.ValidateActor = false;
        };

        private Action<MvcNewtonsoftJsonOptions> JsonOptions => options =>
        {
            options.SerializerSettings.Converters.Add(new StringEnumConverter());
            options.SerializerSettings.NullValueHandling = NullValueHandling.Include;
            options.SerializerSettings.DateTimeZoneHandling = DateTimeZoneHandling.Utc;
        };

        private Action<AuthenticationOptions> AuthenitcationOptions => options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultForbidScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultSignInScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultSignOutScheme = JwtBearerDefaults.AuthenticationScheme;
        };
    }
}
