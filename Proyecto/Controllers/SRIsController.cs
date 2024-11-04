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
    public class SRIsController : Controller
    {
        private readonly ProyectoContext _context;

        public SRIsController(ProyectoContext context)
        {
            _context = context;
        }

        // GET: SRIs
        public async Task<IActionResult> Index()
        {
            var proyectoContext = _context.SRI.Include(s => s.Persona);
            return View(await proyectoContext.ToListAsync());
        }

        // GET: SRIs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sRI = await _context.SRI
                .Include(s => s.Persona)
                .FirstOrDefaultAsync(m => m.IdSRI == id);
            if (sRI == null)
            {
                return NotFound();
            }

            return View(sRI);
        }

        // GET: SRIs/Create
        public IActionResult Create()
        {
            ViewData["IdPersona"] = new SelectList(_context.Persona, "IdPersona", "Cedula");
            return View();
        }

        // POST: SRIs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdSRI,IdPersona,Trabajo,Ingresos_Mensuales,Deudas_Activas,Bienes")] SRI sRI)
        {
            if (ModelState.IsValid)
            {
                _context.Add(sRI);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdPersona"] = new SelectList(_context.Persona, "IdPersona", "Cedula", sRI.IdPersona);
            return View(sRI);
        }

        // GET: SRIs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sRI = await _context.SRI.FindAsync(id);
            if (sRI == null)
            {
                return NotFound();
            }
            ViewData["IdPersona"] = new SelectList(_context.Persona, "IdPersona", "Cedula", sRI.IdPersona);
            return View(sRI);
        }

        // POST: SRIs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdSRI,IdPersona,Trabajo,Ingresos_Mensuales,Deudas_Activas,Bienes")] SRI sRI)
        {
            if (id != sRI.IdSRI)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(sRI);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SRIExists(sRI.IdSRI))
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
            ViewData["IdPersona"] = new SelectList(_context.Persona, "IdPersona", "Cedula", sRI.IdPersona);
            return View(sRI);
        }

        // GET: SRIs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sRI = await _context.SRI
                .Include(s => s.Persona)
                .FirstOrDefaultAsync(m => m.IdSRI == id);
            if (sRI == null)
            {
                return NotFound();
            }

            return View(sRI);
        }

        // POST: SRIs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var sRI = await _context.SRI.FindAsync(id);
            if (sRI != null)
            {
                _context.SRI.Remove(sRI);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SRIExists(int id)
        {
            return _context.SRI.Any(e => e.IdSRI == id);
        }
    }
}
