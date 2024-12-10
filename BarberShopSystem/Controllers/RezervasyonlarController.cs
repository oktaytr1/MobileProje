using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BarberShopSystem.Models;

namespace BarberShopSystem.Controllers
{
    /*SOLID: Principio de resposabilidad única**/
    public class RezervasyonlarController : Controller
    {
        private readonly ApplicationDbContext _context;

        public RezervasyonlarController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Rezervasyonlar
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Rezervasyonlar.Include(r => r.Customer)/*.Include(r => r.Cupo)*/.Include(r => r.Profesyonel).Include(r => r.Servis);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Rezervasyonlar/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Rezervasyonlar == null)
            {
                return NotFound();
            }

            var rezervasyon = await _context.Rezervasyonlar
                .Include(r => r.Customer)
                // .Include(r => r.Cupo)
                .Include(r => r.Profesyonel)
                .Include(r => r.Servis)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (rezervasyon == null)
            {
                return NotFound();
            }

            return View(rezervasyon);
        }

        // GET: Rezervasyonlar/Create
        public IActionResult Create()
        {
            ViewData["CustomerId"] = new SelectList(_context.Customers, "Id", "İsim");
            //ViewData["CupoId"] = new SelectList(_context.Cupos, "Id", "Descripcion");
            ViewData["ServisId"] = new SelectList(_context.Servisler, "Id", "Descripcion");
            //ViewData["ProfesionalId"] = ObtenerListaDeProfesyoneller();
            var profesyoneller = _context.Profesyoneller.ToList();
            ViewData["ProfesyonelId"] = new SelectList(profesyoneller, "Id", "İsim");
            return View();
        }

        // POST: Rezervasyonlar/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Tarih,ProfesyonelId,CupoId,ServisId,CustomerId,RezervasyonDurumu,Yenilik")] Rezervasyon rezervasyon)
        {
            if (ModelState.IsValid)
            {
                // Se establece la tarih de creación
                rezervasyon.OlusturulmaTarihi = DateTime.Now;

                _context.Add(rezervasyon);
                await _context.SaveChangesAsync();

                // TODO: Corregir Lógica de Cupos 
                // Se actualiza el estado del cupo seleccionado
                // var cupoSeleccionado = _context.Cupos.FirstOrDefault(c => c.Id == rezervasyon.CupoId);
                /*
                if (cupoSeleccionado != null)
                {
                    cupoSeleccionado.EstadoCupo = false;
                    await _context.SaveChangesAsync();
                }
                else
                {
                    // Manejo de error si no se encuentra el cupo
                    ModelState.AddModelError("CupoId", "El cupo seleccionado no es válido.");
                    ViewData["CustomerId"] = new SelectList(_context.Customers, "Id", "İsim", rezervasyon.CustomerId);
                    ViewData["ServisId"] = new SelectList(_context.Servisler, "Id", "Descripcion", rezervasyon.ServisId);
                    ViewData["ProfesionalId"] = ObtenerListaDeProfesyoneller();
                    return View(rezervasyon);
                }*/

                return RedirectToAction(nameof(Index));
            }

            ViewData["CustomerId"] = new SelectList(_context.Customers, "Id", "İsim", rezervasyon.CustomerId);
            ViewData["ServicioId"] = new SelectList(_context.Servisler, "Id", "Descripcion", rezervasyon.ServisId);
            ViewData["ProfesyonelId"] = ObtenerListaDeProfesyoneller();

            return View(rezervasyon);
        }

        // GET: Rezervasyonlar/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Rezervasyonlar == null)
            {
                return NotFound();
            }

            var rezervasyon = await _context.Rezervasyonlar.FindAsync(id);
            if (rezervasyon == null)
            {
                return NotFound();
            }
            ViewData["CustomerId"] = new SelectList(_context.Customers, "Id", "İsim", rezervasyon.CustomerId);
            //ViewData["CupoId"] = new SelectList(_context.Cupos, "Id", "Id", rezervasyon.CupoId);
            ViewData["ProfesyonelId"] = new SelectList(_context.Profesyoneller, "Id", "İsim", rezervasyon.ProfesyonelId);
            ViewData["ServisId"] = new SelectList(_context.Servisler, "Id", "Descripcion", rezervasyon.ServisId);
            return View(rezervasyon);
        }

        // POST: Rezervasyonlar/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Tarih,ProfesyonelId,ServisId,CustomerId,RezervasyonDurumu,Yenilik")] Rezervasyon rezervasyon)
        {
            if (id != rezervasyon.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    // Obtener la rezervasyon existente del contexto
                    var rezervasyonExistente = await _context.Rezervasyonlar.FindAsync(rezervasyon.Id);

                    if (rezervasyonExistente == null)
                    {
                        return NotFound();
                    }

                    // Copiar las propiedades modificadas de la nueva rezervasyon a la rezervasyon existente
                    _context.Entry(rezervasyonExistente).CurrentValues.SetValues(rezervasyon);

                    // No actualices la propiedad OlusturulmaTarihi
                    rezervasyonExistente.OlusturulmaTarihi = rezervasyon.OlusturulmaTarihi;

                    // Se establece la tarih de actualización
                    rezervasyonExistente.TarihGuncelleme = DateTime.Now;

                    // Actualizar la entidad existente en el contexto
                    _context.Update(rezervasyonExistente);

                    // Guardar cambios en la base de datos
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RezervasyonExists(rezervasyon.Id))
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

            // Cargar datos necesarios para Customer, Profesional y Servis
            ViewData["CustomerId"] = new SelectList(_context.Customers, "Id", "İsim", rezervasyon.CustomerId);
            ViewData["ProfesyonelId"] = new SelectList(_context.Profesyoneller, "Id", "İsim", rezervasyon.ProfesyonelId);
            ViewData["ServisId"] = new SelectList(_context.Servisler, "Id", "Descripcion", rezervasyon.ServisId);

            return View(rezervasyon);
        }

        // GET: Rezervasyonlar/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Rezervasyonlar == null)
            {
                return NotFound();
            }

            var rezervasyon = await _context.Rezervasyonlar
                .Include(r => r.Customer)
                // .Include(r => r.Cupo)
                .Include(r => r.Profesyonel)
                .Include(r => r.Servis)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (rezervasyon == null)
            {
                return NotFound();
            }

            return View(rezervasyon);
        }

        // POST: Rezervasyonlar/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Rezervasyonlar == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Rezervasyonlar'  is null.");
            }
            var rezervasyon = await _context.Rezervasyonlar.FindAsync(id);
            if (rezervasyon != null)
            {
                _context.Rezervasyonlar.Remove(rezervasyon);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RezervasyonExists(int id)
        {
          return (_context.Rezervasyonlar?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        // Método para obtener la lista de Profesyoneller
        private SelectList ObtenerListaDeProfesyoneller()
        {
            return new SelectList(_context.Profesyoneller, "Id", "İsim");
        }
        /*
        [HttpGet]
        public JsonResult ObtenerCuposDisponibles(int profesionalId, DateTime tarih)
        {
            var tarihSinHora = tarih.Date; // Eliminar la información de la hora

            // Verificar si hay algún cupo existente para la tarih proporcionada
            var existeCupo = _context.Cupos
                .Any(c => c.ProfesionalId == profesionalId && c.Tarih.Date == tarihSinHora && c.EstadoCupo);

            List<string> intervalosDisponibles;

            if (existeCupo)
            {
                // Obtener los intervalos de 30 minutos disponibles basados en los cupos existentes
                intervalosDisponibles = ObtenerIntervalosDisponibles(profesionalId, tarihSinHora);
            }
            else
            {
                // Si no hay cupos existentes, mostrar todos los intervalos del día
                intervalosDisponibles = ObtenerTodosLosIntervalosDelDia();
            }

            return Json(intervalosDisponibles);
        } */

        // Función para obtener intervalos disponibles basados en los cupos existentes
        /*
        private List<string> ObtenerIntervalosDisponibles(int profesionalId, DateTime tarih)
        {
            // Lógica para obtener intervalos disponibles
            // ...

            // En este ejemplo, se devuelve una lista de intervalos fijos para la demostración
            return new List<string> { "8:30 am", "9:00 am", "9:30 am", "10:00 am", "10:30 am", "11:00 am", "11:30 am", "12:00 pm" };
        }

        // Función para obtener todos los intervalos del día (para tarihs sin cupos existentes)
        private List<string> ObtenerTodosLosIntervalosDelDia()
        {
            // Lógica para obtener todos los intervalos del día
            // ...

            // En este ejemplo, se devuelve una lista de intervalos fijos para la demostración
            return new List<string> { "8:30 am", "9:00 am", "9:30 am", "10:00 am", "10:30 am", "11:00 am", "11:30 am", "12:00 pm", "12:30 pm", "1:00 pm", "1:30 pm", "2:00 pm", "2:30 pm", "3:00 pm", "3:30 pm", "4:00 pm", "4:30 pm" };
        }*/
    }
}
