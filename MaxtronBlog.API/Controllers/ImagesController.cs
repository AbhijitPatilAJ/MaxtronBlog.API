using MaxtronBlog.API.Models.Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MaxtronBlog.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public ImagesController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        //post to upload a Image
        [HttpPost]
        [Route("upload")]
        public async Task<IActionResult> UploadImage([FromForm] IFormFile file, [FromForm] string fileName , [FromForm] string title)
        {
            validateFileUpload(file);
            if(ModelState.IsValid)
            {
                var postImage = new PostImage { 
                    Title = title,
                    FileExtension = Path.GetExtension(file.FileName).ToLower(),
                    FileName = fileName,
                    DateCreated = DateTime.Now,
                };
            }

            return Ok();
        }

        private void validateFileUpload(IFormFile file)
        {
            var allowed = new string[] { ".jpg", ".jpeg", ".png" };
            if(allowed.Contains(Path.GetExtension(file.FileName).ToLower() )) {
                ModelState.AddModelError("file", "Unsupport file format");
            }
            if(file.Length > 10485760)
            {
                ModelState.AddModelError("file", "File Size cannot be more than 10MB");
            }
            var allowedExtensions = _configuration.GetValue<Array>("allowedExtensions").ToString();
        }
    }
}
