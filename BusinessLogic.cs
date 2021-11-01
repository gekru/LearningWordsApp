using LearningWordsApp.Helpers;
using LearningWordsApp.Models;
using LearningWordsApp.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace LearningWordsApp
{
    public class BusinessLogic
    {
        private readonly ICardService _cardService;
        private readonly IDeckService _deckService;

        public BusinessLogic(ICardService cardService, IDeckService deckService)
        {
            _cardService = cardService ?? throw new ArgumentNullException(nameof(cardService));
            _deckService = deckService ?? throw new ArgumentNullException(nameof(deckService));
        }

        public void Start()
        {
            ChooseDeck();
        }

        public void AddNewCards(string fileName)
        {
            Card card = new Card("Dog", "Собака", false);
            _cardService.AddCard(fileName, card);
        }

        public void CreateNewDeck(string fileName)
        {
            _deckService.Create(fileName);
        }

        public void ChooseDeck()
        {
            Console.WriteLine("Choose the deck by entering its number: ");
            string[] fileEntries = Directory.GetFiles(PathHelper.DbPath);
            for (int i = 0; i < fileEntries.Length; i++)
            {
                string[] name = fileEntries[i].Replace('\\', ' ').Split(' ');
                Console.WriteLine($"{i + 1} - " + name[name.Length - 1].Replace(".json", ""));
            }
            bool succeded = true;
            while (succeded)
            {
                succeded = int.TryParse(Console.ReadLine(), out int file);

                if (succeded)
                {
                    if (file < 0 || file > fileEntries.Length)
                    {
                        Console.WriteLine("Value should be not higher than file quantity.");
                        continue;
                    }
                    string[] name = fileEntries[file - 1].Replace('\\', ' ').Split(' ');
                    GetRandomCards(name[name.Length - 1]);
                    succeded = false;
                }
                else
                {
                    Console.WriteLine("Enter valid number.");
                    succeded = true;
                }
            }


        }

        public void GetRandomCards(string fileName)
        {
            List<Card> _words = _cardService.GetAllCards(fileName);

            int score = 0;
            List<Card> notLearnedWords = _words.Where(x => !x.Memorized).Select(x => x).ToList();

            for (int i = 0; i < notLearnedWords.Count; i++)
            {
                var word = notLearnedWords.ElementAt(i);
                ShowCard(word.Original);

                switch (Console.ReadKey().Key)
                {
                    case ConsoleKey.LeftArrow:
                        Console.ForegroundColor = ConsoleColor.Red;
                        ShowCard(word.Original);
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        ShowCard(word.Translated, true);
                        break;
                    case ConsoleKey.RightArrow:
                        Console.ForegroundColor = ConsoleColor.Green;
                        ShowCard(word.Original);
                        Card cardToUpdate = _words.FirstOrDefault(x => x.Original == word.Original);
                        cardToUpdate.Memorized = true;
                        _cardService.UpdateCard(fileName, cardToUpdate);
                        score++;
                        break;
                    default:
                        continue;
                }
                Console.WriteLine($"Finish: Your score {score} out of {notLearnedWords.Count}");
            }
        }

        private static void ShowCard(string word, bool wait = false)
        {
            const int cardWidth = 20;

            Console.Clear();
            Console.WriteLine("'Show: <-' \n'Know: ->'");

            Console.WriteLine(" " + new string('_', cardWidth));
            Console.WriteLine($"|{"",-cardWidth}|");
            Console.WriteLine($"|{word.PadLeft(cardWidth / 2 + word.Length / 2),-cardWidth}|");
            Console.WriteLine("|" + new string('_', cardWidth) + "|");

            System.Threading.Thread.Sleep(500);
            Console.ResetColor();
            if (wait)
            {
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
            }
        }
    }
}
