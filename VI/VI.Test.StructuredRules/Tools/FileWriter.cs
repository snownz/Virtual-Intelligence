using System;
using System.IO;

namespace VI.Test.StructuredRules.Tools
{
    public class FileWriter : IFileWriter
    {
        public void Write(string folder, string file, string content,
            bool overwrite = false)
        {
            if (!Directory.Exists(folder)) Directory.CreateDirectory(folder);

            var urlFile = folder + "/" + file;

            if (!File.Exists(urlFile))
            {
                File.WriteAllText(urlFile, content + Environment.NewLine);
            }
            else
            {
                if (overwrite)
                    File.WriteAllText(urlFile, content + Environment.NewLine);
                else
                    File.AppendAllText(urlFile, content + Environment.NewLine);
            }
        }
    }
}
