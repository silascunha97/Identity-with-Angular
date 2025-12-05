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
{   [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class LivrosController : ControllerBase
    {
        private readonly AppDbContext _context;

        

        public LivrosController(AppDbContext context) => _context = context;


        [HttpGet]
        public async Task<ActionResult<List<Livros>>> GetLivros()
        {
            //var livros = await _context.Livros.ToListAsync();
            
            return await _context.Livros.Include(l => l.Emprestimos).ToListAsync();

        }

        [HttpPost]
        public async Task<ActionResult<Livros>> PostLivros(Livros livro)
        {
            _context.Livros.Add(livro);
            await  _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetLivros), new { id = livro.Id }, livro);
        }
    }
}