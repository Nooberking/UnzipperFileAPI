using System.IO.Compression; 

namespace UnzipperFileAPI.Services
{
    public class UnzipService : IUnzipService
    {
      
        public Task UnzipFile(string zipPath)
        {
            ZipFile.ExtractToDirectory(zipPath, @"./temp", true);
            return Task.CompletedTask;
        }
    }
}
