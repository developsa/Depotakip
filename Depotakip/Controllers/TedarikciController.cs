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
    public class TedarikciController : Controller
    {
        private readonly MarketDbContext _context;

        public TedarikciController(MarketDbContext context)
        {
            _context = context;
        }

        // GET: Tedarikci
        public async Task<IActionResult> Index()
        {
              return _context.Tedarikciler != null ? 
                          View(await _context.Tedarikciler.ToListAsync()) :
                          Problem("Entity set 'MarketDbContext.Tedarikciler'  is null.");
        }

        // GET: Tedarikci/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Tedarikciler == null)
            {
                return NotFound();
            }

            var tedarikci = await _context.Tedarikciler
                .FirstOrDefaultAsync(m => m.TedarikciId == id);
            if (tedarikci == null)
            {
                return NotFound();
            }

            return View(tedarikci);
        }

        // GET: Tedarikci/Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TedarikciId,Ad,Adres,Tel")] Tedarikci tedarikci)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tedarikci);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tedarikci);
        }

        // GET: Tedarikci/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Tedarikciler == null)
            {
                return NotFound();
            }

            var tedarikci = await _context.Tedarikciler.FindAsync(id);
            if (tedarikci == null)
            {
                return NotFound();
            }
            return View(tedarikci);
        }

        // POST: Tedarikci/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TedarikciId,Ad,Adres,Tel")] Tedarikci tedarikci)
        {
            if (id != tedarikci.TedarikciId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tedarikci);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TedarikciExists(tedarikci.TedarikciId))
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
            return View(tedarikci);
        }

        // GET: Tedarikci/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Tedarikciler == null)
            {
                return NotFound();
            }

            var tedarikci = await _context.Tedarikciler
                .FirstOrDefaultAsync(m => m.TedarikciId == id);
            if (tedarikci == null)
            {
                return NotFound();
            }

            return View(tedarikci);
        }

        // POST: Tedarikci/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Tedarikciler == null)
            {
                return Problem("Entity set 'MarketDbContext.Tedarikciler'  is null.");
            }
            var tedarikci = await _context.Tedarikciler.FindAsync(id);
            if (tedarikci != null)
            {
                _context.Tedarikciler.Remove(tedarikci);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TedarikciExists(int id)
        {
          return (_context.Tedarikciler?.Any(e => e.TedarikciId == id)).GetValueOrDefault();
        }
    }
}
