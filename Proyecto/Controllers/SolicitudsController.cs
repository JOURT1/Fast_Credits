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
    public class SolicitudsController : Controller
    {
        private readonly ProyectoContext _context;

        public SolicitudsController(ProyectoContext context)
        {
            _context = context;
        }

        // GET: Solicituds
        public async Task<IActionResult> Index(string cedula)
        {
            if (string.IsNullOrEmpty(cedula))
            {
                return View(new List<Civil>());
            }

            var civilsQuery = _context.Civil
                .Include(c => c.Persona)
                .ThenInclude(p => p.SRI)
                .Include(d => d.Persona)
                .ThenInclude(p => p.Legal)
                .Include(d => d.Persona)
                .ThenInclude(p => p.Autos)
                .Where(c => c.Persona.Cedula == cedula);

            var civils = await civilsQuery.ToListAsync();
            return View(civils);
        }

        // GET: Solicituds/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var solicitud = await _context.Solicitud
                .Include(s => s.Persona)
                .FirstOrDefaultAsync(m => m.IdSolicitud == id);
            if (solicitud == null)
            {
                return NotFound();
            }

            return View(solicitud);
        }

        // GET: Solicituds/Create
        public IActionResult Create()
        {
            ViewData["IdPersona"] = new SelectList(_context.Persona, "IdPersona", "Cedula");
            return View();
        }

        // POST: Solicituds/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdSolicitud,IdPersona")] Solicitud solicitud)
        {
            if (ModelState.IsValid)
            {
                _context.Add(solicitud);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdPersona"] = new SelectList(_context.Persona, "IdPersona", "Cedula", solicitud.IdPersona);
            return View(solicitud);
        }

        // GET: Solicituds/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var solicitud = await _context.Solicitud.FindAsync(id);
            if (solicitud == null)
            {
                return NotFound();
            }
            ViewData["IdPersona"] = new SelectList(_context.Persona, "IdPersona", "Cedula", solicitud.IdPersona);
            return View(solicitud);
        }

        // POST: Solicituds/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdSolicitud,IdPersona")] Solicitud solicitud)
        {
            if (id != solicitud.IdSolicitud)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(solicitud);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SolicitudExists(solicitud.IdSolicitud))
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
            ViewData["IdPersona"] = new SelectList(_context.Persona, "IdPersona", "Cedula", solicitud.IdPersona);
            return View(solicitud);
        }

        // GET: Solicituds/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var solicitud = await _context.Solicitud
                .Include(s => s.Persona)
                .FirstOrDefaultAsync(m => m.IdSolicitud == id);
            if (solicitud == null)
            {
                return NotFound();
            }

            return View(solicitud);
        }

        // POST: Solicituds/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var solicitud = await _context.Solicitud.FindAsync(id);
            if (solicitud != null)
            {
                _context.Solicitud.Remove(solicitud);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SolicitudExists(int id)
        {
            return _context.Solicitud.Any(e => e.IdSolicitud == id);
        }

        // API Methods

        [HttpGet]
        [Route("api/Solicituds")]
        public async Task<ActionResult<IEnumerable<Solicitud>>> GetSolicituds()
        {
            return await _context.Solicitud.Include(s => s.Persona).ToListAsync();
        }

        [HttpGet]
        [Route("api/Solicituds/{id}")]
        public async Task<ActionResult<Solicitud>> GetSolicitud(int id)
        {
            var solicitud = await _context.Solicitud.Include(s => s.Persona).FirstOrDefaultAsync(s => s.IdSolicitud == id);

            if (solicitud == null)
            {
                return NotFound();
            }

            return solicitud;
        }

        [HttpPost]
        [Route("api/Solicituds")]
        public async Task<ActionResult<Solicitud>> PostSolicitud(Solicitud solicitud)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Solicitud.Add(solicitud);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSolicitud", new { id = solicitud.IdSolicitud }, solicitud);
        }

        [HttpPut]
        [Route("api/Solicituds/{id}")]
        public async Task<IActionResult> PutSolicitud(int id, Solicitud solicitud)
        {
            if (id != solicitud.IdSolicitud)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Entry(solicitud).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SolicitudExists(id))
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
        [Route("api/Solicituds/{id}")]
        public async Task<IActionResult> DeleteSolicitud(int id)
        {
            var solicitud = await _context.Solicitud.FindAsync(id);
            if (solicitud == null)
            {
                return NotFound();
            }

            _context.Solicitud.Remove(solicitud);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
