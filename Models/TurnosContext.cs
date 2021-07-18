using Microsoft.EntityFrameworkCore;

namespace Turnos.Models
{
    //Para poder instanciar la clase DbContext, desde el nuget manager tenemos que buscar el
    //entityframeworkcore de la versión 5.0 (la misma versión que .net core)
    public class TurnosContext : DbContext
    {
        //Con el parámetro del constructor enviamos las opciones por default
        public TurnosContext(DbContextOptions<TurnosContext> opciones) :base(opciones){

        }
        // DBset es una tabla, va a ser de tipo especialidad (lo agarra del modelo especialidad)
        //por último, le asignamos el nombre de "Especialidad"
        public DbSet<Especialidad> Especialidad{ get; set; }

        //modelBuilder es el modelo que recibimos de namespace Turnos.models
        protected override void OnModelCreating(ModelBuilder modelBuilder){
            modelBuilder.Entity<Especialidad>(entidad=>{
                //Nombre de la tabla
                entidad.ToTable("Especialidad");
                //Primary key de la tabla
                entidad.HasKey(e=>e.IdEspecialidad);
                //Campo descripción, no puede ser nulo, a lo mucho puede tener 200 caracteres
                entidad.Property(e=>e.Descripcion).IsRequired().HasMaxLength(200).IsUnicode(false);
            });
        }
    }
}