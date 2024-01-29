using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using BibliotecaWebb.Repositories;

public class Program
{
    public static void Main(string[] args)
    {
        Console.WriteLine("¡Hola,Stived!! Corriendo..");

        CreateHostBuilder(args).Build().Run();
    }

    public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseStartup<Startup>();
            });

public class Startup
{
    public void ConfigureServices(IServiceCollection services)
    {
        // Otros servicios

        // Registra AutorRepository con una cadena de conexión
        services.AddScoped<AutorRepository>(provider =>
        {
            // Obtiene la cadena de conexión directamente de la configuración
            var configuration = provider.GetRequiredService<IConfiguration>();
            var connectionString = configuration.GetConnectionString("DefaultConnection");

            // Verifica si la cadena de conexión no es nula antes de crear AutorRepository
            if (connectionString != null)
            {
                return new AutorRepository(connectionString);
            }
            else
            {
                throw new InvalidOperationException("La cadena de conexión es nula.");
            }
        });

        // Otros servicios que puedas necesitar
        services.AddControllersWithViews();
    }


        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
    

public class YourDbContext : DbContext
{
    public YourDbContext(DbContextOptions<YourDbContext> options) : base(options)
    {
    }

        // Definir tus DbSet y modelos aquí
    }
}
