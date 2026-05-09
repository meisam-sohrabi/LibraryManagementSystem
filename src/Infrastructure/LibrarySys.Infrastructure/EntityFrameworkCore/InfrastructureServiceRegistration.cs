using LibrarySys.Application.Common.Interfaces;
using LibrarySys.Application.Common.Interfaces.BookAuthorService;
using LibrarySys.Application.Common.Interfaces.FileService;
using LibrarySys.Application.Common.Interfaces.Infrastructure.AuthorContract;
using LibrarySys.Application.Common.Interfaces.Infrastructure.BookContract;
using LibrarySys.Application.Common.Interfaces.Infrastructure.BorrowingContract;
using LibrarySys.Application.Common.Interfaces.Infrastructure.GenericContract;
using LibrarySys.Application.Common.Interfaces.Infrastructure.MemberContract;
using LibrarySys.Application.Common.Interfaces.Infrastructure.PermissionContract;
using LibrarySys.Application.Common.Interfaces.Infrastructure.RefreshTokenContract;
using LibrarySys.Application.Common.Interfaces.Infrastructure.UserContract;
using LibrarySys.Application.Common.Interfaces.Infrastructure.UserPermissionContract;
using LibrarySys.Application.Common.Interfaces.Token;
using LibrarySys.Application.Common.Interfaces.CurrentUser;
using LibrarySys.Infrastructure.EntityFrameworkCore.Context;
using LibrarySys.Infrastructure.EntityFrameworkCore.Repository;
using LibrarySys.Infrastructure.EntityFrameworkCore.Service;
using LibrarySys.Infrastructure.Interceptor;
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
                opt.AddInterceptors(new AuditInterceptor());
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
