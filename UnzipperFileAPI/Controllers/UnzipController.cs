using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UnzipperFileAPI.Services;

namespace UnzipperFileAPI.Controllers
{
    [Route("UnzipperApi/[controller]")]
    [ApiController]
    public class UnzipController : ControllerBase
    {
        private readonly ILogger<UnzipController> _logger;

        private readonly IUnzipService _unzipService; 

        private readonly string _destinationFolder = "/media/source/";

        public UnzipController(ILogger<UnzipController> logger, IUnzipService unzipService)
        {
            _logger = logger;
            _unzipService = unzipService;
        }

        [HttpPost]
        public async Task<IActionResult> SaveZipFile(IFormFile file)
        {
       
            string fileName = HttpContext.Request.Headers.ContainsKey("FileTitle") ? HttpContext.Request.Headers["FileTitle"] : GetRandomizedName(Path.GetTempFileName());

            string destFile = GetDestinationFilePath(fileName);

            using (var stream = System.IO.File.Create(destFile))
            {
                Console.WriteLine(destFile);
                await file.CopyToAsync(stream);
            }
            return Ok();
           
            
        }
        private string GetRandomizedName(string tempFileName)
        {
            return tempFileName.Split("/tmp/")[1].Replace(".tmp", "");
        }

        private string GetDestinationFilePath (string fileName)
        {
            return $"{_destinationFolder}{fileName}.zip";
        }

        [HttpGet]
        public async Task<IActionResult> UnzipFile(string destFile)
        {
            await _unzipService.UnzipFile(destFile);
            return Ok();
        }
    }
}
