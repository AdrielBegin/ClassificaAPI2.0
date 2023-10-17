using Classifica3._0.Context;
using Classifica3._0.Model;
using Classifica3._0.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Classifica3._0.Controllers
{
    [Route("api/[controller]")]
    [Consumes("application/json")]
    [Produces("application/json")]
    [ApiController]
    public class CardsController : ControllerBase
    {
        private readonly ICardsRepository _cardsRepository;

        public CardsController(ICardsRepository cardsRepository)
        {
            _cardsRepository = cardsRepository;
        }

        [HttpGet("GetAll")]
        public async Task<IEnumerable<Card>> GetAllCards()
        {
            return await _cardsRepository.GetAllCardsAsync();
        }

        [HttpGet]
        public async Task<ActionResult<Card>> GetCardsAsync(int CardId)
        {
            var card = await _cardsRepository.GetCardsAsync(CardId);

            if (card != null)
            {
                return card;
            }
            else
            {
                return NotFound("Não encontrado");

            }
        }

        [HttpPost]
        public async Task<ActionResult<Card>> PostCard([FromBody] Card card)
        {
            var newCard = await _cardsRepository.CreateCard(card);
            return CreatedAtAction(nameof(GetCardsAsync), new { id = newCard.CardId }, newCard);
        }

        [HttpPut]
        public async Task<ActionResult> PutCard(int CardId, [FromBody] Card card)
        {
            if (CardId == card.CardId)
            {
                await _cardsRepository.UpdateCard(card);
                return Accepted();
            }
            else
            {
                return BadRequest("Id é diferente");
            }
        }

        [HttpDelete]
        public async Task<ActionResult> DeleteCard(int CardId)
        {
            var cardDelete = await _cardsRepository.GetCardsAsync(CardId);
            if (cardDelete != null)
            {
                await _cardsRepository.DeleteCard(cardDelete.CardId);
                return NoContent();
            }
            else
            {
                return NotFound();
            }
        }
    }
}
