using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace UnzipperFileAPI.Controllers
{
    [Route("UnzipperApi/[controller]")]
    [ApiController]
    public class UnzipController : ControllerBase
    {
        private readonly ILogger<UnzipController> _logger;

        private readonly string _destinationFolder = "/media/source/";

        public UnzipController(ILogger<UnzipController> logger)
        {
            _logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> SaveZipFile(IFormFile file)
        {
            try
            {
                string fileName = HttpContext.Request.Headers.ContainsKey("FileTitle") ? HttpContext.Request.Headers["FileTitle"] : global::UnzipperFileAPI.Controllers.UnzipController.GetRandomizedName();

                string destFile = GetDestinationFilePath(fileName);

                using (var stream = System.IO.File.Create(destFile))
                {
                    await file.CopyToAsync(stream);
                }
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
            
        }

        private static string GetRandomizedName()
        {
            return Path.GetTempFileName().Split("/tmp/")[1].Replace(".tmp", "");
        }

        private string GetDestinationFilePath (string fileName)
        {
            return $"{_destinationFolder}{fileName}.zip";
        }
    }
}
