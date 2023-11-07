using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Pomelo.EntityFrameworkCore.MySql;
using SalesWebMvc.Data;
namespace SalesWebMvc
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //utilizando o provider do MySql para o Entity Framework
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddDbContext<SalesWebMvcContext>(options =>
                options.UseMySql(builder.Configuration.GetConnectionString("SalesWebMvcContext"),
                ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("SalesWebMvcContext")),
                builder => builder.MigrationsAssembly("SalesWebMvc")));
                //options.UseSqlServer(builder.Configuration.GetConnectionString("SalesWebMvcContext") ?? throw new InvalidOperationException("Connection string 'SalesWebMvcContext' not found.")));

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            var app = builder.Build();

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

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}