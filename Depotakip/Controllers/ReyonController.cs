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
    public class ReyonController : Controller
    {
        private readonly MarketDbContext _context;

        private readonly IWebHostEnvironment _hostEnvironment;

        public ReyonController(MarketDbContext context, IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            this._hostEnvironment = hostEnvironment;
        }

        // GET: Reyon
        public async Task<IActionResult> Index()
        {
            var marketContext = _context.Reyonlar.Include(r => r.Depo);
            return View(await marketContext.ToListAsync());
        }

        // GET: Reyon/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Reyonlar == null)
            {
                return NotFound();
            }

            var reyon = await _context.Reyonlar
                .Include(r => r.Depo)
                .FirstOrDefaultAsync(m => m.ReyonId == id);
            if (reyon == null)
            {
                return NotFound();
            }

            return View(reyon);
        }

        // GET: Reyon/Create
        public IActionResult Create()
        {
            ViewData["DepoId"] = new SelectList(_context.Depolar, "DepoId", "Ad");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ReyonId,ReyonAd,ImageFile,DepoId")] Reyon reyon)
        {

            //For Image
            string wwwrootpath = _hostEnvironment.WebRootPath;
            string fileName = Path.GetFileNameWithoutExtension(reyon.ImageFile.FileName);  //parameter
            string extension = Path.GetExtension(reyon.ImageFile.FileName);
            reyon.ReyonImg = fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;  //file name +time+extension
            string path = Path.Combine(wwwrootpath + "/Image/", fileName); // with rootpath rootpath+Image file (in the wwwroot ) and combine file name
            using (var filestream = new FileStream(path, FileMode.Create))
            {
                await reyon.ImageFile.CopyToAsync(filestream);

            }
            _context.Add(reyon);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
          
            ViewData["DepoId"] = new SelectList(_context.Depolar, "DepoId", "DepoId", reyon.DepoId);
            return View(reyon);
        }

        // GET: Reyon/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Reyonlar == null)
            {
                return NotFound();
            }

            var reyon = await _context.Reyonlar.FindAsync(id);
            if (reyon == null)
            {
                return NotFound();
            }
            ViewData["DepoId"] = new SelectList(_context.Depolar, "DepoId", "Ad", reyon.DepoId);
            return View(reyon);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ReyonId,ReyonAd,ImageFile,DepoId")] Reyon reyon)
        {
            string wwwrootpath = _hostEnvironment.WebRootPath;
            string fileName = Path.GetFileNameWithoutExtension(reyon.ImageFile.FileName); 
            string extension = Path.GetExtension(reyon.ImageFile.FileName);
            reyon.ReyonImg = fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;  
            string path = Path.Combine(wwwrootpath + "/Image/", fileName); 
            using (var filestream = new FileStream(path, FileMode.Create))
            {
                await reyon.ImageFile.CopyToAsync(filestream);

            }


            if (id != reyon.ReyonId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(reyon);

                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReyonExists(reyon.ReyonId))
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
            ViewData["DepoId"] = new SelectList(_context.Depolar, "DepoId", "DepoId", reyon.DepoId);
            return View(reyon);
        }

        // GET: Reyon/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Reyonlar == null)
            {
                return NotFound();
            }

            var reyon = await _context.Reyonlar
                .Include(r => r.Depo)
                .FirstOrDefaultAsync(m => m.ReyonId == id);
            if (reyon == null)
            {
                return NotFound();
            }

            return View(reyon);
        }

        // POST: Reyon/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Reyonlar == null)
            {
                return Problem("Entity set 'MarketContext.Reyonlar'  is null.");
            }
            var reyon = await _context.Reyonlar.FindAsync(id);
            if (reyon != null)
            {
                _context.Reyonlar.Remove(reyon);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ReyonExists(int id)
        {
            return (_context.Reyonlar?.Any(e => e.ReyonId == id)).GetValueOrDefault();
        }
    }
}
