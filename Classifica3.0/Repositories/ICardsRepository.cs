using Classifica3._0.Model;

namespace Classifica3._0.Repositories
{
    public interface ICardsRepository
    {
        Task<IEnumerable<Card>> GetAllCardsAsync();
        Task<Card> GetCardsAsync(int CardId);
        Task<Card> CreateCard(Card card);
        Task UpdateCard(Card card);
        Task DeleteCard(int CardId);
    }
}
