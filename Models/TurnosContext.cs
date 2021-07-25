using Microsoft.EntityFrameworkCore;
using Turnos.Models;

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
        public DbSet<Paciente> Paciente{get;set;}
        public DbSet<Medico> Medico { get; set; }

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
            modelBuilder.Entity<Paciente>(entidad=>{
                entidad.ToTable("Paciente");
                entidad.HasKey(p => p.IdPaciente);
                entidad.Property(p => p.Nombre).IsRequired().HasMaxLength(50).IsUnicode(false);
                entidad.Property(p => p.Apellido).IsRequired().HasMaxLength(50).IsUnicode(false);
                entidad.Property(p => p.Direccion).IsRequired().HasMaxLength(250).IsUnicode(false);
                entidad.Property(p => p.Telefono).IsRequired().HasMaxLength(20).IsUnicode(false);
                entidad.Property(p => p.Email).IsRequired().HasMaxLength(100).IsUnicode(false);
            });

            modelBuilder.Entity<Medico>(entidad=>{
                entidad.ToTable("Medico");
                entidad.HasKey(m=>m.IdMedico);
                entidad.Property(m=>m.Nombre).IsRequired().HasMaxLength(50).IsUnicode(false);
                entidad.Property(m=>m.Apellido).IsRequired().HasMaxLength(50).IsUnicode(false);
                entidad.Property(m=>m.Direccion).IsRequired().HasMaxLength(250).IsUnicode(false);
                entidad.Property(m=>m.Telefono).IsRequired().HasMaxLength(20).IsUnicode(false);
                entidad.Property(m=>m.Email).IsRequired().HasMaxLength(50).IsUnicode(false);
                entidad.Property(m=>m.HorarioAtencionDesde).IsRequired().IsUnicode(false);
                entidad.Property(m=>m.HorarioAtencionHasta).IsRequired().IsUnicode(false);
            });
        }

       
    }
}