using Microsoft.Extensions.Logging.Abstractions;
using System.Linq.Expressions;

namespace ToothCareAPI.Repository.IRepository
{
    public interface IRepository<T> where T : class
    {
        Task CreatAsync(T entity);
        Task SaveAsync();
        Task DeleteAsync(T entity);
        Task<List<T>> GetAllAsync(Expression<Func<T,bool>>? filter=null,string? includeProperties = null, int pageSize = 0, int pageNumber = 1);
        Task<T> GetAsync(Expression<Func<T, bool>> filter = null, bool tracked = true, string? includeProperties = null);


    }
}
