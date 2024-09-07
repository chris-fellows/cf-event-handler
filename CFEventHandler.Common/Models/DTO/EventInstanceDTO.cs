﻿namespace CFEventHandler.Models.DTO
{
    public class EventInstanceDTO
    { 
        // <summary>
        /// Unique Id
        /// </summary>
        public string Id { get; set; } = Guid.NewGuid().ToString();

        /// <summary>
        /// Event type
        /// </summary>
        public string EventTypeId { get; set; } = String.Empty;

        /// <summary>
        /// Client that created event
        /// </summary>
        public string EventClientId { get; set; } = String.Empty;

        /// <summary>
        /// Time created
        /// </summary>
        public DateTimeOffset CreatedDateTime { get; set; } = DateTimeOffset.UtcNow;

        /// <summary>
        /// Parameters
        /// </summary>
        public List<EventParameterDTO> Parameters { get; set; }
    }
}
