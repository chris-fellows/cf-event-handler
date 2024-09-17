using CFEventHandler.Interfaces;
using CFEventHandler.Models;

namespace CFEventHandler.Teams
{

    /// <summary>
    /// Handles event by logging to Teams
    /// </summary>
    public class TeamsEventHandler : IEventHandler
    {
        private readonly IDocumentTemplateProcessor _documentTemplateProcessor;
        private readonly IDocumentTemplateService _documentTemplateService;
        private readonly ITeamsSettingsService _teamsSettingsService;

        public string Id => "Teams";

        public TeamsEventHandler(IDocumentTemplateProcessor documentTemplateProcessor,
                        IDocumentTemplateService documentTemplateService,   
                        ITeamsSettingsService teamsSettingsService)
        {
            _documentTemplateProcessor = documentTemplateProcessor;
            _documentTemplateService = documentTemplateService;
            _teamsSettingsService = teamsSettingsService;
        }

        public void Handle(EventInstance eventInstance, string eventSettingsId)
        {
            var eventSettings = _teamsSettingsService.GetByIdAsync(eventSettingsId).Result;
        }
    }
}
