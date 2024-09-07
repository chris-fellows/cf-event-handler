using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.DependencyInjection;

namespace CFEventHandler.Notifications
{
    /// <summary>
    /// Handles notifications
    /// </summary>
    internal class NotificationHandler
    {
        public void Run(System.Threading.CancellationToken cancellationToken, NotificationConfig notificationConfig)
        {          
            System.Console.WriteLine($"{DateTimeOffset.Now.ToString()} Connecting to {notificationConfig.HubURL}");

            var connection = new HubConnectionBuilder()
             .WithUrl(notificationConfig.HubURL, options =>
             {
                 //options.Headers.Add("X-Api-Key", apiKey);
             })
             .AddJsonProtocol(config =>
             {
                 var jsonOptions = new System.Text.Json.JsonSerializerOptions
                 {
                     PropertyNameCaseInsensitive = true,
                 };
                 config.PayloadSerializerOptions = jsonOptions;
             })
             .Build();           

            // Set handler for status message
            connection.On<string>("Status", (message) =>
            {
                System.Console.WriteLine($"{DateTimeOffset.Now.ToString()} Status: {message}");
            });

            // Set handler for test message
            connection.On<object>("Event", (eventInstance) =>
            {
                System.Console.WriteLine($"{DateTimeOffset.Now.ToString()} Event: {eventInstance}");
            });

            connection.StartAsync().ContinueWith(async task =>
            {
                if (task.IsFaulted)
                {
                    System.Console.WriteLine($"{DateTimeOffset.Now.ToString()} Error connecting to API");
                }               
            }).Wait();

            // Run until shutdown
            while (!cancellationToken.IsCancellationRequested)
            {
                System.Threading.Thread.Yield();
                System.Threading.Thread.Sleep(5);
            }

            int xxx = 100;
        }
    }
}
