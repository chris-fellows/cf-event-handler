using CFEventHandler.API.Interfaces;

namespace CFEventHandler.API.Services
{
    public class RequestInfoService : IRequestInfoService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public RequestInfoService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
    }
}
