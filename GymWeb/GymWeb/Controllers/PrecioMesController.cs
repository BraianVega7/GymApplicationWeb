using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GymWeb.Data;
using GymWeb.Models;

namespace GymWeb.Controllers
{
    public class PrecioMesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PrecioMesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: PrecioMes
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.PrecioMes.Include(p => p.Clases);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: PrecioMes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.PrecioMes == null)
            {
                return NotFound();
            }

            var precioMes = await _context.PrecioMes
                .Include(p => p.Clases)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (precioMes == null)
            {
                return NotFound();
            }

            return View(precioMes);
        }

        // GET: PrecioMes/Create
        public IActionResult Create()
        {
            ViewData["ClaseRefId"] = new SelectList(_context.Clase, "Id", "Descripcion");
            return View();
        }

        // POST: PrecioMes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ClaseRefId,Cuota")] PrecioMes precioMes)
        {
            if (ModelState.IsValid)
            {
                _context.Add(precioMes);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ClaseRefId"] = new SelectList(_context.Clase, "Id", "Descripcion", precioMes.ClaseRefId);
            return View(precioMes);
        }

        // GET: PrecioMes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.PrecioMes == null)
            {
                return NotFound();
            }

            var precioMes = await _context.PrecioMes.FindAsync(id);
            if (precioMes == null)
            {
                return NotFound();
            }
            ViewData["ClaseRefId"] = new SelectList(_context.Clase, "Id", "Descripcion", precioMes.ClaseRefId);
            return View(precioMes);
        }

        // POST: PrecioMes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ClaseRefId,Cuota")] PrecioMes precioMes)
        {
            if (id != precioMes.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(precioMes);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PrecioMesExists(precioMes.Id))
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
            ViewData["ClaseRefId"] = new SelectList(_context.Clase, "Id", "Descripcion", precioMes.ClaseRefId);
            return View(precioMes);
        }

        // GET: PrecioMes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.PrecioMes == null)
            {
                return NotFound();
            }

            var precioMes = await _context.PrecioMes
                .Include(p => p.Clases)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (precioMes == null)
            {
                return NotFound();
            }

            return View(precioMes);
        }

        // POST: PrecioMes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.PrecioMes == null)
            {
                return Problem("Entity set 'ApplicationDbContext.PrecioMes'  is null.");
            }
            var precioMes = await _context.PrecioMes.FindAsync(id);
            if (precioMes != null)
            {
                _context.PrecioMes.Remove(precioMes);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PrecioMesExists(int id)
        {
          return (_context.PrecioMes?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
