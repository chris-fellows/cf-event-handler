namespace CFEventHandler.Interfaces
{
    public interface ICacheService<TEntityType, TIDType>
    {
        IEnumerable<TEntityType> GetAll();

        void Add(TEntityType entity, TIDType id);

        TEntityType? GetById(TIDType id);

        void DeleteById(TIDType id);

        void DeleteAll();
    }
}
