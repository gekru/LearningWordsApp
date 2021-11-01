using LearningWordsApp.Repositories;
using System;

namespace LearningWordsApp.Services
{
    public class DeckService : IDeckService
    {
        private readonly IDeckRepository _deckRepository;

        public DeckService(IDeckRepository deckRepository)
        {
            _deckRepository = deckRepository ?? throw new ArgumentNullException(nameof(deckRepository));
        }

        public void Create(string fileName)
        {
            _deckRepository.CreateDeck(fileName);
        }

        public void Delete(string fileName)
        {
            _deckRepository.DeleteDeck(fileName);
        }
    }
}
