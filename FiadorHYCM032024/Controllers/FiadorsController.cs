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
                .Include(s=> s.ReferenciasFamiliare)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (fiador == null)
            {
                return NotFound();
            }
            ViewBag.Accion = "Details";
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

            var fiador = await _context.Fiadors
                .Include(s=> s.ReferenciasFamiliare)
                .FirstAsync(s=> s.Id==id);
            if (fiador == null)
            {
                return NotFound();
            }
            ViewBag.Accion = "Edit";
            return View(fiador);
        }

        // POST: Fiadors/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nombre,Direccion,Telefono,Correo,Ocupacion,IngresoMensual,FechaNacimiento,ReferenciasFamiliare")] Fiador fiador)
        {
            if (id != fiador.Id)
            {
                return NotFound();
            }

            try
                {
                var facturaUpdate = await _context.Fiadors
                        .Include(s => s.ReferenciasFamiliare)
                        .FirstAsync(s => s.Id == fiador.Id);
                facturaUpdate.Nombre = fiador.Nombre;
                facturaUpdate.Direccion = fiador.Direccion;
                facturaUpdate.Telefono = fiador.Telefono;
                var detNew = fiador.ReferenciasFamiliare.Where(s => s.Id == 0);

                //var facturaUpdate = await _context.Fiadors
                //  .Include(s => s.ReferenciasFamiliare)
                //  .FirstAsync(s => s.Id == fiador.Id);
                //facturaUpdate.Nombre = fiador.Nombre;
                //facturaUpdate.Direccion = fiador.Direccion;
                //facturaUpdate.Telefono = fiador.Telefono;
                //facturaUpdate.Correo = fiador.Correo;
                //facturaUpdate.Ocupacion = fiador.Ocupacion;
                //facturaUpdate.IngresoMensual = fiador.IngresoMensual;
                //facturaUpdate.FechaNacimiento = fiador.FechaNacimiento;
                // Obtener todos los detalles que seran nuevos y agregarlos a la base de datos
                
                    foreach (var d in detNew)
                    {
                        facturaUpdate.ReferenciasFamiliare.Add(d);
                    }
                    // Obtener todos los detalles que seran modificados y actualizar a la base de datos
                    var detUpdate = fiador.ReferenciasFamiliare.Where(s => s.Id > 0);
                    foreach (var d in detUpdate)
                    {
                        var det = facturaUpdate.ReferenciasFamiliare.FirstOrDefault(s => s.Id == d.Id);
                        det.Nombre = d.Nombre;
                        det.Relacion = d.Relacion;
                        det.Telefono = d.Telefono;
                        det.Direccion = d.Direccion;
                    }
                    // Obtener todos los detalles que seran eliminados y actualizar a la base de datos
                    var delDet = fiador.ReferenciasFamiliare.Where(s => s.Id < 0).ToList();
                    if (delDet != null && delDet.Count > 0)
                    {
                        foreach (var d in delDet)
                        {
                            d.Id = d.Id * -1;
                            var det = facturaUpdate.ReferenciasFamiliare.FirstOrDefault(s => s.Id == d.Id);
                            _context.Remove(det);
                            // facturaUpdate.DetFacturaVenta.Remove(det);
                        }
                    }
                    // Aplicar esos cambios a la base de datos
                    _context.Update(facturaUpdate);
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

        // GET: Fiadors/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Fiadors == null)
            {
                return NotFound();
            }

            var fiador = await _context.Fiadors
                .Include(s=> s.ReferenciasFamiliare)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (fiador == null)
            {
                return NotFound();
            }
            ViewBag.Accion = "Delete";
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
