using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using IdentityWithAngular.Core.Classes;
using IdentityWithAngular.Core.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace IdentityWithAngular.WebAPI.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class EmprestimosController  : ControllerBase
    {
        private readonly AppDbContext _context;

        public EmprestimosController(AppDbContext context) => _context = context;
    
        [HttpGet]
        public async Task<ActionResult<List<Emprestimo>>> GetEmprestimos(Emprestimo emprestimo)
        {
            return await _context.Emprestimos.Include(e => e.Livro).ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Emprestimo>> GetByIdEmprestimo(int id)
        {
            var emprestimo = await _context.Emprestimos
                .Include(e => e.Livro)
                .FirstOrDefaultAsync(e => e.Id == id);

            if (emprestimo == null)
            {
                return NotFound();
            }

            return emprestimo;
        }
    
        [HttpPost]
        public async Task<ActionResult<Emprestimo>> PostEmprestimos(Emprestimo emprestimo)
        {
            //lógica de verificação de disponibilidade antes
            _context.Emprestimos.Add(emprestimo);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetEmprestimos), new { id = emprestimo.Id }, emprestimo);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Emprestimo>> PutEmprestimos(int id, Emprestimo emprestimo)
        {
            if (id != emprestimo.Id)
            {
                return BadRequest();
            }

            _context.Entry(emprestimo).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteEmprestimos(int id)
        {

            var emprestimo = await _context.Emprestimos.FindAsync(id);
            if (emprestimo == null)
            {
                return NotFound();
            }

            _context.Emprestimos.Remove(emprestimo);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpPatch("{id}/devolver")]
        public async Task<ActionResult> DevolverEmprestimo(int id)
        {
            var emprestimo = await _context.Emprestimos.FindAsync(id);
            if (emprestimo == null)
            {
                return NotFound();
            }

            emprestimo.DataDevolucao = DateTime.Now;
            _context.Entry(emprestimo).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }


        

    }
}