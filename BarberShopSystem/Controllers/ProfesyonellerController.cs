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
    public class ProfesyonellerController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProfesyonellerController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Profesyoneller
        public async Task<IActionResult> Index()
        {
              return _context.Profesyoneller != null ? 
                          View(await _context.Profesyoneller.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.Profesyoneller'  is null.");
        }

        // GET: Profesyoneller/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Profesyoneller == null)
            {
                return NotFound();
            }

            var profesional = await _context.Profesyoneller
                .FirstOrDefaultAsync(m => m.Id == id);
            if (profesional == null)
            {
                return NotFound();
            }

            return View(profesional);
        }

        // GET: Profesyoneller/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Profesyoneller/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,İsim,Posta,Telefon ")] Profesyonel profesional)
        {
            if (ModelState.IsValid)
            {
                _context.Add(profesional);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(profesional);
        }

        // GET: Profesyoneller/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Profesyoneller == null)
            {
                return NotFound();
            }

            var profesional = await _context.Profesyoneller.FindAsync(id);
            if (profesional == null)
            {
                return NotFound();
            }
            return View(profesional);
        }

        // POST: Profesyoneller/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,İsim,Posta,Telefon ")] Profesyonel profesional)
        {
            if (id != profesional.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(profesional);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProfesionalExists(profesional.Id))
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
            return View(profesional);
        }

        // GET: Profesyoneller/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Profesyoneller == null)
            {
                return NotFound();
            }

            var profesional = await _context.Profesyoneller
                .FirstOrDefaultAsync(m => m.Id == id);
            if (profesional == null)
            {
                return NotFound();
            }

            return View(profesional);
        }

        // POST: Profesyoneller/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Profesyoneller == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Profesyoneller'  is null.");
            }
            var profesional = await _context.Profesyoneller.FindAsync(id);
            if (profesional != null)
            {
                _context.Profesyoneller.Remove(profesional);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProfesionalExists(int id)
        {
          return (_context.Profesyoneller?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
