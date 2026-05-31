using LibrarySys.Application.Common.Behaviors;
using LibrarySys.Application.Common.DTOs.Validator.Books;
using Microsoft.Extensions.DependencyInjection;

namespace LibrarySys.Application
{
    public static class ApplicationServiceRegistration
    {
        public static IServiceCollection ApplicationConfiguration(this IServiceCollection services)
        {
            services.AddMediatR(opt =>
            {
                opt.RegisterServicesFromAssembly(typeof(ApplicationServiceRegistration).Assembly);
                //opt.AddBehavior(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
                opt.AddBehavior(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));

            });
            services.AddValidatorsFromAssemblyContaining<BookAuthRequestDtoValidator>();
            return services;
        }
    }
}
