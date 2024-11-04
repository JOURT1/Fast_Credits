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
    public class LegalsController : Controller
    {
        private readonly ProyectoContext _context;

        public LegalsController(ProyectoContext context)
        {
            _context = context;
        }

        // GET: Legals
        public async Task<IActionResult> Index()
        {
            var proyectoContext = _context.Legal.Include(l => l.Persona);
            return View(await proyectoContext.ToListAsync());
        }

        // GET: Legals/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var legal = await _context.Legal
                .Include(l => l.Persona)
                .FirstOrDefaultAsync(m => m.IdLegal == id);
            if (legal == null)
            {
                return NotFound();
            }

            return View(legal);
        }

        // GET: Legals/Create
        public IActionResult Create()
        {
            ViewData["IdPersona"] = new SelectList(_context.Persona, "IdPersona", "Cedula");
            return View();
        }

        // POST: Legals/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdLegal,IdPersona,Denuncias,Antecedentes_Penales,Fraudes")] Legal legal)
        {
            if (ModelState.IsValid)
            {
                _context.Add(legal);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdPersona"] = new SelectList(_context.Persona, "IdPersona", "Cedula", legal.IdPersona);
            return View(legal);
        }

        // GET: Legals/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var legal = await _context.Legal.FindAsync(id);
            if (legal == null)
            {
                return NotFound();
            }
            ViewData["IdPersona"] = new SelectList(_context.Persona, "IdPersona", "Cedula", legal.IdPersona);
            return View(legal);
        }

        // POST: Legals/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdLegal,IdPersona,Denuncias,Antecedentes_Penales,Fraudes")] Legal legal)
        {
            if (id != legal.IdLegal)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(legal);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LegalExists(legal.IdLegal))
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
            ViewData["IdPersona"] = new SelectList(_context.Persona, "IdPersona", "Cedula", legal.IdPersona);
            return View(legal);
        }

        // GET: Legals/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var legal = await _context.Legal
                .Include(l => l.Persona)
                .FirstOrDefaultAsync(m => m.IdLegal == id);
            if (legal == null)
            {
                return NotFound();
            }

            return View(legal);
        }

        // POST: Legals/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var legal = await _context.Legal.FindAsync(id);
            if (legal != null)
            {
                _context.Legal.Remove(legal);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LegalExists(int id)
        {
            return _context.Legal.Any(e => e.IdLegal == id);
        }
    }
}
