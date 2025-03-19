namespace CFEventHandler.Interfaces
{
    /// <summary>
    /// Interface for reading list of entities
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public interface IEntityReader<TEntity>
    {
        Task<List<TEntity>> ReadAllAsync();        
    }
}
