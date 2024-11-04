using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Proyecto.Data;
using Proyecto.Models;

namespace Proyecto.Controllers
{
    public class CivilsController : Controller
    {
        private readonly ProyectoContext _context;

        public CivilsController(ProyectoContext context)
        {
            _context = context;
        }

        // GET: Civils
        public async Task<IActionResult> Index()
        {
            var proyectoContext = _context.Civil.Include(c => c.Persona);
            return View(await proyectoContext.ToListAsync());
        }

        // GET: Civils/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var civil = await _context.Civil
                .Include(c => c.Persona)
                .FirstOrDefaultAsync(m => m.IdCivil == id);
            if (civil == null)
            {
                return NotFound();
            }

            return View(civil);
        }

        // GET: Civils/Create
        public IActionResult Create()
        {
            ViewData["IdPersona"] = new SelectList(_context.Persona, "IdPersona", "Cedula");
            return View();
        }

        // POST: Civils/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdCivil,IdPersona,Casado,Hijos")] Civil civil)
        {
            if (ModelState.IsValid)
            {
                _context.Add(civil);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdPersona"] = new SelectList(_context.Persona, "IdPersona", "Cedula", civil.IdPersona);
            return View(civil);
        }

        // GET: Civils/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var civil = await _context.Civil.FindAsync(id);
            if (civil == null)
            {
                return NotFound();
            }
            ViewData["IdPersona"] = new SelectList(_context.Persona, "IdPersona", "Cedula", civil.IdPersona);
            return View(civil);
        }

        // POST: Civils/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdCivil,IdPersona,Casado,Hijos")] Civil civil)
        {
            if (id != civil.IdCivil)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(civil);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CivilExists(civil.IdCivil))
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
            ViewData["IdPersona"] = new SelectList(_context.Persona, "IdPersona", "Cedula", civil.IdPersona);
            return View(civil);
        }

        // GET: Civils/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var civil = await _context.Civil
                .Include(c => c.Persona)
                .FirstOrDefaultAsync(m => m.IdCivil == id);
            if (civil == null)
            {
                return NotFound();
            }

            return View(civil);
        }

        // POST: Civils/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var civil = await _context.Civil.FindAsync(id);
            if (civil != null)
            {
                _context.Civil.Remove(civil);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CivilExists(int id)
        {
            return _context.Civil.Any(e => e.IdCivil == id);
        }
    }
}
