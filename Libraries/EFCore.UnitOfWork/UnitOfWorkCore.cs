
using Data.Repositories;
using EFCore.UnitOfWorkCore.Interfaces;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace EFCore.UnitOfWorkCore
{
    internal sealed class UnitOfWorkCore : IUnitOfWorkCore
    {
        private static readonly object _createRepositoryLock = new object();

        private readonly Dictionary<Type, object> _repositories = new Dictionary<Type, object>();

        private readonly IJayDbContext _context;

        public UnitOfWorkCore(IJayDbContext context)
        {
            this._context =  context;
        }

        /// <summary>
        /// Gets the DbContext Database Facade.
        /// </summary>
        /// <returns></returns>
        public DatabaseFacade GetDatabase()
        {
            return _context.Database;
        }

        /// <summary>
        /// Dynamic Repository.
        /// </summary>
        /// <typeparam name="T">Type</typeparam>
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

        /// <summary>
        /// Persists all changes to the database.
        /// </summary>
        /// <returns></returns>
        public int SaveChanges()
        {
            return _context.SaveChanges();
        }

        /// <summary>
        /// Persists all changes to the database.
        /// </summary>
        /// <param name="cancellationToken">Cancellation Token.</param>
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