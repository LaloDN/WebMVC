//Instancia de la clase controller
using Microsoft.AspNetCore.Mvc;
using Turnos.Models;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

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
        public async Task<IActionResult> Index(){
            //Támbien aquí le ponemos de parámetro el contexto, accedemos a la tabla especialidad y
            //con LINQ lo convertimos a una lista, con el _context ya tenemos listo nuestro controlador
            return View(await _context.Especialidad.ToListAsync());
        }

        /*Para este método de editar le vamos a pasar un parámetro de tipo entero que será el ID de la especialidad que
        vamos a querer editar, el signo de interrogación en el int es por que puede pasar que recibamos el parámetro
        en null por algúna excepción de la aplicación o error*/
        public async Task<IActionResult> Edit(int? id){
            if(id==null){
                //Eror 404
                return NotFound();
            }
            //En la variable especialidad guardamos de la base de datos de Turnos, la tabla especialidad el valor que
            //encuentre con el id que nosotros le pasamos
            var especialidad=await _context.Especialidad.FindAsync(id);
            //Por si no existe el registro en la base de datos
            if(especialidad==null){
                return NotFound();
            }
            return View(especialidad);
        }

        //Recibimos del formulario IdEspecialidad y descripción en la propiedad bind, es de tipo especialidad y
        //le ponemos el nombre de especialidad, bind nos trae esa info.
        [HttpPost] //Esto hace que el método sea el encargado de enviar la info y diferencia el otro método edit
        public async Task<IActionResult> Edit(int id, [Bind("IdEspecialidad,Descripcion")] Especialidad especialidad){
           //Controlamos una excepción en dado caso que sea dif. el Id (propiedad) de la tabla especialidad 
           if(id!=especialidad.IdEspecialidad){
               return NotFound();
           }
           //Con modelstate estamos asegurando que nos enlazamos correctamente con el bind y nos trajo correctamente
           //los valores de IdEspecialidad y descripción del formulario y este mismo ya esta validado.
           if(ModelState.IsValid){  
                _context.Update(especialidad);
                await _context.SaveChangesAsync();
                //una vez que actualizamos y guardamos los cambios, regresamos la vista Index para que nos rediriga a ella
                return RedirectToAction(nameof(Index));
           }
            //Si el modelo no es válido, entonces le regresamos la vista con especialidad para un posible tratamiento
            return View(especialidad);
        }

        public async Task<IActionResult> Delete(int? id){
            if(id==null){
                return NotFound();
            }
            //Ahora de la base de datos en la tabala especialidad con firstordefault encontramos el primer coincidencia
            var especialidad= await _context.Especialidad.FirstOrDefaultAsync(e=>e.IdEspecialidad==id);
            //Por si el Id no es nulo pero el id no existe en la tabla
            if(especialidad==null){
                return NotFound();
            }
            return View(especialidad);
        }

        [HttpPost]
        public  async Task<IActionResult> Delete(int id){
            var especialidad= await _context.Especialidad.FindAsync(id);
            _context.Especialidad.Remove(especialidad);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Create(){
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create([Bind("IdEspecialidad,Descripcion")] Especialidad especialidad){
            if(ModelState.IsValid){
                _context.Add(especialidad);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index)); 
            }
            return View();
        }
    }
}