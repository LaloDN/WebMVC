//Instancia de la clase controller
using Microsoft.AspNetCore.Mvc;
using Turnos.Models;
using System.Linq;

namespace Turnos.Controles
{
    public class EspecialidadController : Controller {
        
        private readonly TurnosContext _context;

        //Siempre creamos el constructor
        //Cuando le ponemos de parámetro el turnoscontext hacemos que la vista se pueda conectar a la base
        public EspecialidadController(TurnosContext context){
            _context=context;
        }

        //Este método devuelve el resultado de una acción en una vista para que lo vea el usuario en pantalla
        public IActionResult Index(){
            //Támbien aquí le ponemos de parámetro el contexto, accedemos a la tabla especialidad y
            //con LINQ lo convertimos a una lista, con el _context ya tenemos listo nuestro controlador
            return View(_context.Especialidad.ToList());
        }

        /*Para este método de editar le vamos a pasar un parámetro de tipo entero que será el ID de la especialidad que
        vamos a querer editar, el signo de interrogación en el int es por que puede pasar que recibamos el parámetro
        en null por algúna excepción de la aplicación o error*/
        public IActionResult Edit(int? id){
            if(id==null){
                //Eror 404
                return NotFound();
            }
            //En la variable especialidad guardamos de la base de datos de Turnos, la tabla especialidad el valor que
            //encuentre con el id que nosotros le pasamos
            var especialidad=_context.Especialidad.Find(id);
            //Por si no existe el registro en la base de datos
            if(especialidad==null){
                return NotFound();
            }
            return View(especialidad);
        }

        //Recibimos del formulario IdEspecialidad y descripción en la propiedad bind, es de tipo especialidad y
        //le ponemos el nombre de especialidad, bind nos trae esa info.
        [HttpPost] //Esto hace que el método sea el encargado de enviar la info y diferencia el otro método edit
        public IActionResult Edit(int id, [Bind("IdEspecialidad,Descripcion")] Especialidad especialidad){
           //Controlamos una excepción en dado caso que sea dif. el Id (propiedad) de la tabla especialidad 
           if(id!=especialidad.IdEspecialidad){
               return NotFound();
           }
           //Con modelstate estamos asegurando que nos enlazamos correctamente con el bind y nos trajo correctamente
           //los valores de IdEspecialidad y descripción del formulario y este mismo ya esta validado.
           if(ModelState.IsValid){  
                _context.Update(especialidad);
                _context.SaveChanges();
                //una vez que actualizamos y guardamos los cambios, regresamos la vista Index para que nos rediriga a ella
                return RedirectToAction(nameof(Index));
           }
            //Si el modelo no es válido, entonces le regresamos la vista con especialidad para un posible tratamiento
            return View(especialidad);
        }

        public IActionResult Delete(int? id){
            if(id==null){
                return NotFound();
            }
            //Ahora de la base de datos en la tabala especialidad con firstordefault encontramos el primer coincidencia
            var especialidad=_context.Especialidad.FirstOrDefault(e=>e.IdEspecialidad==id);
            return View();
        }
    }
}