using System;
using System.IO;
using System.IO.Compression;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoadImdbToMongoDB
{
    class Program
    {
        static void Main(string[] args)
        {

            FileInfo[] files;

            DirectoryInfo directory = new DirectoryInfo(@"C:\Users\Ryan-P\Source\Repos\mongomovie2\imdb");
            files = directory.GetFiles();

            foreach (var file in files)
            {
                FileInfo fileToDecompress = file;

                using (FileStream originalFileStream = fileToDecompress.OpenRead())
                {
                    string currentFileName = fileToDecompress.FullName;
                    string newFileName = currentFileName.Remove(currentFileName.Length - fileToDecompress.Extension.Length);

                    using (FileStream decompressedFileStream = File.Create(newFileName))
                    {

                        using (GZipStream decompressionStream = new GZipStream(originalFileStream, CompressionMode.Decompress))
                        {
                            decompressionStream.CopyTo(decompressedFileStream);
                            Console.WriteLine("Decompressed: {0}", fileToDecompress);
                        }

                    }
                }
                
            }

            Console.ReadLine();

        }
    }
}
