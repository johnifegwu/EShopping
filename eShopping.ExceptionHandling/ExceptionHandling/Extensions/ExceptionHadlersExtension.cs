
using Microsoft.Extensions.DependencyInjection;

namespace eShopping.ExceptionHandling
{
    public static class ExceptionHadlersExtension
    {

        public static void AddExceptionHadlers(this IServiceCollection services)
        {
            services.AddExceptionHandler<GlobalExceptionHandler>();
            services.AddExceptionHandler<BadRequestExceptionHandler>();
            services.AddExceptionHandler<NotFoundExceptionHandler>();
            services.AddExceptionHandler<ValidationExceptionHandler>();
            services.AddProblemDetails();
        }
    }
}
