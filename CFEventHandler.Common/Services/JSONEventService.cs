//using CFEventHandler.Interfaces;
//using CFEventHandler.JSON;
//using CFEventHandler.Models;

//namespace CFEventHandler.Services
//{
//    public class JSONEventService : JSONItemRepository<EventInstance, string>, IEventService
//    {
//        public JSONEventService(string folder) :
//                   base(folder,
//                       ((EventInstance eventInstance) => { return eventInstance.Id; }),
//                       ((EventInstance eventInstance) => { eventInstance.Id = Guid.NewGuid().ToString(); }))
//        {

//        }

//        public async Task<List<EventInstance>> GetByFilter(EventFilter eventFilter)
//        {
//            var events = base.GetByFilter((eventInstance) =>
//            {
//                return eventFilter.IsValidForFilter(eventInstance);
//            }).ToList();

//            return events;
//        }
//    }
//}
