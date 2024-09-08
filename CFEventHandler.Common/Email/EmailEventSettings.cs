using CFEventHandler.Models;
using System;
using System.Collections.Generic;

namespace CFEventHandler.Email
{
    /// <summary>
    /// Settings for handling event for sending email
    /// </summary>
    public class EmailEventSettings : EventSettings
    {
        public string Server { get; set; } = String.Empty;

        public int Port { get; set; } = 0;

        public string Username { get; set; } = String.Empty;

        public string Password { get; set; } = String.Empty;

        //public string EmailCreatorId { get; set; } = String.Empty;

        public string SenderAddress { get; set; } = String.Empty;

        public List<string> RecipientAddresses { get; set; }
    }
}
