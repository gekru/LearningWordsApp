using LearningWordsApp.Models;
using System.Collections.Generic;

namespace LearningWordsApp.Repositories
{
    public interface ICardRepository
    {
        List<Card> GetAllCards(string fileName);
        void AddOrUpdateCards(string fileName, List<Card> cards);
    }
}
