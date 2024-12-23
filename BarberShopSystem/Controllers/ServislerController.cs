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
    public class ServislerController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ServislerController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Servisler
        public async Task<IActionResult> Index()
        {
              return _context.Servisler != null ? 
                          View(await _context.Servisler.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.Servisler'  is null.");
        }

        // GET: Servisler/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Servisler == null)
            {
                return NotFound();
            }

            var servis = await _context.Servisler
                .FirstOrDefaultAsync(m => m.Id == id);
            if (servis == null)
            {
                return NotFound();
            }

            return View(servis);
        }

        // GET: Servisler/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Servisler/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Descripcion,Sure,Fiyat")] Servis servis)
        {
            if (ModelState.IsValid)
            {
                _context.Add(servis);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(servis);
        }

        // GET: Servisler/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Servisler == null)
            {
                return NotFound();
            }

            var servis = await _context.Servisler.FindAsync(id);
            if (servis == null)
            {
                return NotFound();
            }
            return View(servis);
        }

        // POST: Servisler/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Descripcion,Sure,Fiyat")] Servis servis)
        {
            if (id != servis.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(servis);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ServisExists(servis.Id))
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
            return View(servis);
        }

        // GET: Servisler/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Servisler == null)
            {
                return NotFound();
            }

            var servis = await _context.Servisler
                .FirstOrDefaultAsync(m => m.Id == id);
            if (servis == null)
            {
                return NotFound();
            }

            return View(servis);
        }

        // POST: Servisler/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Servisler == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Servisler'  is null.");
            }
            var servis = await _context.Servisler.FindAsync(id);
            if (servis != null)
            {
                _context.Servisler.Remove(servis);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ServisExists(int id)
        {
          return (_context.Servisler?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
