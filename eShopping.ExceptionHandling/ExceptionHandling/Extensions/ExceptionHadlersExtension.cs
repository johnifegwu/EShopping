
using Microsoft.Extensions.DependencyInjection;

namespace eShopping.ExceptionHandling
{
    public static class ExceptionHadlersExtension
    {

        public static void AddExceptionHadlers(this IServiceCollection services)
        {
            services.AddExceptionHandler<BadRequestExceptionHandler>();
            services.AddExceptionHandler<ValidationExceptionHandler>();
            services.AddExceptionHandler<NotFoundExceptionHandler>();
            services.AddExceptionHandler<GlobalExceptionHandler>();
            services.AddProblemDetails();
        }
    }
}
