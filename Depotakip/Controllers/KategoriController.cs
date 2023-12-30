using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Depotakip.Models;

namespace Depotakip.Controllers
{
    public class KategoriController : Controller
    {
        private readonly MarketDbContext _context;

        public KategoriController(MarketDbContext context)
        {
            _context = context;
        }

        // GET: Kategori
        public async Task<IActionResult> Index()
        {
            var marketDbContext = _context.Kategoriler.Include(k => k.Reyon);
            return View(await marketDbContext.ToListAsync());
        }

        // GET: Kategori/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Kategoriler == null)
            {
                return NotFound();
            }

            var kategori = await _context.Kategoriler
                .Include(k => k.Reyon)
                .FirstOrDefaultAsync(m => m.KategoriId == id);
            if (kategori == null)
            {
                return NotFound();
            }

            return View(kategori);
        }

        // GET: Kategori/Create
        public IActionResult Create()
        {
            ViewData["ReyonId"] = new SelectList(_context.Reyonlar, "ReyonId", "ReyonAd");
            return View();
        }

  
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("KategoriId,KategoriAd,ReyonId")] Kategori kategori)
        {
            if (ModelState.IsValid)
            {
                _context.Add(kategori);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ReyonId"] = new SelectList(_context.Reyonlar, "ReyonId", "ReyonId", kategori.ReyonId);
            return View(kategori);
        }

        // GET: Kategori/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Kategoriler == null)
            {
                return NotFound();
            }

            var kategori = await _context.Kategoriler.FindAsync(id);
            if (kategori == null)
            {
                return NotFound();
            }
            ViewData["ReyonId"] = new SelectList(_context.Reyonlar, "ReyonId", "ReyonAd", kategori.ReyonId);
            return View(kategori);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("KategoriId,KategoriAd,ReyonId")] Kategori kategori)
        {
            if (id != kategori.KategoriId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(kategori);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!KategoriExists(kategori.KategoriId))
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
            ViewData["ReyonId"] = new SelectList(_context.Reyonlar, "ReyonId", "ReyonId", kategori.ReyonId);
            return View(kategori);
        }

        // GET: Kategori/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Kategoriler == null)
            {
                return NotFound();
            }

            var kategori = await _context.Kategoriler
                .Include(k => k.Reyon)
                .FirstOrDefaultAsync(m => m.KategoriId == id);
            if (kategori == null)
            {
                return NotFound();
            }

            return View(kategori);
        }

        // POST: Kategori/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Kategoriler == null)
            {
                return Problem("Entity set 'MarketDbContext.Kategoriler'  is null.");
            }
            var kategori = await _context.Kategoriler.FindAsync(id);
            if (kategori != null)
            {
                _context.Kategoriler.Remove(kategori);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool KategoriExists(int id)
        {
          return (_context.Kategoriler?.Any(e => e.KategoriId == id)).GetValueOrDefault();
        }
    }
}
