using System;

namespace assecor_assessment_backend.Services.Path
{
    public class PathService : IPathService
    {
        public string Combine(string path1, string path2)
        {
            if (string.IsNullOrEmpty(path1))
                return path2;

            if (string.IsNullOrEmpty(path2))
                return path1;

            return System.IO.Path.Combine(path1, path2);
        }

        public string GetSolutionDir()
        {
            return Environment.CurrentDirectory;
        }
    }
}