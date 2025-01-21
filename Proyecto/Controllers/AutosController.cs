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
    public class AutosController : Controller
    {
        private readonly ProyectoContext _context;

        public AutosController(ProyectoContext context)
        {
            _context = context;
        }

        // GET: Autos
        public async Task<IActionResult> Index()
        {
            var proyectoContext = _context.Autos.Include(a => a.Persona);
            return View(await proyectoContext.ToListAsync());
        }

        // GET: Autos/Detailss/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var autos = await _context.Autos
                .Include(a => a.Persona)
                .FirstOrDefaultAsync(m => m.IdAutos == id);
            if (autos == null)
            {
                return NotFound();
            }

            return View(autos);
        }

        // GET: Autos/Create
        public IActionResult Create()
        {
            ViewData["IdPersona"] = new SelectList(_context.Persona, "IdPersona", "Cedula");
            return View();
        }

        // POST: Autos/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdAutos,IdPersona,Marca,Modelo,ano,precio")] Autos autos)
        {
            if (ModelState.IsValid)
            {
                _context.Add(autos);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdPersona"] = new SelectList(_context.Persona, "IdPersona", "Cedula", autos.IdPersona);
            return View(autos);
        }

        // GET: Autos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var autos = await _context.Autos.FindAsync(id);
            if (autos == null)
            {
                return NotFound();
            }
            ViewData["IdPersona"] = new SelectList(_context.Persona, "IdPersona", "Cedula", autos.IdPersona);
            return View(autos);
        }

        // POST: Autos/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdAutos,IdPersona,Marca,Modelo,ano,precio")] Autos autos)
        {
            if (id != autos.IdAutos)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(autos);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AutosExists(autos.IdAutos))
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
            ViewData["IdPersona"] = new SelectList(_context.Persona, "IdPersona", "Cedula", autos.IdPersona);
            return View(autos);
        }

        // GET: Autos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var autos = await _context.Autos
                .Include(a => a.Persona)
                .FirstOrDefaultAsync(m => m.IdAutos == id);
            if (autos == null)
            {
                return NotFound();
            }

            return View(autos);
        }

        // POST: Autos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var autos = await _context.Autos.FindAsync(id);
            if (autos != null)
            {
                _context.Autos.Remove(autos);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AutosExists(int id)
        {
            return _context.Autos.Any(e => e.IdAutos == id);
        }

        // API Methods

        [HttpGet]
        [Route("api/Autos")]
        public async Task<ActionResult<IEnumerable<Autos>>> GetAutos()
        {
            return await _context.Autos.Include(a => a.Persona).ToListAsync();
        }

        [HttpGet]
        [Route("api/Autos/{id}")]
        public async Task<ActionResult<Autos>> GetAuto(int id)
        {
            var auto = await _context.Autos.Include(a => a.Persona).FirstOrDefaultAsync(a => a.IdAutos == id);

            if (auto == null)
            {
                return NotFound();
            }

            return auto;
        }

        [HttpPost]
        [Route("api/Autos")]
        public async Task<ActionResult<Autos>> PostAuto(Autos autos)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Autos.Add(autos);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAuto", new { id = autos.IdAutos }, autos);
        }

        [HttpPut]
        [Route("api/Autos/{id}")]
        public async Task<IActionResult> PutAuto(int id, Autos autos)
        {
            if (id != autos.IdAutos)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Entry(autos).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AutosExists(id))
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
        [Route("api/Autos/{id}")]
        public async Task<IActionResult> DeleteAuto(int id)
        {
            var auto = await _context.Autos.FindAsync(id);
            if (auto == null)
            {
                return NotFound();
            }

            _context.Autos.Remove(auto);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
