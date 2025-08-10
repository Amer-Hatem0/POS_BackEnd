using BRIXEL_core.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BRIXEL.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MediaController : ControllerBase
    {
        private readonly IImageService _images;
        public MediaController(IImageService images) => _images = images;

        // مثال: رفع صورة واحدة
        [HttpPost("upload")]
        [RequestSizeLimit(50_000_000)]
        public async Task<IActionResult> Upload([FromForm] IFormFile file, [FromForm] string? folder = null)
        {
            var (url, publicId) = await _images.UploadAsync(file, folder);
            return Ok(new { url, publicId });
        }

        // مثال: رفع عدّة صور
        [HttpPost("upload-many")]
        [RequestSizeLimit(50_000_000)]
        public async Task<IActionResult> UploadMany([FromForm] List<IFormFile> files, [FromForm] string? folder = null)
        {
            var list = new List<object>(files.Count);
            foreach (var f in files)
            {
                var (url, pid) = await _images.UploadAsync(f, folder);
                list.Add(new { url, publicId = pid });
            }
            return Ok(list);
        }

        // حذف صورة عبر publicId
        [HttpDelete("{publicId}")]
        public async Task<IActionResult> Delete(string publicId)
        {
            await _images.DeleteAsync(publicId);
            return NoContent();
        }
    }
}
