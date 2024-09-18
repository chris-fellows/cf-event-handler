using CFEventHandler.API.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using System.Text.Encodings.Web;

namespace CFEventHandler.API.Security
{
    public class ApiKeyAuthHandler : AuthenticationHandler<ApiKeyAuthOptions>
    {
        private readonly IAPIKeyCacheService _apiKeyCacheService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ApiKeyAuthHandler(IOptionsMonitor<ApiKeyAuthOptions> options, ILoggerFactory logger, UrlEncoder encoder, ISystemClock clock, IHttpContextAccessor httpContextAccessor, IAPIKeyCacheService apiKeyCacheService) : base(options, logger, encoder, clock)
        {
            _apiKeyCacheService = apiKeyCacheService;
            _httpContextAccessor = httpContextAccessor;
        }

        protected override Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            var apiKey = Context.Request.Headers["X-API-KEY"];

            // Check if API key passed
            if (String.IsNullOrEmpty(apiKey))
            {
                return Task.FromResult(AuthenticateResult.Fail("Invalid X-API-KEY"));
            }

            // Check if API key valid
            var apiKeyInstance = _apiKeyCacheService.GetById(apiKey);
            if (apiKeyInstance == null || apiKeyInstance.EndTime < DateTimeOffset.UtcNow)
            {
                return Task.FromResult(AuthenticateResult.Fail("Invalid X-API-KEY"));
            }
            
            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name, "VALID USER")
            };
            if (apiKeyInstance.Roles != null)
            {
                apiKeyInstance.Roles.ForEach(role => claims.Add(new Claim(ClaimTypes.Role, role)));
            }
            var identity = new ClaimsIdentity(claims.ToArray(), Scheme.Name);
            var principal = new ClaimsPrincipal(identity);            
            var ticket = new AuthenticationTicket(principal, Scheme.Name);
                       
            //claims.AddRange(apiKeyInstance.Roles.Select(role => new Claim(ClaimTypes.Role, role)));

            return Task.FromResult(AuthenticateResult.Success(ticket));
        }
    }
}
