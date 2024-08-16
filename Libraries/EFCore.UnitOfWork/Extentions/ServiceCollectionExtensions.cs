
using Data.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace EFCore.UnitOfWorkCore.Extentions
{
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Sets up EFCore UnitOfWork.
        /// </summary>
        public static void AddEFCoreUnitOfWork(this IServiceCollection services)
        {
            services.AddTransient<IUnitOfWorkCore, UnitOfWorkCore>();
        }
    }
}
