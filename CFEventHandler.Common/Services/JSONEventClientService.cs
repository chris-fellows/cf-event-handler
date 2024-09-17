//using CFEventHandler.Interfaces;
//using CFEventHandler.JSON;
//using CFEventHandler.Models;

//namespace CFEventHandler.Services
//{
//    /// <summary>
//    /// Service for event client instances
//    /// </summary>
//    public class JSONEventClientService : JSONItemRepository<EventClient, string>, IEventClientService
//    {
//        public JSONEventClientService(string folder) :
//                 base(folder,
//                     ((EventClient eventClient) => { return eventClient.Id; }),
//                     ((EventClient eventClient) => { eventClient.Id = Guid.NewGuid().ToString(); }))
//        {

//        }       
//    }
//}
