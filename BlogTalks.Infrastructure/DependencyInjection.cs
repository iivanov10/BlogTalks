using BlogTalks.Application.Abstractions;
using BlogTalks.Domain.Repositories;
using BlogTalks.Infrastructure.Authentication;
using BlogTalks.Infrastructure.Data.DataContext;
using BlogTalks.Infrastructure.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Swashbuckle.AspNetCore.SwaggerGen;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.OpenApi.Models;

namespace BlogTalks.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("Database");

            services.AddDbContext<ApplicationDbContext>(
                options => options
                    .UseNpgsql(connectionString, npgsqlOptions =>
                        npgsqlOptions.MigrationsHistoryTable(HistoryRepository.DefaultTableName, Schemas.Default)));

            //services.AddScoped<IApplicationDbContext>(sp => sp.GetRequiredService<ApplicationDbContext>());
            services.AddScoped<ApplicationDbContext>();
            services.AddTransient<IBlogPostRepository, BlogPostRepository>();
            services.AddTransient<ICommentRepository, CommentRepository>();
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IAuthService, AuthService>();

            services.AddHttpContextAccessor();

            var jwtSettings = configuration.GetSection("JwtSettings").Get<JwtSettings>();
            services.Configure<JwtSettings>(configuration.GetSection("JwtSettings"));

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = jwtSettings.Issuer,
                        ValidAudience = jwtSettings.Audience,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.SecretKey))
                    };
                });

            services.AddSwaggerGen(options =>
            {
                options.CustomSchemaIds(type => type.FullName);

                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Please insert JWT with Bearer into field",
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey
                });

                options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new string[] { }
                    }
                });
            });

            services.AddAuthorization();

            return services;
        }
    }
}
