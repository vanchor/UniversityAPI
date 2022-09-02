namespace UniversityApi.Repositories
{
    public interface IRepository<T> 
        where T : class
    {
        Task<IEnumerable<T>> Get();
        Task<T> Get(int id);
        Task<T> Create(T item);
        Task Update(T item);
        Task Delete(int id);
    }
}
