using System;
using System.IO;
using System.IO.Compression;
using System.Collections.Generic;
using System.Linq;
using MongoDB.Driver;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;

namespace LoadImdbToMongoDB
{
    class Program
    {
        //private string mongoConnection = "localhost:27017";

        private List<string> filesList;
        private string path = @"C:\Users\Ryan-P\Source\Repos\mongomovie2\imdb";

        static void Main(string[] args)
        {
            
            Program program = new Program();

            if (program.Decompress())
            {
                //program.ImportToMongo();
            }
            else
            {
 
            }

            Console.ReadLine();

        }

        private bool Decompress()
        {
            FileInfo[] files;

            DirectoryInfo directory = new DirectoryInfo(path);
            files = directory.GetFiles();
            var fileArr = files.ToArray();
            fileArr.OrderBy(f => f); //sorted alphabetically

            try
            {
                foreach (var file in fileArr)
                {
                    FileInfo fileToDecompress = file;

                    long size = fileToDecompress.Length;

                    using (FileStream originalFileStream = fileToDecompress.OpenRead())
                    {
                        string currentFileName = fileToDecompress.FullName;
                        string newFileName = currentFileName.Remove(currentFileName.Length - fileToDecompress.Extension.Length);

                        Console.WriteLine("File " + currentFileName + " is " + size + " before decompression.");

                        using (FileStream decompressedFileStream = File.Create(newFileName))
                        {
                            using (GZipStream decompressionStream = new GZipStream(originalFileStream, CompressionMode.Decompress))
                            {
                                decompressionStream.CopyTo(decompressedFileStream);
                                long newSize = newFileName.Length;
                                Console.WriteLine("Decompressed: {0}", fileToDecompress);
                                Console.WriteLine("Decompressed file size is " + newSize);

                                filesList.Add(newFileName);
                            }

                        }

                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return true;
        }

        private void ImportToMongo()
        {
            MongoClient client = new MongoClient();

            foreach (string origFileName in filesList)
            {
                FileInfo origFileInfo = new FileInfo(path + origFileName);

                using (FileStream origFileStream = origFileInfo.OpenRead())
                {
                    string currentFileName = origFileInfo.FullName;

                    IMongoDatabase database = client.GetDatabase("Imdb");
                    IMongoCollection<BsonDocument> collection = database.GetCollection<BsonDocument>(""); //TODO: need to change document collection using origfilename

                }
            }
        }
    }
}
