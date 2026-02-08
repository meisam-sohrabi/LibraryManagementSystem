using LibrarySys.Application.Contract;
using LibrarySys.Application.Contract.BookAuthorService;
using LibrarySys.Application.Contract.Infrastructure;
using LibrarySys.Infrastructure.EntityFrameworkCore.Context;
using LibrarySys.Infrastructure.EntityFrameworkCore.Repository;
using LibrarySys.Infrastructure.EntityFrameworkCore.Service;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace LibrarySys.Infrastructure.EntityFrameworkCore
{
    public static class InfrastructureServiceRegistration
    {
        public static IServiceCollection InfrastructureConfiguration(this IServiceCollection services
            , IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(opt =>
            {
                opt.UseSqlServer(configuration["ConnectionStrings:LibraryManagementConnection"]);
            });

            services.AddScoped(typeof(IGenericRepositry<>), typeof(GenericRepository<>));
            services.AddScoped<IAuthorRepository, AuthorRepository>();
            services.AddScoped<IBookRepository, BookRepository>();
            services.AddScoped<IBorrowingRepository, BorrowingRepository>();
            services.AddScoped<IMemberRepository, MemberRepository>();
            services.AddScoped<IBookAuthorService, BookAuthorService>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            return services;
        }
    }
}
