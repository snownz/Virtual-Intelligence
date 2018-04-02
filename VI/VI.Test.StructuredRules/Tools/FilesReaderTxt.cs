using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace VI.Test.StructuredRules.Tools
{
    public class FilesReaderTxt
    {
        private readonly string TXT_EXTENSION = "*.txt";

        public IList<string> ReadFilesFromFolder(string urlFolder)
        {
            var filesFromFolder = Directory.EnumerateFiles(urlFolder, TXT_EXTENSION);
            return filesFromFolder.ToList();
        }

        public string ReadFile(string urlFile)
        {
            return File.ReadAllText(urlFile);
        }
    }
}
