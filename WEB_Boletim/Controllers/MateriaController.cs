using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WEB_Boletim.Data;
using WEB_Boletim.Models;

namespace WEB_Boletim.Controllers
{
    [Authorize]
    public class MateriaController : Controller
    {
        private readonly WEB_BoletimContext _context;

        public MateriaController(WEB_BoletimContext context)
        {
            _context = context;
        }

        // GET: Materia
        public async Task<IActionResult> Index()
        {
            var wEB_BoletimContext = _context.Materia.Include(m => m.Situacoes);
            return View(await wEB_BoletimContext.ToListAsync());
        }

        // GET: Materia/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var materia = await _context.Materia
                .Include(m => m.Situacoes)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (materia == null)
            {
                return NotFound();
            }

            return View(materia);
        }

        // GET: Materia/Create
        public IActionResult Create()
        {
            ViewData["SituacaoID"] = new SelectList(_context.Set<Situacao>(), "ID", "SituacaoMateria");
            return View();
        }

        // POST: Materia/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,DescricaoMateria,DataDeCadastroMateria,SituacaoID")] Materia materia)
        {
            if (ModelState.IsValid)
            {
                _context.Add(materia);
                await _context.SaveChangesAsync();
                ViewBag._sucesso = "sucesso";
                return View();
            }
            ViewData["SituacaoID"] = new SelectList(_context.Set<Situacao>(), "ID", "SituacaoMateria", materia.SituacaoID);
            return View(materia);
        }

        // GET: Materia/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var materia = await _context.Materia.FindAsync(id);
            if (materia == null)
            {
                return NotFound();
            }
            ViewData["SituacaoID"] = new SelectList(_context.Set<Situacao>(), "ID", "SituacaoMateria", materia.SituacaoID);
            return View(materia);
        }

        // POST: Materia/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,DescricaoMateria,DataDeCadastroMateria,SituacaoID")] Materia materia)
        {
            if (id != materia.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(materia);
                    await _context.SaveChangesAsync();
                    ViewBag._sucesso = "sucesso";
                    return View();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MateriaExists(materia.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            }
            ViewData["SituacaoID"] = new SelectList(_context.Set<Situacao>(), "ID", "SituacaoMateria", materia.SituacaoID);
            return View(materia);
        }

        // GET: Materia/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var materia = await _context.Materia
                .Include(m => m.Situacoes)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (materia == null)
            {
                return NotFound();
            }

            return View(materia);
        }

        // POST: Materia/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var materia = await _context.Materia.FindAsync(id);
            _context.Materia.Remove(materia);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MateriaExists(int id)
        {
            return _context.Materia.Any(e => e.ID == id);
        }
    }
}
