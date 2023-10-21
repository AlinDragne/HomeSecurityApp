using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Threading.Tasks;

namespace HomeSecurityApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : ControllerBase
    {
        [HttpGet("{filename}")]
        public async Task<IActionResult> GetImage(string filename)
        {
            var imagePath = Path.Combine("D:\\VisualStudio\\HomeSecurityApp\\Data\\Frames", filename);

            if (!System.IO.File.Exists(imagePath))
            {
                return NotFound();
            }

            var imageBytes = await System.IO.File.ReadAllBytesAsync(imagePath);
            return File(imageBytes, "image/jpeg");
        }
    }
}
