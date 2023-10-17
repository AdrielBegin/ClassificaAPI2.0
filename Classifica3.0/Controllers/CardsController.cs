using Classifica3._0.Context;
using Classifica3._0.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Classifica3._0.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CardsController : Controller
    {
        private readonly AppDbContext _context;

        public CardsController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            if (_context.Cards != null)
            {
                var cards = await _context.Cards.ToListAsync();
                return Ok(cards);
            }
            else
            {
                return Problem("Entity set 'AppDbContext.Cards'  is null.", null, 500);
            }

        }
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Cards == null)
            {
                return NotFound("Não encontrado ou não existe");
            }

            var card = await _context.Cards.FirstOrDefaultAsync(m => m.CardId == id);

            if (card == null)
            {
                return NotFound("Não encontrado o id");
            }

            return Ok(card);
        }

        [HttpPost]
        public async Task<IActionResult> Create([Bind("CardId,Titulo,Descricao,DataCardCreated,DataCardUpdated")] Card card)
        {
            if (ModelState.IsValid)
            {
                card.Ativo();
                _context.Add(card);
                await _context.SaveChangesAsync();
                return Ok();
            }
            return Ok(card);
        }
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Cards == null)
            {
                return NotFound();
            }
            var card = await _context.Cards.FindAsync(id);

            if (card == null)
            {
                return NotFound();
            }
            return Ok(card);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, [Bind("CardId,Titulo,Descricao,DataCardCreated,DataCardUpdated")] Card card)
        {
            if (id != card.CardId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    card.DateUpdated();
                    _context.Update(card);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CardExists(card.CardId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return Ok();
            }
            return Ok(card);
        }
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Cards == null)
            {
                return NoContent();
            }

            var card = await _context.Cards.FirstOrDefaultAsync(m => m.CardId == id);

            if (card == null)
            {
                return NotFound();
            }
            return Ok(card);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Cards == null)
            {
                return Problem("Entity set 'AppDbContext.Cards'  is null.");
            }
            var card = await _context.Cards.FindAsync(id);
            if (card != null)
            {
                card.Inativo();
                _context.Cards.Update(card);
            }
            await _context.SaveChangesAsync();

            return Ok();

        }
        private bool CardExists(int id)
        {
            return (_context.Cards?.Any(e => e.CardId == id)).GetValueOrDefault();
        }
    }
}
