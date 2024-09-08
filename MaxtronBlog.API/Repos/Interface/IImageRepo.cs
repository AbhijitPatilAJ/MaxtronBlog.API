using MaxtronBlog.API.Models.Domain;

namespace MaxtronBlog.API.Repos.Interface
{
    public interface IImageRepo
    {
        Task<PostImage> UploadImage(IFormFile file, PostImage postImage);
    }
}
