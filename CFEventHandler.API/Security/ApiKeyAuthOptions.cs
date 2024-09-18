using Microsoft.AspNetCore.Authentication;

namespace CFEventHandler.API.Security
{
    public class ApiKeyAuthOptions : AuthenticationSchemeOptions
    {
        public const string DefaultScheme = "APIKey";   // Was "API Key"
        public static string Scheme => DefaultScheme;
        public const string AuthenticationType = DefaultScheme;
    }
}
