using domain.Entities;
using domain.Enums;
using repository;
using System;
using System.Threading.Tasks;

namespace service.Implementations
{
    public class CursoService : GenericService<Curso>, ICursoService
    {
        public CursoService(IGenericRepository<Curso> repository) : base(repository)
        {
        }

        public override async Task<(Code code, Curso result)> SetNewAsync(Curso entity)
        {
            var samePeriod = await GetWhereAsync(curso => curso.DataInicio >= entity.DataInicio && curso.DataTermino <= entity.DataTermino);

            if (samePeriod.code == Code.SUCCESS)
                return (Code.BUSY_PERIOD, null);

            if (entity.DataInicio > entity.DataTermino)
                return (Code.INVALID_DATE_RANGE, null);

            if (entity.DataInicio < DateTime.Now)
                return (Code.INVALID_START_DATE, null);

            return await base.SetNewAsync(entity);
        }

        public override async Task<(Code code, Curso result)> AlterAsync(Curso entity)
        {
            var samePeriod = await GetWhereAsync(curso => curso.DataInicio >= entity.DataInicio && curso.DataTermino <= entity.DataTermino);

            if (samePeriod.code == Code.SUCCESS)
                return (Code.BUSY_PERIOD, null);

            if (entity.DataInicio > entity.DataTermino)
                return (Code.INVALID_DATE_RANGE, null);

            if (entity.DataInicio < DateTime.Now)
                return (Code.INVALID_START_DATE, null);

            return await base.AlterAsync(entity);
        }
    }
}
