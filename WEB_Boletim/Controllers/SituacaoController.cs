using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WEB_Boletim.Data;
using WEB_Boletim.Models;

namespace WEB_Boletim.Controllers
{
    public class SituacaoController : Controller
    {
        private readonly WEB_BoletimContext _context;

        public SituacaoController(WEB_BoletimContext context)
        {
            _context = context;
        }

        // GET: Situacao
        public async Task<IActionResult> Index()
        {
            return View(await _context.Situacao.ToListAsync());
        }

        // GET: Situacao/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var situacao = await _context.Situacao
                .FirstOrDefaultAsync(m => m.ID == id);
            if (situacao == null)
            {
                return NotFound();
            }

            return View(situacao);
        }

        // GET: Situacao/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Situacao/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,SituacaoMateria")] Situacao situacao)
        {
            if (ModelState.IsValid)
            {
                _context.Add(situacao);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(situacao);
        }

        // GET: Situacao/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var situacao = await _context.Situacao.FindAsync(id);
            if (situacao == null)
            {
                return NotFound();
            }
            return View(situacao);
        }

        // POST: Situacao/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,SituacaoMateria")] Situacao situacao)
        {
            if (id != situacao.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(situacao);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SituacaoExists(situacao.ID))
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
            return View(situacao);
        }

        // GET: Situacao/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var situacao = await _context.Situacao
                .FirstOrDefaultAsync(m => m.ID == id);
            if (situacao == null)
            {
                return NotFound();
            }

            return View(situacao);
        }

        // POST: Situacao/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var situacao = await _context.Situacao.FindAsync(id);
            _context.Situacao.Remove(situacao);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SituacaoExists(int id)
        {
            return _context.Situacao.Any(e => e.ID == id);
        }
    }
}
