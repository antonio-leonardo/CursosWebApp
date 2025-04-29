using Microsoft.EntityFrameworkCore.Storage;

namespace Cursos.Domain.Interfaces.Repositories
{
    public interface IUnitOfWork
    {
        Task<int> SaveAllAsync();
        Task RollBackAsync();
        Task<IDbContextTransaction> GetContextTransactionAsync();
    }
}