using System.IO;
using System.Text;

namespace FileWriter
{
    public class FileWriter : IFileWriter
    {
        public void WriteToFile(string filename, string fileContent)
        {
            using (var fs = File.Create(Path.Combine(Directory.GetCurrentDirectory(), filename)))
            {
                var info = new UTF8Encoding(true).GetBytes(fileContent);

                fs.Write(info, 0, info.Length);
            }
        }
    }
}
