namespace FlatFileGenerator.DataAccess.Interfaces
{
    public interface IGuidRepository<T>
    {
        IEnumerable<T> GetList(int take);
        IQueryable<T> GetQueryList();
        T? Get(Guid id);
        void Add(T entity);
        void Delete(Guid id);
        void Update(T entity);
    }
}
