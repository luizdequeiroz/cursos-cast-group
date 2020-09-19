using domain.Attributes;
using domain.Entities;
using domain.Enums;
using repository;
using repository.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace service.Implementations
{
    public class GenericService<E> : IGenericService<E> where E : Entity
    {
        protected IGenericRepository<E> repository;

        protected GenericService(IGenericRepository<E> repository)
        {
            this.repository = repository;
        }

        public virtual async Task<(Code code, E result)> AlterAsync(E entity)
        {
            var original = await GetByIdAsync(entity.Id);

            if (original.code != Code.SUCCESS)
                return (Code.ITEM_DOES_NOT_EXIST, null);

            var exceptions = new List<string>();
            entity.GetType().GetProperties().ToList().ForEach(prop =>
            {
                if (prop.GetCustomAttributes(typeof(NotSetAutomaticallyAttribute), true).Length > 0)
                {
                    exceptions.Add(prop.Name);
                }

                var value = prop.GetValue(entity);
                bool isUnsetted = false;

                switch (prop.PropertyType.Name)
                {
                    case "String":
                        isUnsetted = string.IsNullOrEmpty(value?.ToString());
                        break;
                    case "Int32":
                        isUnsetted = Convert.ToInt32(value) == default(int);
                        break;
                    case "Double":
                        isUnsetted = Convert.ToDouble(value) == default(double);
                        break;
                    case "DateTime":
                        isUnsetted = Convert.ToDateTime(value) == default(DateTime);
                        break;
                    default: break;
                }

                if (isUnsetted) exceptions.Add(prop.Name);
            });

            original.result.SetDifferentProperties(entity, exceptions.ToArray());
            original.result.UpdateDate = DateTime.Now;
            return (Code.SUCCESS, await repository.UpdateAsync(original.result));
        }

        public virtual async Task<(Code code, bool result)> DeleteAsync(int id)
        {
            E result = (await GetByIdAsync(id)).result;
            if (result == null) return (Code.ITEM_NOT_DELETED, false);
            await repository.DeleteAsync(result);

            return (Code.SUCCESS, true);
        }

        public virtual async Task<(Code code, IList<E> result)> GetAllAsync()
        {
            var all = await repository.SelectAllAsync();

            if (all == null)
                return (Code.UNLISTED_ITEMS, null);

            if (all.Count() == 0)
                return (Code.ITENS_NOT_FOUND, all.ToList());

            return (Code.SUCCESS, all.ToList());
        }

        public virtual async Task<(Code code, E result)> GetByIdAsync(int id)
        {
            E result = await repository.SelectByIdAsync(id);

            if (result == null)
                return (Code.ITEM_DOES_NOT_EXIST, null);

            return (Code.SUCCESS, result);
        }

        public virtual async Task<(Code code, E result)> SetNewAsync(E entity)
        {
            entity.RegisterDate = DateTime.Now;
            E inserted = await repository.InsertAsync(entity);
            return (Code.SUCCESS, inserted);
        }

        public virtual async Task<(Code code, IQueryable<E> result)> GetWhereAsync(Expression<Func<E, bool>> expression)
        {
            var result = await repository.SelectWhereAsync(expression);

            if (result == null)
                return (Code.UNLISTED_ITEMS, null);

            if (result.Count() == 0)
                return (Code.ITENS_NOT_FOUND, result);

            return (Code.SUCCESS, result);
        }
    }
}
