using BlogComunitario.Extensions;
using BlogComunitario.Models;
using BlogComunitario.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddRazorPages();

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<BlogDbContext>(opts =>
{
	opts.UseSqlServer(
	builder.Configuration["ConnectionStrings:blogAppConnectionString"]);
});

//Added for session state
builder.Services.AddDistributedMemoryCache();
//Add dependecy injection
builder.Services.ApplicationServices(builder.Configuration);
//builder.Services.AddIdentityServices(builder.Configuration);

builder.Services.Configure<IdentityOptions>(options =>
{
    // Configurações de senha
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = true;
    options.Password.RequiredLength = 6;

    // Configurações de bloqueio
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);  // Tempo de bloqueio de 5 minutos
    options.Lockout.MaxFailedAccessAttempts = 5;  // Número máximo de tentativas falhas
    options.Lockout.AllowedForNewUsers = true;    // Permitir bloqueio de novos usuários
});

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("CanEditPolicy", policy =>
    policy.RequireClaim("CanEditPost", "true"));
});

var app = builder.Build();

// Outros middlewares...
//prevent back action when sucessfull lougout
app.UseMiddleware<NoCacheMiddleware>();

//app.Use(async (context, next) =>
//{
//    context.Response.Headers.Add("Cache-Control", "no-store, no-cache, must-revalidate");
//    await next();
//});

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler("/Home/Error");
	// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
	app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
               name: "default",
               pattern: "{controller=Account}/{action=Login}/{id?}");
app.MapControllerRoute(
            name: "ConfirmEmail",
            pattern: "{controller=Account}/{action=ConfirmEmail}/{userId}/{token}");
app.Run();

