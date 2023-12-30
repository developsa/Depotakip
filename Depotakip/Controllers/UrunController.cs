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
    public class UrunController : Controller
    {
        private readonly MarketDbContext _context;

        private readonly IWebHostEnvironment _hostEnvironment;

        public UrunController(MarketDbContext context, IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            this._hostEnvironment = hostEnvironment; 
        }

        // GET: Urun
       // [Authorize] 
        public async Task<IActionResult> Index()
        {
            var marketContext = _context.Urunler.Include(u => u.Kategori);
            return View(await marketContext.ToListAsync());
        }

        // GET: Urun/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Urunler == null)
            {
                return NotFound();
            }

            var urun = await _context.Urunler
                .Include(u => u.Kategori)
                .FirstOrDefaultAsync(m => m.UrunId == id);
            if (urun == null)
            {
                return NotFound();
            }

            return View(urun);
        }

        // GET: Urun/Create
        public IActionResult Create()
        {
            ViewData["KategoriId"] = new SelectList(_context.Kategoriler, "KategoriId", "KategoriAd");
            return View();
        }

        // POST: Urun/Create
      
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UrunId,KategoriId,UrunAd,UrunAdet,UrunFiyat,Gelistarih,ImageFile")] Urun urun)
        {
            string wwwrootpath = _hostEnvironment.WebRootPath;
            string fileName = Path.GetFileNameWithoutExtension(urun.ImageFile.FileName);  //parameter  aldık
            string extension = Path.GetExtension(urun.ImageFile.FileName);
            urun.UrunFoto = fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;  //file name +time+extension
            string path = Path.Combine(wwwrootpath + "/Image/", fileName); // with rootpath rootpath+Image file (in the wwwroot ) and combine file name
            using (var filestream = new FileStream(path, FileMode.Create))
            {
                await urun.ImageFile.CopyToAsync(filestream);

            }
            //if (ModelState.IsValid)
            //{
            _context.Add(urun);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
            //}
            ViewData["KategoriId"] = new SelectList(_context.Kategoriler, "KategoriId", "KategoriId", urun.KategoriId);
            return View(urun);
        }

        // GET: Urun/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Urunler == null)
            {
                return NotFound();
            }

            var urun = await _context.Urunler.FindAsync(id);
            if (urun == null)
            {
                return NotFound();
            }
            ViewData["KategoriId"] = new SelectList(_context.Kategoriler, "KategoriId", "KategoriAd", urun.KategoriId);
            return View(urun);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("UrunId,KategoriId,UrunAd,UrunAdet,UrunFiyat,Gelistarih,ImageFile")] Urun urun)
        {
            string wwwrootpath = _hostEnvironment.WebRootPath;
            string fileName = Path.GetFileNameWithoutExtension(urun.ImageFile.FileName); 
            string extension = Path.GetExtension(urun.ImageFile.FileName);
            urun.UrunFoto = fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;  
            string path = Path.Combine(wwwrootpath + "/Image/", fileName); 
            using (var filestream = new FileStream(path, FileMode.Create))
            {
                await urun.ImageFile.CopyToAsync(filestream);

            }
            if (id != urun.UrunId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(urun);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UrunExists(urun.UrunId))
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
            ViewData["KategoriId"] = new SelectList(_context.Kategoriler, "KategoriId", "KategoriId", urun.KategoriId);
            return View(urun);
        }

        // GET: Urun/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Urunler == null)
            {
                return NotFound();
            }

            var urun = await _context.Urunler
                .Include(u => u.Kategori)
                .FirstOrDefaultAsync(m => m.UrunId == id);
            if (urun == null)
            {
                return NotFound();
            }

            return View(urun);
        }

        // POST: Urun/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Urunler == null)
            {
                return Problem("Entity set 'MarketContext.Urunler'  is null.");
            }
            var urun = await _context.Urunler.FindAsync(id);
            if (urun != null)
            {
                _context.Urunler.Remove(urun);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UrunExists(int id)
        {
            return (_context.Urunler?.Any(e => e.UrunId == id)).GetValueOrDefault();
        }
    }
}
