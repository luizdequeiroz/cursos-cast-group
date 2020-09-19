using domain.Entities;
using repository;

namespace service.Implementations
{
    public class CategoriaService : GenericService<Categoria>, ICategoriaService
    {
        public CategoriaService(IGenericRepository<Categoria> repository) : base(repository)
        {
        }
    }
}
