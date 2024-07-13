using CFEventHandler.Models;

namespace CFEventHandler.SQL
{
    public interface ISQLSettingsService
    {
        SQLEventSettings GetSettings(EventInstance eventInstance);
    }
}
