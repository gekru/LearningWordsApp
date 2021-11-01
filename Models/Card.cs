namespace LearningWordsApp.Models
{
    public class Card
    {
        public string Original { get; set; }
        public string Translated { get; set; }
        public bool Memorized { get; set; }

        public Card(string original, string translated, bool memorized)
        {
            Original = original;
            Translated = translated;
            Memorized = memorized;
        }
    }
}
