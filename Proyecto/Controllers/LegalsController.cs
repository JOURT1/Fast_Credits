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

        // API Methods

        [HttpGet]
        [Route("api/Legals")]
        public async Task<ActionResult<IEnumerable<Legal>>> GetLegals()
        {
            return await _context.Legal.Include(l => l.Persona).ToListAsync();
        }

        [HttpGet]
        [Route("api/Legals/{id}")]
        public async Task<ActionResult<Legal>> GetLegal(int id)
        {
            var legal = await _context.Legal.Include(l => l.Persona).FirstOrDefaultAsync(l => l.IdLegal == id);

            if (legal == null)
            {
                return NotFound();
            }

            return legal;
        }

        [HttpPost]
        [Route("api/Legals")]
        public async Task<ActionResult<Legal>> PostLegal(Legal legal)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Legal.Add(legal);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetLegal", new { id = legal.IdLegal }, legal);
        }

        [HttpPut]
        [Route("api/Legals/{id}")]
        public async Task<IActionResult> PutLegal(int id, Legal legal)
        {
            if (id != legal.IdLegal)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Entry(legal).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LegalExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        [HttpDelete]
        [Route("api/Legals/{id}")]
        public async Task<IActionResult> DeleteLegal(int id)
        {
            var legal = await _context.Legal.FindAsync(id);
            if (legal == null)
            {
                return NotFound();
            }

            _context.Legal.Remove(legal);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
