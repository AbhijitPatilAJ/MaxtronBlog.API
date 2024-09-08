using MaxtronBlog.API.Models.Domain;
using MaxtronBlog.API.Repos.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MaxtronBlog.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IImageRepo _imageRepo;

        public ImagesController(IConfiguration configuration, IImageRepo imageRepo)
        {
            _configuration = configuration;
            _imageRepo = imageRepo;
        }
        //post to upload a Image
        [HttpPost]
        [Route("uploadImage")]
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
                await _imageRepo.UploadImage(file, postImage);
            }

            return Ok(true);
        }

        private void validateFileUpload(IFormFile file)
        {
            var allowed = new string[] { ".jpg", ".jpeg", ".png" };
            if(!allowed.Contains(Path.GetExtension(file.FileName).ToLower() )) {
                ModelState.AddModelError("file", "Unsupport file format");
            }
            if(file.Length > 10485760)
            {
                ModelState.AddModelError("file", "File Size cannot be more than 10MB");
            }
        }
    }
}
