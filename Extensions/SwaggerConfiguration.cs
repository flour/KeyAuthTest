using ApiKeyTest.Commons;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace ApiKeyTest.Extensions
{

    public static class SwaggerConfiguration
    {
        public static IServiceCollection AddAppSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo { Title = "Test Api with keys", Version = "v1" });
                options.AddSecurityDefinition(ApiKeyConstants.HeaderName, new OpenApiSecurityScheme
                {
                    Type = SecuritySchemeType.ApiKey,
                    In = ParameterLocation.Header,
                    Name = ApiKeyConstants.HeaderName,
                    Description = "Api key is needed"
                });
                options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Name = ApiKeyConstants.HeaderName,
                            Type = SecuritySchemeType.ApiKey,
                            In = ParameterLocation.Header,
                            Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = ApiKeyConstants.HeaderName },
                        },
                        new string[] {}
                    }
                });
            });
            return services;
        }
    }
}