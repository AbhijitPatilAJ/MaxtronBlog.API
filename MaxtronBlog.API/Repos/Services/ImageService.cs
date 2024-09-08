using Azure.Core;
using MaxtronBlog.API.Data;
using MaxtronBlog.API.Models.Domain;
using MaxtronBlog.API.Repos.Interface;

namespace MaxtronBlog.API.Repos.Services
{
    public class ImageService : IImageRepo
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ApplicationDbContext _applicationDbContext;

        public ImageService(IWebHostEnvironment webHostEnvironment, IHttpContextAccessor httpContextAccessor, ApplicationDbContext applicationDbContext )
        {
            _webHostEnvironment = webHostEnvironment;
            _httpContextAccessor = httpContextAccessor;
            _applicationDbContext = applicationDbContext;
        }
        public async Task<PostImage> UploadImage(IFormFile file, PostImage postImage){

            //upload the tmage to API folder
            var localPath = Path.Combine(_webHostEnvironment.ContentRootPath, "Images", $"{postImage.FileName}{postImage.FileExtension}");
            var stream = new FileStream(localPath, FileMode.Create);
            await file.CopyToAsync(stream);

            var request = _httpContextAccessor.HttpContext.Request;
            //update the database to file
            var urlPath = $"{request.Scheme}://{request.Host}{request.PathBase}/Images/{postImage.FileName}{postImage.FileExtension}";
            postImage.Url = urlPath;
            await _applicationDbContext.PostImages.AddAsync(postImage);
            await _applicationDbContext.SaveChangesAsync();
            
           return postImage;
        }
    }
}
