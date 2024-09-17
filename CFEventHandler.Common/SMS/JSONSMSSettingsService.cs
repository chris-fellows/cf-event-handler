//using CFEventHandler.JSON;
//using CFEventHandler.SignalR;

//namespace CFEventHandler.SMS
//{
//    public class JSONSMSSettingsService : JSONItemRepository<SMSEventSettings, string>,  ISMSSettingsService
//    {
//        public JSONSMSSettingsService(string folder) :
//                      base(folder,
//                          ((SMSEventSettings settings) => { return settings.Id; }),
//                          ((SMSEventSettings settings) => { settings.Id = Guid.NewGuid().ToString(); }))
//        {

//        }
//    }
//}
