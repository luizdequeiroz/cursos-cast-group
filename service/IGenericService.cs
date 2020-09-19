using domain.Entities;
using domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace service
{
    public interface IGenericService<E> where E : Entity
    {
        Task<(Code code, E result)> SetNewAsync(E entity);
        Task<(Code code, IList<E> result)> GetAllAsync();
        Task<(Code code, E result)> GetByIdAsync(int id);
        Task<(Code code, E result)> AlterAsync(E entity);
        Task<(Code code, bool result)> DeleteAsync(int id);
        Task<(Code code, IQueryable<E> result)> GetWhereAsync(Expression<Func<E, bool>> expression);
    }
}
