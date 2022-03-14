#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using beadando.Data;
using beadando.Models;
using Microsoft.AspNetCore.Authorization;

namespace beadando.Controllers
{
    public class AdatmodelsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AdatmodelsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Adatmodels
        public async Task<IActionResult> Index(string elnevezeskereses, string kategoriaKereses)
        {
            Arukereses keresoAru = new Arukereses();
            var Aru = _context.Adatmodel.Select(x => x);
            if (!string.IsNullOrEmpty(elnevezeskereses))
            {
                keresoAru.elnevezeskereses = elnevezeskereses;
                Aru = Aru.Where(x => x.Elnevezes.Contains(elnevezeskereses));
            }

            if (!string.IsNullOrEmpty(kategoriaKereses))
            {
                keresoAru.kategoriaKereses = kategoriaKereses;
                Aru = Aru.Where(x => x.Kategoria.Equals(kategoriaKereses));
            }

            keresoAru.kategorialista = new SelectList(await _context.Adatmodel.Select(x => x.Kategoria).Distinct().ToListAsync());
            keresoAru.Aru = await Aru.ToListAsync();

            return View(keresoAru);
        }

        // GET: Adatmodels/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var adatmodel = await _context.Adatmodel
                .FirstOrDefaultAsync(m => m.Id == id);
            if (adatmodel == null)
            {
                return NotFound();
            }

            return View(adatmodel);
        }

        // GET: Adatmodels/Create
        [Authorize]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Adatmodels/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Elnevezes,Kategoria,Csomagolasiegyseg,Darabszam")] Adatmodel adatmodel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(adatmodel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(adatmodel);
        }

        // GET: Adatmodels/Edit/5
        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var adatmodel = await _context.Adatmodel.FindAsync(id);
            if (adatmodel == null)
            {
                return NotFound();
            }
            return View(adatmodel);
        }

        // POST: Adatmodels/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Elnevezes,Kategoria,Csomagolasiegyseg,Darabszam")] Adatmodel adatmodel)
        {
            if (id != adatmodel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(adatmodel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AdatmodelExists(adatmodel.Id))
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
            return View(adatmodel);
        }

        // GET: Adatmodels/Delete/5
        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var adatmodel = await _context.Adatmodel
                .FirstOrDefaultAsync(m => m.Id == id);
            if (adatmodel == null)
            {
                return NotFound();
            }

            return View(adatmodel);
        }

        // POST: Adatmodels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var adatmodel = await _context.Adatmodel.FindAsync(id);
            _context.Adatmodel.Remove(adatmodel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AdatmodelExists(int id)
        {
            return _context.Adatmodel.Any(e => e.Id == id);
        }
    }
}
