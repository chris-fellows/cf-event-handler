// See https://aka.ms/new-console-template for more information
using CFEventHandler.Notifications;

// Set notification config
var notificationConfig = new NotificationConfig()
{
    HubURL = "https://localhost:7034/notificationhub"
};

var cancellationSource = new CancellationTokenSource();
var notificationHandler = new NotificationHandler();
try
{
    notificationHandler.Run(cancellationSource.Token, notificationConfig);
}
catch (Exception exception)
{
    Console.WriteLine($"Error: {exception.Message}");
}

Console.WriteLine("Press a key");
string output = Console.ReadLine();

int xxx = 1000;