using LearningWordsApp.Models;
using LearningWordsApp.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LearningWordsApp.Services
{
    public class CardService : ICardService
    {
        private readonly ICardRepository _cardRepository;

        public CardService(ICardRepository cardRepository)
        {
            _cardRepository = cardRepository ?? throw new ArgumentNullException(nameof(cardRepository));
        }

        public List<Card> GetAllCards(string fileName)
        {
            return _cardRepository.GetAllCards(fileName);
        }

        public string AddCard(string fileName, Card card)
        {
            List<Card> cards = _cardRepository.GetAllCards(fileName);

            string messasge;
            if (cards.Any(x => x.Original == card.Original))
            {
                messasge = "This card already in current deck.";
                Console.WriteLine(messasge);
                return messasge;
            }
            cards.Add(card);

            _cardRepository.AddOrUpdateCards(fileName, cards);
            messasge = "Card added successfully.";
            return messasge;
        }

        public void UpdateCard(string fileName, Card updatedCard)
        {
            List<Card> cards = _cardRepository.GetAllCards(fileName);
            Card cardToUpdate = cards.First(x => x.Original == updatedCard.Original);
            cardToUpdate.Translated = updatedCard.Translated;
            cardToUpdate.Memorized = updatedCard.Memorized;

            _cardRepository.AddOrUpdateCards(fileName, cards);
        }
    }
}
