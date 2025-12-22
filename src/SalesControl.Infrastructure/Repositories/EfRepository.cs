using Ardalis.Specification.EntityFrameworkCore;
using SalesControl.Domain;
using SalesControl.Infrastructure.Persistence;
using SalesControl.Application.Interfaces;

namespace SalesControl.Infrastructure.Repositories
{
    /// <summary>
    /// Repositório genérico baseado em Ardalis.Specification.RepositoryBase.
    /// Implementa a interface de domínio `IRepository<T>`.
    /// </summary>
    /// <typeparam name="T">Entidade agregada</typeparam>
    public class EfRepository<T> : RepositoryBase<T>, IRepository<T>
        where T : class, IAggregateRoot
    {
        private readonly AppDbContext _dbContext;

        public EfRepository(AppDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        // Implementações específicas podem ser adicionadas aqui se necessário.
    }
}