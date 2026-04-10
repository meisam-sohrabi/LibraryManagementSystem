using LibrarySys.Application.Contract;
using LibrarySys.Application.Contract.BookAuthorService;
using LibrarySys.Application.Contract.CurrentUser;
using LibrarySys.Application.Contract.FileService;
using LibrarySys.Application.Contract.Infrastructure.AuthorContract;
using LibrarySys.Application.Contract.Infrastructure.BookContract;
using LibrarySys.Application.Contract.Infrastructure.BorrowingContract;
using LibrarySys.Application.Contract.Infrastructure.GenericContract;
using LibrarySys.Application.Contract.Infrastructure.MemberContract;
using LibrarySys.Application.Contract.Infrastructure.PermissionContract;
using LibrarySys.Application.Contract.Infrastructure.RefreshTokenContract;
using LibrarySys.Application.Contract.Infrastructure.UserContract;
using LibrarySys.Application.Contract.Infrastructure.UserPermissionContract;
using LibrarySys.Application.Contract.Token;
using LibrarySys.Infrastructure.EntityFrameworkCore.Context;
using LibrarySys.Infrastructure.EntityFrameworkCore.Repository;
using LibrarySys.Infrastructure.EntityFrameworkCore.Service;
using LibrarySys.Infrastructure.Jobs;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Quartz;

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

            services.AddQuartz(opt =>
            {
                JobKey jobKey = new JobKey("Seed-Data");
                opt.AddJob<SeedDataJob>(o => o.WithIdentity(jobKey));
                opt.AddTrigger(o => o.ForJob(jobKey).WithIdentity("Seed-Data-Triger")
                .StartNow()
                .WithSimpleSchedule(o => o.WithIntervalInSeconds(1)));

            });

            services.AddQuartzHostedService(c =>
            {
                c.WaitForJobsToComplete = true;
            });

            services.AddScoped(typeof(IGenericRepositry<>), typeof(GenericRepository<>));
            services.AddScoped<IAuthorRepository, AuthorRepository>();
            services.AddScoped<IUserManagerRepository,UserManagerRepository>();
            services.AddScoped<IBookRepository, BookRepository>();
            services.AddScoped<IBorrowingRepository, BorrowingRepository>();
            services.AddScoped<IMemberRepository, MemberRepository>();
            services.AddScoped<IBookAuthorService, BookAuthorService>();
            services.AddScoped<IPermissionRepository, PermissionRepository>();
            services.AddScoped<IUserPermissionRepository,UserPermissionRepository>();
            services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();
            services.AddScoped<IFileStorageService,LocalFileStorageService>();
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            return services;
        }
    }
}
