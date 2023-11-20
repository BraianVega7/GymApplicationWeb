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
    public class CalendariosController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CalendariosController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Calendarios
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Calendario.Include(c => c.Clases);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Calendarios/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Calendario == null)
            {
                return NotFound();
            }

            var calendario = await _context.Calendario
                .Include(c => c.Clases)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (calendario == null)
            {
                return NotFound();
            }

            return View(calendario);
        }

        // GET: Calendarios/Create
        public IActionResult Create()
        {
            ViewData["ClaseRefId"] = new SelectList(_context.Clase, "Id", "Nombre");
            return View();
        }

        // POST: Calendarios/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ClaseRefId,Dia,HoraInicio,HoraFin")] Calendario calendario)
        {
            if (ModelState.IsValid)
            {
                _context.Add(calendario);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ClaseRefId"] = new SelectList(_context.Clase, "Id", "Nombre", calendario.ClaseRefId);
            return View(calendario);
        }

        // GET: Calendarios/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Calendario == null)
            {
                return NotFound();
            }

            var calendario = await _context.Calendario.FindAsync(id);
            if (calendario == null)
            {
                return NotFound();
            }
            ViewData["ClaseRefId"] = new SelectList(_context.Clase, "Id", "Nombre", calendario.ClaseRefId);
            return View(calendario);
        }

        // POST: Calendarios/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ClaseRefId,Dia,HoraInicio,HoraFin")] Calendario calendario)
        {
            if (id != calendario.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(calendario);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CalendarioExists(calendario.Id))
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
            ViewData["ClaseRefId"] = new SelectList(_context.Clase, "Id", "Nombre", calendario.ClaseRefId);
            return View(calendario);
        }

        // GET: Calendarios/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Calendario == null)
            {
                return NotFound();
            }

            var calendario = await _context.Calendario
                .Include(c => c.Clases)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (calendario == null)
            {
                return NotFound();
            }

            return View(calendario);
        }

        // POST: Calendarios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Calendario == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Calendario'  is null.");
            }
            var calendario = await _context.Calendario.FindAsync(id);
            if (calendario != null)
            {
                _context.Calendario.Remove(calendario);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CalendarioExists(int id)
        {
          return (_context.Calendario?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
