using CFEventHandler.Interfaces;
using CFEventHandler.Models;
using System.Collections.Generic;

namespace CFEventHandler.Email
{
    public class EmailSettingsService : IEmailSettingsService
    {
        // Just one set of server credentials. We could support multiple.
        private readonly string _server;
        private readonly int _port;
        private readonly string _username;
        private readonly string _password;
        private readonly Dictionary<string, string> _emailCreatorIdByEventTypeId;

        public EmailSettingsService(string server, int port, string username, string password,
                            Dictionary<string, string> emailCreatorIdByEventTypeId)
        {
            _server = server;
            _port = port;
            _username = username;
            _password = password;
            _emailCreatorIdByEventTypeId = emailCreatorIdByEventTypeId;
        }

        public EmailEventSettings GetSettings(EventInstance eventInstance)
        {
            // Do nothing if no IEmailCreator defined
            if (!_emailCreatorIdByEventTypeId.ContainsKey(eventInstance.EventTypeId)) return null;

            var settings = new EmailEventSettings()
            {
                Server  = _server,
                Port = _port,
                Username = _username,
                Password = _password,                
                EmailCreatorId = _emailCreatorIdByEventTypeId[eventInstance.EventTypeId],
                RecipientAddresses = new List<string>() { "chrisfellows90@gmail.com" },
                SenderAddress = "chrismfellows@hotmail.co.uk"
            };             

            return settings;
        }
    }
}
