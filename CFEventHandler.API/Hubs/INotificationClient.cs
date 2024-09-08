namespace CFEventHandler.API.Hubs
{
    public interface INotificationClient
    {
        Task ReceiveMessage(string user, string message);
    }
}
