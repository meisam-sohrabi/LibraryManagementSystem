using LibrarySys.Application;
using LibrarySys.Application.Contract.CurrentUser;
using LibrarySys.Application.Contract.IdentityService;
using LibrarySys.Application.Option;
using LibrarySys.Identity;
using LibrarySys.Identity.Service;
using LibrarySys.Infrastructure.EntityFrameworkCore;
using LibrarySysApi.CurrentUser;
using Microsoft.OpenApi.Models;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
//builder.Services.AddOpenApi();

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


builder.Services.AddHttpContextAccessor();
builder.Services.AddHttpClient<IAuthService, AuthService>();
builder.Services.AddScoped<ICurrentUserService, CurrentUserService>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();
app.UseMiniProfiler();
app.MapControllers();

app.Run();
