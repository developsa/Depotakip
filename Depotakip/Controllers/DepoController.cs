using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Depotakip.Models;
using Microsoft.AspNetCore.Authorization;

namespace Depotakip.Controllers
{
    public class DepoController : Controller
    {
        private readonly MarketDbContext _context;

        public DepoController(MarketDbContext context)
        {
            _context = context;
        }

        // GET: Depo
        [Authorize]
        public async Task<IActionResult> Index()
        {
              return _context.Depolar != null ? 
                          View(await _context.Depolar.ToListAsync()) :
                          Problem("Entity set 'MarketDbContext.Depolar'  is null.");
        }

        // GET: Depo/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Depolar == null)
            {
                return NotFound();
            }

            var depo = await _context.Depolar
                .FirstOrDefaultAsync(m => m.DepoId == id);
            if (depo == null)
            {
                return NotFound();
            }

            return View(depo);
        }

        // GET: Depo/Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DepoId,Ad,Adres,Kapasite,PersonelSayisi")] Depo depo)
        {
            if (ModelState.IsValid)
            {
                _context.Add(depo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(depo);
        }

        // GET: Depo/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Depolar == null)
            {
                return NotFound();
            }

            var depo = await _context.Depolar.FindAsync(id);
            if (depo == null)
            {
                return NotFound();
            }
            return View(depo);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("DepoId,Ad,Adres,Kapasite,PersonelSayisi")] Depo depo)
        {
            if (id != depo.DepoId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(depo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DepoExists(depo.DepoId))
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
            return View(depo);
        }

        // GET: Depo/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Depolar == null)
            {
                return NotFound();
            }

            var depo = await _context.Depolar
                .FirstOrDefaultAsync(m => m.DepoId == id);
            if (depo == null)
            {
                return NotFound();
            }

            return View(depo);
        }

        // POST: Depo/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Depolar == null)
            {
                return Problem("Entity set 'MarketDbContext.Depolar'  is null.");
            }
            var depo = await _context.Depolar.FindAsync(id);
            if (depo != null)
            {
                _context.Depolar.Remove(depo);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DepoExists(int id)
        {
          return (_context.Depolar?.Any(e => e.DepoId == id)).GetValueOrDefault();
        }
    }
}
