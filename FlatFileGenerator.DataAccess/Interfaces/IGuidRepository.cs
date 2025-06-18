namespace FlatFileGenerator.DataAccess.Interfaces
{
    public interface IGuidRepository<T>
    {
        Task <IEnumerable<T>> GetList(int take);
        IQueryable<T> GetQueryList();
        T? Get(Guid id);
        void Add(T entity);
        Task AddRange(IEnumerable<T> entities);
        void Delete(Guid id);
        void Update(T entity);
    }
}
