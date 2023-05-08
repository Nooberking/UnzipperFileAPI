using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UnzipperFileAPI.Models;

namespace UnzipperFileAPI.Controllers
{
    [Route("UnzipperApi/[controller]")]
    [ApiController]
    public class UnzipController : ControllerBase
    {
        private readonly ILogger<UnzipController> _logger;

        public UnzipController(ILogger<UnzipController> logger)
        {
            _logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> SaveZipFile(IFormFile file)
        {

            string randomizedName = Path.GetTempFileName().Split("/tmp/")[1].Replace(".tmp", "");

            string fileTitle = HttpContext.Request.Headers.ContainsKey("FileTitle") ? HttpContext.Request.Headers["FileTitle"] : randomizedName;

            string destFile = $"/media/source/{fileTitle}.zip";

            using (var stream = System.IO.File.Create(destFile))
            {
                await file.CopyToAsync(stream);
            }
            return Ok();
        }
    }
}
