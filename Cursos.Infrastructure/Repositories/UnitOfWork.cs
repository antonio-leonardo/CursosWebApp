using Cursos.CrossCutting;
using Cursos.Domain.Interfaces.Repositories;
using Cursos.Infrastructure.Context;
using Microsoft.EntityFrameworkCore.Storage;

namespace Cursos.Infrastructure.Repositories
{
    public class UnitOfWork : LoggingBase, IUnitOfWork
    {
        private readonly DataContext _context;
        private IDbContextTransaction _currentTransaction;
        public UnitOfWork(DataContext context)
        {
            Log.Info("Constructor UnitOfWork");
            _context = context;
            _currentTransaction = null;
        }
        public async Task<IDbContextTransaction> GetContextTransactionAsync()
        {
            Log.Info("Obtém transação corrente");
            if (_currentTransaction == null)
                _currentTransaction = await _context.Database.BeginTransactionAsync();

            return _currentTransaction;
        }

        public async Task RollBackAsync()
        {
            Log.Info("Faz roll-back");
            await _context.DisposeAsync();
        }

        public async Task<int> SaveAllAsync()
        {
            Log.Info("Faz commit");
            return await _context.SaveChangesAsync();
        }
    }
}