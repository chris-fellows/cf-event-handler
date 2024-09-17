namespace CFEventHandler.Interfaces
{
    public interface IDatabaseConfig
    {
        string ConnectionString { get; }

        string DatabaseName { get; }
    }
}
