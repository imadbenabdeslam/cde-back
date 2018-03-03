using CoreTest.Models.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreTest.Repositories.Interfaces
{
    public interface IBaseRepository<T>
        where T : Entity
    {

        Task<IQueryable<T>> GetQuery(bool asNoTracking);
        Task<IQueryable<T>> GetQueryAsNoTracking();
        Task<IEnumerable<T>> GetAll();
        Task<T> GetById(int id);
    }
}
