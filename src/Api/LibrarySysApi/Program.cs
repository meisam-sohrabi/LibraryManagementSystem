using LibrarySys.Application;
using LibrarySys.Application.Common.DTOs;
using LibrarySys.Application.Common.DTOs.Validator.Books;
using LibrarySys.Application.Common.Interfaces.CurrentUser;
using LibrarySys.Application.Common.Interfaces.IdentityService;
using LibrarySys.Application.Features.Books.Request.Command;
using LibrarySys.Application.Option;
using LibrarySys.Identity;
using LibrarySys.Identity.Service;
using LibrarySys.Infrastructure.EntityFrameworkCore;
using LibrarySysApi.Attributes;
using LibrarySysApi.CurrentUser;
using LibrarySysApi.Middleware;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Scalar.AspNetCore;
using Serilog;
using Serilog.Sinks.Elasticsearch;
using System.Text;
var builder = WebApplication.CreateBuilder(args);


// Add services to the container.


// Add serilog using elastic search with kibana


Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .CreateLogger();


builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
//builder.Services.AddOpenApi(options =>
//{
//    options.AddDocumentTransformer((document, context, cancellationToken) =>
//    {
//        document.Components ??= new();
//        document.Components.SecuritySchemes.Add("Bearer", new OpenApiSecurityScheme
//        {
//            Type = SecuritySchemeType.Http,
//            Scheme = "Bearer",
//            BearerFormat = "JWT"
//        });

//        document.SecurityRequirements.Add(new OpenApiSecurityRequirement
//        {
//            {
//                new OpenApiSecurityScheme
//                {
//                    Reference = new OpenApiReference
//                    {
//                        Type = ReferenceType.SecurityScheme,
//                        Id = "Bearer"
//                    }
//                },
//                Array.Empty<string>()
//            }
//        });

//        return Task.CompletedTask;
//    });
//});

builder.Services.ApplicationConfiguration();
builder.Services.InfrastructureConfiguration(builder.Configuration);
builder.Services.IdentityConfiguration(builder.Configuration);
builder.Services.Configure<JwtOption>(builder.Configuration.GetSection("Jwt"));
builder.Services.Configure<FileStoragePathOption>(builder.Configuration.GetSection("FileStorage"));

builder.Services.AddSwaggerGen(c =>
{
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = @"JWT Authorization header using the Bearer scheme. 
                      Enter 'Bearer' [space] and then your token in the text input below.
                      Example: 'Bearer 1234sddsw'",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"

    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {{
        new OpenApiSecurityScheme
        {
            Reference = new OpenApiReference
            {
                Type = ReferenceType.SecurityScheme,
                Id = "Bearer"
            }
        },
        new List<string>()
        }
    });
    c.SwaggerDoc("v1", new OpenApiInfo()
    {
        Version = "v1",
        Title = "Library Management Api"
    });
});


builder.Services.AddMiniProfiler(option =>
{
    option.RouteBasePath = "/profiler";
}).AddEntityFramework();

builder.Host.UseSerilog();

builder.Services.AddHttpContextAccessor();
builder.Services.AddHttpClient<IAuthService, AuthService>();
builder.Services.AddScoped<ICurrentUserService, CurrentUserService>();
builder.Services.AddScoped<ValidationFilter<BookAuthorRequestDto>>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment() || app.Environment.IsProduction())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    //app.MapOpenApi();
    //app.MapScalarApiReference();

}

app.UseHttpsRedirection();

app.UseSerilogRequestLogging();

app.UseGlobalExtentionMiddleware();

app.UseAuthentication();
app.UseAuthorization();
app.UseMiniProfiler();
app.MapControllers();

app.Run();
