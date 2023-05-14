namespace UnzipperFileAPI.Services
{
    public interface IUnzipService
    {
        Task SaveZipFile(string fileName, IFormFile file);
        Task UnzipFile(string path);

        string ExtractRandomizedName(string tempFileName);

    }
}
