using CFEventHandler.Interfaces;
using CFEventHandler.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CFEventHandler.Services
{
    /// <summary>
    /// Event manager service. Handles event and passes to relevant event handlers
    /// </summary>
    public class EventManagerService : IEventManagerService
    {
        private readonly List<IEventHandler> _eventHandlers;
        private readonly List<EventHandlerRule> _eventHandlerRules;

        public EventManagerService(List<IEventHandler> eventHandlers,
                                List<EventHandlerRule> eventHandlerRules)   
        {
            _eventHandlers = eventHandlers;
            _eventHandlerRules = eventHandlerRules;
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
        private List<IEventHandler> GetEventHandlers(EventInstance eventInstance)
        {
            var eventHandlers = new List<IEventHandler>();

            foreach(var eventHandlerRule in _eventHandlerRules.Where(ehr => ehr.EventTypeId == eventInstance.EventTypeId))
            {
                foreach(var eventHandlerId in eventHandlerRule.EventHandlerIds)
                {
                    if (!eventHandlers.Any(eh => eh.Id == eventHandlerId))
                    {
                        eventHandlers.Add(_eventHandlers.First(eh => eh.Id == eventHandlerId));
                    }
                }
            }

            return eventHandlers.OrderBy(eh => eh.Id).ToList(); // Consistent order
        }
       
        /// <summary>
        /// Handles event asynchronously
        /// </summary>
        /// <param name="eventInstance"></param>
        /// <param name="eventHandler"></param>
        /// <returns></returns>
        private Task HandleAsync(EventInstance eventInstance, IEventHandler eventHandler)
        {
            var task = Task.Factory.StartNew(() =>
            {
                eventHandler.Handle(eventInstance);
            });
            return task;
        }
    }
}
