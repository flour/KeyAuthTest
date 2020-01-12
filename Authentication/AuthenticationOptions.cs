using Microsoft.AspNetCore.Authentication;

namespace ApiKeyTest.Authentication
{
    public class AuthenticationOptions : AuthenticationSchemeOptions
    {
        public const string DefaultScheme = "API Key";
        public string Scheme => DefaultScheme;
        public string AuthenticationType = DefaultScheme;
    }
}