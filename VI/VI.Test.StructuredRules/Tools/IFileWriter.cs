namespace VI.Test.StructuredRules.Tools
{
    public interface IFileWriter
    {
        void Write(string folder, string file, string content, bool overwrite = false);
    }
}
