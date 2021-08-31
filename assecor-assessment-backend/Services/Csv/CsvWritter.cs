using System;
using System.IO;

namespace assecor_assessment_backend.Services.Csv
{
    public class CsvWritter : IDisposable
    {
        private readonly string _filePath;
        public CsvWritter(string filePath)
        {
            _filePath = filePath;
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        public bool Writte(string newLine)
        {
            if (string.IsNullOrEmpty(newLine))
                return false;

            File.AppendAllText(_filePath, newLine);
            return true;
        }
    }
}