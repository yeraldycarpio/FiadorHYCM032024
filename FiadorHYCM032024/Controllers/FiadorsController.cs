using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FiadorHYCM032024.Models;

namespace FiadorHYCM032024.Controllers
{
    public class FiadorsController : Controller
    {
        private readonly FiadorHYCM032024Context _context;

        public FiadorsController(FiadorHYCM032024Context context)
        {
            _context = context;
        }

        // GET: Fiadors
        public async Task<IActionResult> Index()
        {
              return _context.Fiadors != null ? 
                          View(await _context.Fiadors.ToListAsync()) :
                          Problem("Entity set 'FiadorHYCM032024Context.Fiadors'  is null.");
        }

        // GET: Fiadors/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Fiadors == null)
            {
                return NotFound();
            }

            var fiador = await _context.Fiadors
                .FirstOrDefaultAsync(m => m.Id == id);

            if (fiador == null)
            {
                return NotFound();
            }

            return View(fiador);
        }

        // GET: Fiadors/Create
        public IActionResult Create()
        {
            var fiador = new Fiador();
            fiador.FechaNacimiento = DateTime.Now;
            fiador.ReferenciasFamiliare = new List<ReferenciasFamiliare>();
            fiador.ReferenciasFamiliare.Add(new ReferenciasFamiliare
            {

            });
            ViewBag.Accion = "Create";
            return View(fiador);
        }

        // POST: Fiadors/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nombre,Direccion,Telefono,Correo,Ocupacion,IngresoMensual,FechaNacimiento,ReferenciasFamiliare")] Fiador fiador)
        {
            _context.Add(fiador);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
            //return View(fiador);
        }

        [HttpPost]
        public ActionResult AgregarDetalles([Bind("Id,Nombre,Direccion,Telefono,Correo,Ocupacion,IngresoMensual,FechaNacimiento,ReferenciasFamiliare")] Fiador fiador, string accion)
        {
            fiador.ReferenciasFamiliare.Add(new ReferenciasFamiliare {  });
            ViewBag.Accion = accion;
            return View(accion, fiador);
        }

        public ActionResult EliminarDetalles([Bind("Id,Nombre,Direccion,Telefono,Correo,Ocupacion,IngresoMensual,FechaNacimiento,ReferenciasFamiliare")] Fiador fiador,
           int index, string accion)
        {
            var det = fiador.ReferenciasFamiliare[index];
            if (accion == "Edit" && det.Id > 0)
            {
                det.Id = det.Id * -1;
            }
            else
            {
                fiador.ReferenciasFamiliare.RemoveAt(index);
            }

            ViewBag.Accion = accion;
            return View(accion, fiador);
        }

        // GET: Fiadors/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Fiadors == null)
            {
                return NotFound();
            }

            var fiador = await _context.Fiadors.FindAsync(id);
            if (fiador == null)
            {
                return NotFound();
            }
            return View(fiador);
        }

        // POST: Fiadors/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nombre,Direccion,Telefono,Correo,Ocupacion,IngresoMensual,FechaNacimiento")] Fiador fiador)
        {
            if (id != fiador.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(fiador);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FiadorExists(fiador.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(fiador);
        }

        // GET: Fiadors/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Fiadors == null)
            {
                return NotFound();
            }

            var fiador = await _context.Fiadors
                .FirstOrDefaultAsync(m => m.Id == id);
            if (fiador == null)
            {
                return NotFound();
            }

            return View(fiador);
        }

        // POST: Fiadors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Fiadors == null)
            {
                return Problem("Entity set 'FiadorHYCM032024Context.Fiadors'  is null.");
            }
            var fiador = await _context.Fiadors.FindAsync(id);
            if (fiador != null)
            {
                _context.Fiadors.Remove(fiador);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FiadorExists(int id)
        {
          return (_context.Fiadors?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
