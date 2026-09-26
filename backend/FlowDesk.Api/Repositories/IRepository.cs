namespace FlowDesk.Api.Repositories
{
    public interface IRepository<T> where T : class
    {
        Task<T?> GetByIdAsync(int id);

        Task<List<T>> GetAllAsync();

        IQueryable<T> Query();

        Task AddAsync(T entity);

        void Update(T entity);

        void Delete(T entity);

        Task<int> SaveChangesAsync();
    }
}
