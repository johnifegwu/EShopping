using Catalog.Infrastructure.Persistence;
using Data.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Catalog.Infrastructure.Repositories
{
    internal class UnitOfWorkCatalog : IUnitOfWorkCore
    {
        private static readonly object _createRepositoryLock = new object();

        private readonly Dictionary<Type, object> _repositories = new Dictionary<Type, object>();

        private readonly DbContext _context;

        public UnitOfWorkCatalog(CatalogDbContext context)
        {
            this._context = context;
        }

        public DbContext GetContext()
        {
            return this._context;
        }

        public IRepository<T> Repository<T>() where T : class
        {
            if (!_repositories.ContainsKey(typeof(T)))
            {
                lock (_createRepositoryLock)
                {
                    if (!_repositories.ContainsKey(typeof(T)))
                    {
                        CreateRepository<T>();
                    }
                }
            }

            return _repositories[typeof(T)] as IRepository<T>;
        }
        private void CreateRepository<T>() where T : class
        {
            _repositories.Add(typeof(T), new Repository<T>(_context));
        }

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return await _context.SaveChangesAsync(cancellationToken);
        }

        private bool disposed = false;

        protected void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

    }
}