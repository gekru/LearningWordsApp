using LearningWordsApp.Models;
using System.Collections.Generic;

namespace LearningWordsApp.Services
{
    public interface ICardService
    {
        List<Card> GetAllCards(string fileName);
        string AddCard(string fileName, Card card);
        void UpdateCard(string fileName, Card updatedCard);
    }
}
