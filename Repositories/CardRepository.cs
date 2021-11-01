using LearningWordsApp.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;

namespace LearningWordsApp.Repositories
{
    public class CardRepository : ICardRepository
    {
        private static readonly string _projectFolderPath = Path.GetFullPath(@"..\..\..");
        private static readonly string _dbPath = Path.Combine(_projectFolderPath, "MockDb");

        public CardRepository()
        {
            DirectoryInfo dirInfo = new DirectoryInfo(_dbPath);
            if (!dirInfo.Exists)
            {
                dirInfo.Create();
            }
        }

        public List<Card> GetAllCards(string fileName)
        {
            string data = null;
            using (StreamReader sr = new StreamReader($"{_dbPath}\\{fileName}"))
            {
                data = sr.ReadToEnd();
            }

            List<Card> cards = new List<Card>();
            if (string.IsNullOrWhiteSpace(data))
            {
                return cards;
            }

            try
            {
                cards = JsonSerializer.Deserialize<List<Card>>(data);
            }
            catch (Exception)
            {
                Console.WriteLine("Check deck file.");
            }
            return cards;
        }

        public void AddOrUpdateCards(string fileName, List<Card> cards)
        {
            string data = JsonSerializer.Serialize(cards, new JsonSerializerOptions { WriteIndented = true, Encoder = JavaScriptEncoder.Create(UnicodeRanges.All) });
            using (StreamWriter sw = new StreamWriter($"{_dbPath}\\{fileName}"))
            {
                sw.WriteLine(data);
            }
        }
    }
}
