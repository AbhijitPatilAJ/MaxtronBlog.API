using MaxtronBlog.API.Models.Domain;
using MaxtronBlog.API.Repos.Interface;

namespace MaxtronBlog.API.Repos.Services
{
    public class ImageService : IImageRepo
    {
        public async Task<PostImage> UploadImage(IFormFile file, PostImage postImage){

            //upload the tmage to API folder

            //update the database
            var res =  new PostImage() { };
            return res;
        }
    }
}
