using System.Collections.Generic;
using System.Threading.Tasks;

namespace Data.Repository
{
    public interface IBaseRepository<TEntity> where TEntity : class
    {
        Task Create(TEntity entity);
        void Delete(string id);
        Task DeleteAllDocuments();
        Task<IEnumerable<TEntity>> Get();
        Task<TEntity> Get(string id);
        void Update(TEntity entity);
    }
}