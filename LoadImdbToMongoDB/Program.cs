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

                long size = fileToDecompress.Length;

                using (FileStream originalFileStream = fileToDecompress.OpenRead())
                {
                    string currentFileName = fileToDecompress.FullName;
                    string newFileName = currentFileName.Remove(currentFileName.Length - fileToDecompress.Extension.Length);

                    Console.WriteLine("File "+ currentFileName + " is " + size + " before decompression.");

                    using (FileStream decompressedFileStream = File.Create(newFileName))
                    {
                        using (GZipStream decompressionStream = new GZipStream(originalFileStream, CompressionMode.Decompress))
                        {
                            decompressionStream.CopyTo(decompressedFileStream);
                            long newSize = newFileName.Length;
                            Console.WriteLine("Decompressed: {0}", fileToDecompress);
                            Console.WriteLine("Decompressed file size is " + newSize);
                        }

                    }
                }
                
            }

            Console.ReadLine();

        }
    }
}
