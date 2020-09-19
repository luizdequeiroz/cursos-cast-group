# cursos-cast-group

# Usei arquitetura DDD

Na qual tem-se 
- domain: onde está dbContext, entidades, attributes, enums etc
- repository: onde se encontra um generic repository
```csharp
  public interface IGenericRepository<E>
  {
      Task<E> InsertAsync(E entity);
      Task<IQueryable<E>> SelectAllAsync();
      Task<E> SelectByIdAsync(int id);
      Task<E> UpdateAsync(E entity);
      Task DeleteAsync(E entity);
      Task<IQueryable<E>> SelectWhereAsync(Expression<Func<E, bool>> expression);
  }
```
cuja implementação usa a manipulação de dados a partir do DbContext definido em domain

- service: onde está a implementação base do CRUD, tendo como dependência o repository, porém com declarações da classe abstrata de servico com o modificador virtual, a fim de permitir sobrescritas de métodos do CRUD (há exemplo de override para atender às regras definidas no documento de requisito).
- app: a exposição da API Restful se encontra aqui, bem como a injeção das dependências, conhecendo apenas o método de extensão que injeta a camada de serviços

PS.: em service e repository foram implementadas classes que provém extensões para injeção de dependência das classes definidas em cada, desacoplando totalmente e respeitando princípio de camadas

em repository:
```csharp
  namespace repository
  {
    public static class Injector
    {
      public static void InjectRepositories(this IServiceCollection services)
      {
          services.AddTransient<IGenericRepository<Curso>, GenericRepository<Curso>>();
          services.AddTransient<IGenericRepository<Categoria>, GenericRepository<Categoria>>();
      }
    }
  }
```
em service:
```csharp
  namespace service
  {
      public static class Injector
      {
          public static void InjectServices(this IServiceCollection services, IConfiguration configuration)
          {
              services.InjectRepositories();

              services.AddTransient<ICursoService, CursoService>();
              services.AddTransient<ICategoriaService, CategoriaService>();
          }
      }
  }
```

no Startup.cs (app):
```csharp
  services.InjectServices(Configuration);
```
