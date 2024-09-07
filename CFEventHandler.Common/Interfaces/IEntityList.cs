namespace CFEventHandler.Interfaces
{
    /// <summary>
    /// Interface for accessing list of entities. We can read or write all entities.
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public interface IEntityList<TEntity>
    {
        Task<List<TEntity>> ReadAllAsync();

        Task WriteAllAsync(List<TEntity> entities);
    }
}
