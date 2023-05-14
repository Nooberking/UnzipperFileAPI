using System.IO.Compression; 

namespace UnzipperFileAPI.Services
{
    public class UnzipService : IUnzipService
    {
        private readonly string _destinationFolder = "/media/source/";
      
       

        public async Task SaveZipFile(string fileName, IFormFile file)
        {
            string destFile = GetDestinationFilePath(fileName);

            using (var stream = System.IO.File.Create(destFile))
            {
                Console.WriteLine(destFile);
                await file.CopyToAsync(stream);
            }
        }
        public string ExtractRandomizedName(string tempFileName)
        {
            return tempFileName.Split("/tmp/")[1].Replace(".tmp", "");
        }

        private string GetDestinationFilePath(string fileName)
        {
            return $"{_destinationFolder}{fileName}.zip";
        }

        public Task UnzipFile(string zipPath)
        {
            ZipFile.ExtractToDirectory(zipPath, @"./temp", true);
            return Task.CompletedTask;
        }

    }
}
