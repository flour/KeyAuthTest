using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Threading.Tasks;
using ApiKeyTest.Commons;
using ApiKeyTest.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace ApiKeyTest.Authentication
{
    public class AuthenticationHandler : AuthenticationHandler<AuthenticationOptions>
    {
        private ILogger _logger;
        private IKeyStorage _keysStorage;

        public AuthenticationHandler(
            IOptionsMonitor<AuthenticationOptions> options,
            ILoggerFactory logger,
            UrlEncoder encoder,
            ISystemClock clock,
            IKeyStorage keysStorage
        ) : base(options, logger, encoder, clock)
        {
            _logger = logger.CreateLogger(nameof(AuthenticationHandler));
            _keysStorage = keysStorage;
        }

        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            if (!Request.Headers.TryGetValue(ApiKeyConstants.HeaderName, out var apiKeyHeaderValues))
            {
                _logger.LogWarning("No header provided");
                return AuthenticateResult.NoResult();
            }
            var headerKey = apiKeyHeaderValues.FirstOrDefault();
            if (string.IsNullOrEmpty(headerKey))
            {
                _logger.LogWarning("Auth Header is empty");
                return AuthenticateResult.NoResult();
            }
            var apiKey = await _keysStorage.GetApiKey(headerKey);
            if (apiKey == null)
            {
                _logger.LogWarning("Invalid API Key provided.");
                return AuthenticateResult.Fail("Invalid API Key provided.");
            }

            var claims = new List<Claim>(apiKey.Roles.Select(role => new Claim(ClaimTypes.Role, role)));
            claims.Add(new Claim(ClaimTypes.Name, apiKey.OwnerName));

            var principal = new ClaimsPrincipal(new ClaimsIdentity(claims, Options.AuthenticationType));
            var ticket = new AuthenticationTicket(principal, Options.Scheme);
            return AuthenticateResult.Success(ticket);
        }

        protected override async Task HandleForbiddenAsync(AuthenticationProperties properties)
        {
            Response.StatusCode = 403;
            Response.ContentType = ApiKeyConstants.ContentType;
            await Response.WriteAsync(JsonSerializer.Serialize(new ProblemDetails { Title = "Forbidden", Status = 403 }));
        }

        protected override async Task HandleChallengeAsync(AuthenticationProperties properties)
        {
            Response.StatusCode = 401;
            Response.ContentType = ApiKeyConstants.ContentType;
            await Response.WriteAsync(JsonSerializer.Serialize(
                new ProblemDetails
                {
                    Title = "Unauthorized",
                    Detail = "",
                    Status = 401,
                    Type = "https://httpstatuses.com/401"
                },
                new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                    IgnoreNullValues = true
                }
            ));
        }

    }
}