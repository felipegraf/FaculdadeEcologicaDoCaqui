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
    public class NotaController : Controller
    {
        private readonly WEB_BoletimContext _context;

        public NotaController(WEB_BoletimContext context)
        {
            _context = context;
        }


        // GET: Nota
        public async Task<IActionResult> Index(string searchString)
        {
            ViewData["AlunoID"] = new SelectList(_context.Aluno, "NomeAluno", "NomeAluno");
           
            var alunos = from a in _context.Nota.Include(n => n.Materias)
                         select a;
            
              
            if (!string.IsNullOrEmpty(searchString))
            {
                ViewBag._pesquisaAlunos = searchString;
                alunos = alunos.Where(a => a.Alunos.NomeAluno.Contains(searchString));
                return View(await alunos.ToListAsync());
            }


            return View(await alunos.ToListAsync());
        }

        // GET: Nota/Details/5  
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var nota = await _context.Nota
                .Include(n => n.Alunos)
                .Include(n => n.Materias)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (nota == null)
            {
                return NotFound();
            }

            return View(nota);
        }

        // GET: Nota/Create
        public IActionResult Create()
        {
            ViewData["AlunoID"] = new SelectList(_context.Aluno, "ID", "NomeAluno");
            ViewData["MateriaID"] = new SelectList(_context.Materia, "ID", "DescricaoMateria");
            return View();
        }

        // POST: Nota/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,AlunoID,MateriaID,NotaAluno")] Nota nota)
        {
            if (nota.NotaAluno >= 0 && nota.NotaAluno <= 10)
            {
                if (ModelState.IsValid)
                {
                    _context.Add(nota);
                    await _context.SaveChangesAsync();
                    ViewBag._sucesso = "sucesso";
                    return View();
                }
            }
            else
            {
                ViewBag._erroNota = "Digite uma nota entre 0 e 10.";
            }
            ViewData["AlunoID"] = new SelectList(_context.Aluno, "ID", "NomeAluno", nota.AlunoID);
            ViewData["MateriaID"] = new SelectList(_context.Materia, "ID", "DescricaoMateria", nota.MateriaID);
            return View(nota);
        }

        // GET: Nota/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var nota = await _context.Nota.FindAsync(id);
            if (nota == null)
            {
                return NotFound();
            }
            ViewData["AlunoID"] = new SelectList(_context.Aluno, "ID", "NomeAluno", nota.AlunoID);
            ViewData["MateriaID"] = new SelectList(_context.Materia, "ID", "DescricaoMateria", nota.MateriaID);
            return View(nota);
        }

        // POST: Nota/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,AlunoID,MateriaID,NotaAluno")] Nota nota)
        {
            if (id != nota.ID)
            {
                return NotFound();
            }

            if (nota.NotaAluno >= 0 && nota.NotaAluno <= 10) 
            {

                if (ModelState.IsValid)
                {
                    try
                    {
                        _context.Update(nota);
                        ViewBag._sucesso = "sucesso";
                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!NotaExists(nota.ID))
                        {
                            return NotFound();
                        }
                        else
                        {
                            throw;
                        }
                    }
                }
      
            }
            else
            {
                ViewBag._erroNota = "Digite uma nota entre 0 e 10.";
            }
            ViewData["AlunoID"] = new SelectList(_context.Aluno, "ID", "NomeAluno", nota.AlunoID);
            ViewData["MateriaID"] = new SelectList(_context.Materia, "ID", "DescricaoMateria", nota.MateriaID);
            return View(nota);
        }

        // GET: Nota/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var nota = await _context.Nota
                .Include(n => n.Alunos)
                .Include(n => n.Materias)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (nota == null)
            {
                return NotFound();
            }

            return View(nota);
        }

        // POST: Nota/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var nota = await _context.Nota.FindAsync(id);
            _context.Nota.Remove(nota);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool NotaExists(int id)
        {
            return _context.Nota.Any(e => e.ID == id);
        }
    }
}
