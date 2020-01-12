using ApiKeyTest.Authentication.Models;
using Microsoft.Extensions.DependencyInjection;

namespace ApiKeyTest.Extensions
{
    public static class AuthConfigurations
    {
        public static IServiceCollection AddAppPolicies(this IServiceCollection services)
        {
            services.AddAuthorization(options =>
            {
                options.AddPolicy(nameof(Roles.Employee), policy => policy.RequireRole(Roles.Employee));
                options.AddPolicy(nameof(Roles.Manager), policy => policy.RequireRole(Roles.Manager));
                options.AddPolicy(nameof(Roles.Accountant), policy => policy.RequireRole(Roles.Accountant));
                options.AddPolicy(nameof(Roles.SupplyManager), policy => policy.RequireRole(Roles.SupplyManager));
                options.AddPolicy(nameof(Roles.Guest), policy => policy.RequireRole(Roles.Guest));
            });
            return services;
        }
    }
}