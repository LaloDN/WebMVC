using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Turnos.Models;

namespace Turnos
{
    //Las clase startup proporciona todos los servicios para iniciar la aplicacion
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            //Le establece los valores por default del archivo json
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //Con este componente de service nos permite ejecutar el MVC
            services.AddControllersWithViews();
            //con esto establecemos la conexión con la base de datos inyectando un service
            //Para referenciar turnos context necesitamos el namespace turnos.models
            //Para referenciar al método usesqlserver necesitamos añadir el entityframeworksql 5.0
            //Con el get connection string accedemos al appsettings.json y buscamos el objeto ConnectionString y buscamos la propiedad turnoscontext
            services.AddDbContext<TurnosContext>(opciones => opciones.UseSqlServer(Configuration.GetConnectionString("TurnosContext")));
            //el dbcontext necesita el contexto que vamos a utilizar (vamos a utilizar las tablas de turnos)
            //Luego en opciones le asignamos el motor de base de datos que va a utilizar, el objeto opciones va a
            //ser el que se conecte con SQL server
        }

        //Los middleware
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            //Desactiva el rediccionamiento a un protoclo http
            //app.UseHttpsRedirection();

            //Sirve todos los archivos estaticos de nuestro proyecto (archivos de js, imagenes, css, etc.)
            app.UseStaticFiles();
            //Con esto realizamos los routeos para los controladores y las vistas
            app.UseRouting();
            //Capa de seguridad
            app.UseAuthorization();
            //Este es el controlador por default y que vista nos va a abrir
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    //Controlador home, action (vista) index
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
