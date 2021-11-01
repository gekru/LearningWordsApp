using LearningWordsApp.Helpers;
using System;
using System.IO;

namespace LearningWordsApp.Repositories
{
    public class DeckRepository : IDeckRepository
    {
        public DeckRepository()
        {
            DirectoryInfo dirInfo = new DirectoryInfo(PathHelper.DbPath);
            if (!dirInfo.Exists)
            {
                dirInfo.Create();
            }
        }

        public void CreateDeck(string fileName)
        {
            FileInfo fileInfo = new FileInfo($"{PathHelper.DbPath}\\{fileName}");
            if (!fileInfo.Exists)
            {
                Console.WriteLine($"{fileName} deck created.");
                fileInfo.Create();
            }
            Console.WriteLine($"{fileName} deck already exist.");
        }

        public void DeleteDeck(string fileName)
        {
            FileInfo fileInfo = new FileInfo($"{PathHelper.DbPath}\\{fileName}");
            if (fileInfo.Exists)
            {
                Console.WriteLine($"{fileName} deck deleted.");
                fileInfo.Delete();
            }
            Console.WriteLine($"There is no {fileName} deck.");
        }
    }
}
