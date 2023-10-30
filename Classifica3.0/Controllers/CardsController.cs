using Classifica3._0.Context;
using Classifica3._0.Model;
using Classifica3._0.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace Classifica3._0.Controllers
{
    [Route("api/[controller]")]
    [Consumes("application/json")]
    [Produces("application/json")]
    [ApiController]

    [Authorize]
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

        [HttpGet("Get")]
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

        [HttpPost("Create")]
        public async Task<ActionResult<Card>> PostCard([FromBody] Card card)
        {
            var newCard = await _cardsRepository.CreateCard(card);
            try
            {
                CreatedAtAction(nameof(GetCardsAsync), new { id = newCard.CardId }, newCard);

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Deu ruim: {ex}");
            }
            return newCard;
        }

        [HttpPut("Update")]
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

        [HttpDelete("Delete")]
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
