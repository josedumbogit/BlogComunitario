using BlogComunitario.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
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


			services.AddCors((setup) =>
			{
				setup.AddPolicy("default", (options) =>
				{
					options.AllowAnyMethod().AllowAnyHeader().AllowAnyOrigin();
				});
			});

			return services;
		}
	}
}
