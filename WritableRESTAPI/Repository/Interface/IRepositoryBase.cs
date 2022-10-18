using System.Threading.Tasks;

namespace WritableRESTAPI.Repository.Interface
{
    public interface IRepositoryBase<T>
    {
        Task<T> GetAsync(int id);

        Task<T> InsertAsync(T entity);

        Task<T> UpdateAsync(int id, T entity);

        Task<T> DeleteAsync(int id);
    }
}
