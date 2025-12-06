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
    [Route("api/[controller]")]
    [ApiController]

    public class LivrosController : ControllerBase
    {
        private readonly AppDbContext _context;

        public LivrosController(AppDbContext context) => _context = context;

        [HttpGet]
        public async Task<ActionResult<List<Livros>>> GetLivros()
        {
            //var livros = await _context.Livros.ToListAsync();
            
            return await _context.Livros.ToListAsync();

        }


        [HttpGet("{id}")]
        public async Task<ActionResult<Livros>> GetByIdLivros(int id)
        {
            var livro = await _context.Livros
                .Include(l => l.Emprestimos)
                .FirstOrDefaultAsync(l => l.Id == id);

            if (livro == null)
            {
                return NotFound();
            }

            return livro;
        }



        [HttpPost]
        public async Task<ActionResult<Livros>> PostLivros(Livros livro)
        {
            _context.Livros.Add(livro);
            await  _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetLivros), new { id = livro.Id }, livro);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Livros>> PutLivros(int id, Livros livro)
        {
            if (id != livro.Id)
            {
                return BadRequest();
            }

            _context.Entry(livro).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteLivros(int id)
        {
            var livro = await _context.Livros.FindAsync(id);
            if (livro == null)
            {
                return NotFound();
            }

            _context.Livros.Remove(livro);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}