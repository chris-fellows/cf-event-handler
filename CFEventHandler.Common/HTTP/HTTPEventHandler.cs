using CFEventHandler.Interfaces;
using CFEventHandler.Models;

namespace CFEventHandler.HTTP
{
    /// <summary>
    /// Handles event by HTTP request
    /// </summary>
    public class HTTPEventHandler : IEventHandler
    {
        private readonly IDocumentTemplateProcessor _documentTemplateProcessor;
        private readonly IDocumentTemplateService _documentTemplateService;
        private readonly IHTTPSettingsService _httpSettingsService;

        public string Id => "HTTP";

        public HTTPEventHandler(IDocumentTemplateProcessor documentTemplateProcessor,
                            IDocumentTemplateService documentTemplateService,
                            IHTTPSettingsService httpSettingsService)
        {
            _documentTemplateProcessor = documentTemplateProcessor;
            _documentTemplateService = documentTemplateService;
            _httpSettingsService = httpSettingsService;
        }

        public void Handle(EventInstance eventInstance, string eventSettingsId)
        {
            var eventSettings = _httpSettingsService.GetByIdAsync(eventSettingsId).Result;
        }
    }
}
