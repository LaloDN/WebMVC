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
    }
}