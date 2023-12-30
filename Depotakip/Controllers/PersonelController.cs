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
    public class PersonelController : Controller
    {
        private readonly MarketDbContext _context;

        private readonly IWebHostEnvironment _hostEnvironment;  //Image

        public PersonelController(MarketDbContext context, IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            this._hostEnvironment = hostEnvironment;
        }

        // GET: Personel
        public async Task<IActionResult> Index()
        {
            var marketContext = _context.Personeller.Include(p => p.Depo);
            return View(await marketContext.ToListAsync());
        }

        // GET: Personel/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Personeller == null)
            {
                return NotFound();
            }

            var personel = await _context.Personeller
                .Include(p => p.Depo)
                .FirstOrDefaultAsync(m => m.PersonelId == id);
            if (personel == null)
            {
                return NotFound();
            }

            return View(personel);
        }

        // GET: Personel/Create
        public IActionResult Create()
        {
            ViewData["DepoId"] = new SelectList(_context.Depolar, "DepoId", "Ad");
            return View();
        }

 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PersonelId,Ad,Soyad,Maas,DogumTarihi,ImageFile,DepoId")] Personel personel)
        {
           
            string wwwrootpath = _hostEnvironment.WebRootPath;
            string fileName = Path.GetFileNameWithoutExtension(personel.ImageFile.FileName);  
            string extension = Path.GetExtension(personel.ImageFile.FileName);
            personel.PersonelFoto = fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;  
            string path = Path.Combine(wwwrootpath + "/Image/", fileName); 
            using (var filestream = new FileStream(path, FileMode.Create))
            {
                await personel.ImageFile.CopyToAsync(filestream);

            }
            _context.Add(personel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
           
            ViewData["DepoId"] = new SelectList(_context.Depolar, "DepoId", "DepoId", personel.DepoId);
            return View(personel);
        }

        // GET: Personel/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Personeller == null)
            {
                return NotFound();
            }

            var personel = await _context.Personeller.FindAsync(id);
            if (personel == null)
            {
                return NotFound();
            }
            ViewData["DepoId"] = new SelectList(_context.Depolar, "DepoId", "Ad", personel.DepoId);
          
            return View(personel);
        }

       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PersonelId,Ad,Soyad,Maas,DogumTarihi,ImageFile,DepoId")] Personel personel)
        {


            string wwwrootpath = _hostEnvironment.WebRootPath;
            string fileName = Path.GetFileNameWithoutExtension(personel.ImageFile.FileName);
            string extension = Path.GetExtension(personel.ImageFile.FileName);
            personel.PersonelFoto = fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
            string path = Path.Combine(wwwrootpath + "/Image/", fileName);
            using (var filestream = new FileStream(path, FileMode.Create))
            {
                await personel.ImageFile.CopyToAsync(filestream);

            }
            if (id != personel.PersonelId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(personel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PersonelExists(personel.PersonelId))
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
            ViewData["DepoId"] = new SelectList(_context.Depolar, "DepoId", "DepoId", personel.DepoId);
            

            return View(personel);
        }

        // GET: Personel/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Personeller == null)
            {
                return NotFound();
            }

            var personel = await _context.Personeller
                .Include(p => p.Depo)
                .FirstOrDefaultAsync(m => m.PersonelId == id);
            if (personel == null)
            {
                return NotFound();
            }

            return View(personel);
        }

        // POST: Personel/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Personeller == null)
            {
                return Problem("Entity set 'MarketContext.Personeller'  is null.");
            }
            var personel = await _context.Personeller.FindAsync(id);
            if (personel != null)
            {
                _context.Personeller.Remove(personel);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PersonelExists(int id)
        {
            return (_context.Personeller?.Any(e => e.PersonelId == id)).GetValueOrDefault();
        }
    }
}
