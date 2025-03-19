namespace CFEventHandler.Interfaces
{  
    /// <summary>
    /// Interface for reading list of entities
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public interface IEntityWriter<TEntity>
    {
        Task WriteAllAsync(List<TEntity> entities);
    }
}
