using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace repository
{
    public interface IGenericRepository<E>
    {
        Task<E> InsertAsync(E entity);
        Task<IQueryable<E>> SelectAllAsync();
        Task<E> SelectByIdAsync(int id);
        Task<E> UpdateAsync(E entity);
        Task DeleteAsync(E entity);
        Task<IQueryable<E>> SelectWhereAsync(Expression<Func<E, bool>> expression);
    }
}
