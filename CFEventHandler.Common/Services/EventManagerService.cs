using CFEventHandler.Enums;
using CFEventHandler.Interfaces;
using CFEventHandler.Models;

namespace CFEventHandler.Services
{
    /// <summary>
    /// Event manager service. Handles event and passes to relevant event handlers
    /// </summary>
    public class EventManagerService : IEventManagerService
    {
        private readonly List<IEventHandler> _eventHandlers;        
        private readonly IEventHandlerRuleService _eventHandlerRuleService;

        private class EventHandlingInfo
        {
            public IEventHandler EventHandler { get; set; }

            public string EventSettingsId { get; set; } = String.Empty;
        }

        public EventManagerService(IEnumerable<IEventHandler> eventHandlers,
                                   IEventHandlerRuleService eventHandlerRuleService)
        {
            _eventHandlers = eventHandlers.ToList();
            _eventHandlerRuleService = eventHandlerRuleService;
        }

        public void Handle(EventInstance eventInstance)
        {
            // Get event handlers for event
            var eventHandlers = GetEventHandlers(eventInstance);

            if (eventHandlers.Any())
            {
                // Pass event to each event handler
                var tasks = new List<Task>();
                foreach (var eventHandler in eventHandlers)
                {
                    var task = HandleAsync(eventInstance, eventHandler);
                    tasks.Add(task);
                }

                // Wait for completion
                Task.WaitAll(tasks.ToArray());
            }
        }

        /// <summary>
        /// Returns IEventHandler(s) to handle event
        /// </summary>
        /// <param name="eventInstance"></param>
        /// <returns></returns>
        private List<EventHandlingInfo> GetEventHandlers(EventInstance eventInstance)
        {            
            var results = new List<EventHandlingInfo>();

            // Get event handler rules
            // TODO: Cache this
            var eventHandlerRules = _eventHandlerRuleService.GetAllAsync().Result;

            // Check enabled rules
            foreach(var eventHandlerRule in eventHandlerRules.Where(ehr => ehr.Enabled && ehr.EventTypeId == eventInstance.EventTypeId))
            {                
                results.Add(new EventHandlingInfo()
                {
                    EventHandler = _eventHandlers.First(eh => eh.Id == eventHandlerRule.EventHandlerId),
                    EventSettingsId = eventHandlerRule.EventSettingsId                    
                });
            }

            return results.OrderBy(r => r.EventHandler.Id).ToList();   // Consistent order
        }
       
        /// <summary>
        /// Handles event asynchronously
        /// </summary>
        /// <param name="eventInstance"></param>
        /// <param name="eventHandler"></param>
        /// <returns></returns>
        private Task HandleAsync(EventInstance eventInstance, EventHandlingInfo eventHandlingInfo)
        {
            var task = Task.Factory.StartNew(() =>
            {
                eventHandlingInfo.EventHandler.Handle(eventInstance, eventHandlingInfo.EventSettingsId);
            });
            return task;
        }
    }
}
