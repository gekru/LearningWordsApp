namespace LearningWordsApp.Repositories
{
    public interface IDeckRepository
    {
        void CreateDeck(string fileName);
        void DeleteDeck(string fileName);
    }
}
