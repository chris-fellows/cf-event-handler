using CFEventHandler.Enums;
using CFEventHandler.Interfaces;
using EventHandler = CFEventHandler.Models.EventHandler;

namespace CFEventHandler.Seed
{
    /// <summary>
    /// Event handler seed example 1
    /// </summary>
    public class EventHandlerSeed1 : IEntityList<EventHandler>
    {
        public async Task<List<EventHandler>> ReadAllAsync()
        {
            var eventHandlers = new List<EventHandler>();

            eventHandlers.Add(new EventHandler()
            {
                Id = "1",
                Name = "Console",
                EventHandlerType = EventHandlerTypes.Console
            });

            eventHandlers.Add(new EventHandler()
            {
                Id = "2",
                Name = "CSV",
                EventHandlerType = EventHandlerTypes.CSV
            });

            eventHandlers.Add(new EventHandler()
            {
                Id = "3",
                Name = "Datadog event",
                EventHandlerType = EventHandlerTypes.DatadogEvent
            });

            eventHandlers.Add(new EventHandler()
            {
                Id = "4",
                Name = "Datadog metric",
                EventHandlerType = EventHandlerTypes.DatadogMetric
            });

            eventHandlers.Add(new EventHandler()
            {
                Id = "5",
                Name = "Email",
                EventHandlerType = EventHandlerTypes.Email
            });

            eventHandlers.Add(new EventHandler()
            {
                Id = "6",
                Name = "HTTP",
                EventHandlerType = EventHandlerTypes.HTTP
            });

            eventHandlers.Add(new EventHandler()
            {
                Id = "7",
                Name = "Jira issue",
                EventHandlerType = EventHandlerTypes.JiraIssue
            });

            eventHandlers.Add(new EventHandler()
            {
                Id = "8",
                Name = "Process",
                EventHandlerType = EventHandlerTypes.Process
            });

            eventHandlers.Add(new EventHandler()
            {
                Id = "9",
                Name = "SMS",
                EventHandlerType = EventHandlerTypes.SMS
            });

            eventHandlers.Add(new EventHandler()
            {
                Id = "10",
                Name = "SQL database",
                EventHandlerType = EventHandlerTypes.SQL
            });

            eventHandlers.Add(new EventHandler()
            {
                Id = "11",
                Name = "Teams channel",
                EventHandlerType = EventHandlerTypes.TeamsChannel
            });

            return eventHandlers;
        }

        public async Task WriteAllAsync(List<EventHandler> eventHandlers)
        {
            // No action
        }
    }
}
