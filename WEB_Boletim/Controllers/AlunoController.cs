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
    public class AlunoController : Controller
    {
        private readonly WEB_BoletimContext _context;

        public AlunoController(WEB_BoletimContext context)
        {
            _context = context;
        }

        // GET: Aluno
        public async Task<IActionResult> Index(string searchString)
        {
           
            var wEB_BoletimContext = _context.Aluno.Include(a => a.Cursos);
            return View(await wEB_BoletimContext.ToListAsync());
        }

        // GET: Aluno/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var aluno = await _context.Aluno
                .Include(a => a.Cursos)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (aluno == null)
            {
                return NotFound();
            }

            return View(aluno);
        }

        public static bool ValidaCPF(string vrCPF)

        {
            string valor = vrCPF.Replace(".", "");

            valor = valor.Replace("-", "");



            if (valor.Length != 11)

                return false;



            bool igual = true;

            for (int i = 1; i < 11 && igual; i++)

                if (valor[i] != valor[0])

                    igual = false;



            if (igual || valor == "12345678909")

                return false;



            int[] numeros = new int[11];



            for (int i = 0; i < 11; i++)

                numeros[i] = int.Parse(

                  valor[i].ToString());



            int soma = 0;

            for (int i = 0; i < 9; i++)

                soma += (10 - i) * numeros[i];



            int resultado = soma % 11;



            if (resultado == 1 || resultado == 0)

            {

                if (numeros[9] != 0)

                    return false;

            }

            else if (numeros[9] != 11 - resultado)

                return false;



            soma = 0;

            for (int i = 0; i < 10; i++)

                soma += (11 - i) * numeros[i];



            resultado = soma % 11;



            if (resultado == 1 || resultado == 0)

            {

                if (numeros[10] != 0)

                    return false;

            }

            else

                if (numeros[10] != 11 - resultado)

                return false;



            return true;

        }

        // GET: Aluno/Create
        public IActionResult Create()
        {
            ViewData["CursoID"] = new SelectList(_context.Set<Curso>(), "ID", "CursoFaculdade");
            return View();
        }

        // POST: Aluno/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,NomeAluno,SobrenomeAluno,DataNascimentoAluno,CPFAluno,CursoID")] Aluno aluno)
        {
            if (String.IsNullOrEmpty(aluno.CPFAluno))
            {
                ViewData["CursoID"] = new SelectList(_context.Set<Curso>(), "ID", "CursoFaculdade");
                return View();
            }
            else {
                if (aluno.DataNascimentoAluno.AddYears(18) < DateTime.Now && ValidaCPF(aluno.CPFAluno) == true)
                {
                    if (ModelState.IsValid)
                    {
                        _context.Add(aluno);
                        await _context.SaveChangesAsync();
                        ViewBag._sucesso = "sucesso";
                        return View();
                    }
                }
                else
                {
                    if (ValidaCPF(aluno.CPFAluno) == false)
                    {
                        ViewBag._erroCPF = "Digite um CPF válido.";
                        ViewData["CursoID"] = new SelectList(_context.Set<Curso>(), "ID", "CursoFaculdade");
                        return View();
                    }
                    else
                    {
                        ViewBag._erroIdade = "Aluno deve ser maior que 18 anos.";
                        ViewData["CursoID"] = new SelectList(_context.Set<Curso>(), "ID", "CursoFaculdade");
                        return View();
                    }
                }
            }
            ViewData["CursoID"] = new SelectList(_context.Set<Curso>(), "ID", "CursoFaculdade");
            return View(aluno);
        }

        // GET: Aluno/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var aluno = await _context.Aluno.FindAsync(id);
            if (aluno == null)
            {
                return NotFound();
            }
            ViewData["CursoID"] = new SelectList(_context.Set<Curso>(), "ID", "CursoFaculdade", aluno.CursoID);
            return View(aluno);
        }

        // POST: Aluno/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,NomeAluno,SobrenomeAluno,DataNascimentoAluno,CPFAluno,CursoID")] Aluno aluno)
        {
            if (id != aluno.ID)
            {
                return NotFound();
            }
            if (aluno.DataNascimentoAluno.AddYears(18) < DateTime.Now && ValidaCPF(aluno.CPFAluno) == true)
            {
                if (ModelState.IsValid)
                {
                    try
                    {
                        _context.Update(aluno);
                        await _context.SaveChangesAsync();
                        ViewBag._sucesso = "sucesso";
                        return View();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!AlunoExists(aluno.ID))
                        {
                            return NotFound();
                        }
                        else
                        {
                            throw;
                        }
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            else
            {
                if (ValidaCPF(aluno.CPFAluno) == false)
                {
                    ViewBag._erroCPF = "Digite um CPF válido.";
                    ViewData["CursoID"] = new SelectList(_context.Set<Curso>(), "ID", "CursoFaculdade");
                    return View();
                }
                else
                {
                    ViewBag._erroIdade = "Aluno deve ser maior que 18 anos.";
                    ViewData["CursoID"] = new SelectList(_context.Set<Curso>(), "ID", "CursoFaculdade");
                    return View();
                }
            }

            //ViewData["CursoID"] = new SelectList(_context.Set<Curso>(), "ID", "CursoFaculdade", aluno.CursoID);
            //return View(aluno);
        }

        // GET: Aluno/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var aluno = await _context.Aluno
                .Include(a => a.Cursos)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (aluno == null)
            {
                return NotFound();
            }

            return View(aluno);
        }

        // POST: Aluno/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var aluno = await _context.Aluno.FindAsync(id);
            _context.Aluno.Remove(aluno);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AlunoExists(int id)
        {
            return _context.Aluno.Any(e => e.ID == id);
        }
    }
}
