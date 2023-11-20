using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GymWeb.Data;
using GymWeb.Models;
using System.Data;
using ClosedXML.Excel;
using GymWeb.ViewModel;
using Microsoft.AspNetCore.Hosting;
using DocumentFormat.OpenXml.Drawing.Diagrams;

namespace GymWeb.Controllers
{
    public class AlumnosController : Controller
    {
        private readonly ApplicationDbContext _context;

        private readonly IWebHostEnvironment _webHostEnvironment;
        public AlumnosController(ApplicationDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;

        }

        // GET: Alumnos
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Alumno.Include(a => a.Clases);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Alumnos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Alumno == null)
            {
                return NotFound();
            }

            var alumno = await _context.Alumno
                .Include(a => a.Clases)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (alumno == null)
            {
                return NotFound();
            }

            return View(alumno);
        }

        // GET: Alumnos/Create
        public IActionResult Create()
        {
            ViewData["ClaseRefId"] = new SelectList(_context.Clase, "Id", "Nombre");
            return View();
        }

        // POST: Alumnos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AlumnoViewModel model)
        {
            string uniqueFileName = UploadedFile(model);
            if (ModelState.IsValid)
            {
                Alumno alumno = new Alumno()
                {
                    Imagen = uniqueFileName,
                    Nombre = model.Nombre,
                    Apellido = model.Apellido,
                    Dni = model.Dni,
                    Telefono = model.Telefono,
                    Direccion = model.Direccion,
                    ClaseRefId = model.ClaseRefId,
                    FechaRegistro = model.FechaRegistro,

                };
                _context.Add(alumno);
                await _context.SaveChangesAsync();  
                return RedirectToAction(nameof(Index));
            }
            ViewData["ClaseRefId"] = new SelectList(_context.Clase, "Id", "Nombre", model.ClaseRefId);
            return View(model);
        }

        // GET: Alumnos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Alumno == null)
            {
                return NotFound();
            }

            var alumno = await _context.Alumno.FindAsync(id);
            AlumnoViewModel alumnoViewModel = new AlumnoViewModel()
            {
                Nombre = alumno.Nombre,
                Apellido = alumno.Apellido,
                Dni = alumno.Dni,
                Telefono = alumno.Telefono,
                Direccion = alumno.Direccion,
                ClaseRefId = alumno.ClaseRefId,
                FechaRegistro = alumno.FechaRegistro,

            };
            if (alumno == null)
            {
                return NotFound();
            }
            ViewData["ClaseRefId"] = new SelectList(_context.Clase, "Id", "Nombre", alumno.ClaseRefId);
            return View(alumnoViewModel);
        }

        // POST: Alumnos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id,AlumnoViewModel model)
        {
            string uniqueFileName = UploadedFile(model);
            if (id != model.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var alumno = await _context.Alumno.FindAsync(id);
                    alumno.Imagen = uniqueFileName;
                    alumno.Nombre = model.Nombre;
                    alumno.Apellido = model.Apellido;
                    alumno.Dni = model.Dni;
                    alumno.Telefono = model.Telefono;
                    alumno.Direccion = model.Direccion;
                    alumno.ClaseRefId = model.ClaseRefId;
                    alumno.FechaRegistro = model.FechaRegistro;
                    _context.Update(alumno);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AlumnoExists(model.Id))
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
            ViewData["ClaseRefId"] = new SelectList(_context.Clase, "Id", "Nombre", model.ClaseRefId);
            return View(model);
        }

        // GET: Alumnos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Alumno == null)
            {
                return NotFound();
            }

            var alumno = await _context.Alumno
                .Include(a => a.Clases)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (alumno == null)
            {
                return NotFound();
            }

            return View(alumno);
        }

        // POST: Alumnos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Alumno == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Alumno'  is null.");
            }
            var alumno = await _context.Alumno.FindAsync(id);
            if (alumno != null)
            {
                _context.Alumno.Remove(alumno);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AlumnoExists(int id)
        {
          return (_context.Alumno?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        [HttpGet]
        public async Task<FileResult> ExportarLibrosAExcel()
        {
            var alumno = await _context.Alumno.ToListAsync();
            var nombreArchivo = $"Alumnos.xlsx";
            return GenerarExcel(nombreArchivo, alumno);
        }

        private FileResult GenerarExcel(string nombreArchivo, IEnumerable<Alumno> alumnos)
        {
            DataTable datatable = new DataTable("Alumnos");
            datatable.Columns.AddRange(new DataColumn[]
            {
                new DataColumn ("Id"),
                new DataColumn ("Nombre"),
                new DataColumn ("Apellido"),
                new DataColumn ("Dni"),
                new DataColumn ("Edad"),
                new DataColumn ("Telefono"),
                new DataColumn ("Direccion"),
                new DataColumn ("Clase")
            });
            foreach (var alumno in alumnos)
            {
                datatable.Rows.Add(alumno.Id,
                    alumno.Nombre,
                    alumno.Apellido,
                    alumno.Dni,
                    alumno.Edad,
                    alumno.Telefono,
                    alumno.Direccion,
                    alumno.ClaseRefId
                    );
            }
            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.Worksheets.Add(datatable);

                using (MemoryStream stream = new MemoryStream())
                {
                    wb.SaveAs(stream);
                    return File(stream.ToArray(),
                        "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                        nombreArchivo);
                }
            }
        }
        private string UploadedFile(AlumnoViewModel model)
        {
            string uniqueFileName = null;

            if (model.ImagenAlumno != null)
            {
                string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "images");
                uniqueFileName = Guid.NewGuid().ToString() + "" + model.ImagenAlumno.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    model.ImagenAlumno.CopyTo(fileStream);
                }
            }
            return uniqueFileName;
        }
    }
}
