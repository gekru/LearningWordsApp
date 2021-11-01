using System.IO;

namespace LearningWordsApp.Helpers
{
    public static class PathHelper
    {
        public static readonly string ProjectFolderPath = Path.GetFullPath(@"..\..\..");
        public static readonly string DbPath = Path.Combine(ProjectFolderPath, "MockDb");
    }
}
