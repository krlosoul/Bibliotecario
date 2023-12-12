
namespace PruebaIngresoBibliotecario.Infrastructure.DataAccess
{
    using Microsoft.EntityFrameworkCore.Storage;
    using PruebaIngresoBibliotecario.Business.Contract.DataAccess;
    using PruebaIngresoBibliotecario.Core.Entities;
    using System.Threading.Tasks;

    public class UnitOfWork : IUnitOfWork
    {
        #region Properties
        public BibliotecaContext DbContext { get; set; }

        private IDbContextTransaction? _transaction;

        private IRepository<Loan>? _LoanRepository;
        #endregion

        public UnitOfWork(BibliotecaContext dbContext)
        {
            DbContext = dbContext;
        }

        #region Transactions
        private async Task SaveAsync()
        {
            await DbContext.SaveChangesAsync();
        }

        public async Task BeginTransactionAsync()
        {
            _transaction ??= await DbContext.Database.BeginTransactionAsync();
        }

        public async Task CommitTransactionAsync()
        {
            if (_transaction != null)
            {
                await _transaction.CommitAsync();
                await SaveAsync();
            }
        }

        public async Task CloseTransactionAsync()
        {
            if (_transaction != null)
            {
                await _transaction.DisposeAsync();
            }
        }

        public async Task RollbackTransactionAsync()
        {
            if (_transaction != null)
            {
                await _transaction.RollbackAsync();
                await _transaction.DisposeAsync();
            }
        }
        #endregion

        #region Repositories       
        public IRepository<Loan> LoanRepository
        {
            get
            {
                return _LoanRepository ??= new Repository<Loan>(DbContext);
            }
        }
        #endregion
    }
}
