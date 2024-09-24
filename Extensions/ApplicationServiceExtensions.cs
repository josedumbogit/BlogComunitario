using BlogComunitario.Models;
using BlogComunitario.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Identity.Client;
using System.Runtime.InteropServices.JavaScript;

namespace BlogComunitario.Extensions
{
    public static class ApplicationServiceExtensions
    {
        public static IServiceCollection ApplicationServices(this IServiceCollection services,
            IConfiguration config)
        {
            services.AddDbContext<BlogDbContext>(options =>
                options.UseSqlServer(config.GetConnectionString("blogAppConnectionString")));
            
            services.AddIdentity<ApplicationUser, IdentityRole>()
              .AddEntityFrameworkStores<BlogDbContext>()
            .AddDefaultTokenProviders();

            services.Configure<SmtpSettings>(config.GetSection("SmtpSettings"));

            services.AddCors((setup) =>
            {
                setup.AddPolicy("default", (options) =>
                {
                    options.AllowAnyMethod().AllowAnyHeader().AllowAnyOrigin();
                });
            });

            services.AddScoped<IEmailSender, EmailSender>();

            return services;
        }
    }
}
