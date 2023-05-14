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

        public UnzipController(ILogger<UnzipController> logger, IUnzipService unzipService)
        {
            _logger = logger;
            _unzipService = unzipService;
        }

        [HttpPost]
        public async Task<IActionResult> SaveZipFile(IFormFile file)
        {
            string fileName = HttpContext.Request.Headers.ContainsKey("FileTitle") ? 
                HttpContext.Request.Headers["FileTitle"] : 
                _unzipService.ExtractRandomizedName(Path.GetTempFileName());
            await _unzipService.SaveZipFile(fileName, file); 
            return Ok();
                       
        }

        [HttpGet]
        public async Task<IActionResult> UnzipFile(string destFile)
        {
            await _unzipService.UnzipFile(destFile);
            return Ok();
        }
    }
}
