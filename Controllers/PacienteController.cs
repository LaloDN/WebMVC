using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Turnos.Models;
using Microsoft.EntityFrameworkCore;

namespace Turnos.Controles{
    public class PacienteController:Controller{
        public readonly TurnosContext _context;
        public PacienteController(TurnosContext context){
            _context=context;
        }

        public async Task<IActionResult> Index(){
            return View(await _context.Paciente.ToListAsync());
        }

        public async Task<IActionResult> Details(int? id){
            if(id==null){
                return NotFound();
            }
            var paciente=await _context.Paciente.FirstOrDefaultAsync(p=>p.IdPaciente==id);
            if(paciente==null){
                return NotFound();
            }
            return View(paciente);
        }

        public IActionResult Create(){
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken] //Válida que el método sea ejecutado através del formulario, no através de la url del navegador
        public async Task<IActionResult> Create([Bind("IdPaciente","Nombre","Apellido","Direccion",
        "Telefono","Email")] Paciente paciente){
            if(ModelState.IsValid){
                _context.Add(paciente);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View();
        }

        public async Task<IActionResult> Edit(int? id){
            if(id==null){
                return NotFound();
            }
            var paciente=await _context.Paciente.FindAsync(id);
            if(paciente==null){
                return NotFound();
            }
            return View(paciente);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id,[Bind("IdPaciente","Nombre","Apellido","Direccion",
        "Telefono","Email")] Paciente paciente){
            if(id!=paciente.IdPaciente){
                return NotFound();
            }
            if(ModelState.IsValid){
                _context.Update(paciente);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(paciente);
        }

        public async Task<IActionResult> Delete(int? id){
            if(id==null){
                return NotFound();
            }
            var paciente=await _context.Paciente.FirstOrDefaultAsync(p=>p.IdPaciente==id);
            if(paciente==null){
                return NotFound();
            }
            return View(paciente);
        }

        [HttpPost, ActionName("Delete")] //Desde la vista llamamos con el sobrenombre delete
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int? id){
            if(id==null){
                return NotFound();
            }
            var paciente=await _context.Paciente.FindAsync(id);
            if(paciente==null){
                return NotFound();
            }
            _context.Paciente.Remove(paciente);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}