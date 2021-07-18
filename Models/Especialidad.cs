using System.ComponentModel.DataAnnotations;

namespace Turnos.Models{
    public class Especialidad{
        [Key] //Con la palabra reservada key le decimoos al entity framework que esta es una llave primaria
        public int IdEspecialidad {get;set;} //PRIMARY KEY DE LA TABLA
        public string Descripcion {get;set;}
        
    }
}