using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
            var filePath = Path.GetTempFileName();

            using (var stream = System.IO.File.Create(filePath))
            {
                await file.CopyToAsync(stream);
            }
            return Ok();
        }
    }
}
