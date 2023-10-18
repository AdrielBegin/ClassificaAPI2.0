using Classifica3._0.Context;
using Classifica3._0.Model;
using Microsoft.EntityFrameworkCore;

namespace Classifica3._0.Repositories
{
    public class CardsRepository : ICardsRepository
    {
        private readonly AppDbContext _context;

        public CardsRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Card> CreateCard(Card card)
        {

            card.DateUpdated();
            _context.Cards.Add(card);
            await _context.SaveChangesAsync();
            return card;
        }

        public async Task DeleteCard(int CardId)
        {
            var cardDelete = await _context.Cards.FindAsync(CardId);

            if (cardDelete != null)
            {
                cardDelete.Inativo();
                _context.Cards.Update(cardDelete);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Card>> GetAllCardsAsync()
        {
            return await _context.Cards.ToListAsync();
        }

        public async Task<Card> GetCardsAsync(int CardId)
        {
            return await _context.Cards.FindAsync(CardId);
        }

        public async Task UpdateCard(Card card)
        {
            card.DateUpdated();
            _context.Entry(card).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}
