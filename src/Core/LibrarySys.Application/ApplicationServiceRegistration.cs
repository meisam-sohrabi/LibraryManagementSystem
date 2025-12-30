using FluentValidation;
using LibrarySys.Application.DTOs.Validator.Books;
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
            });
            services.AddValidatorsFromAssemblyContaining<BookAuthRequestDtoValidator>();
            return services;
        }
    }
}
