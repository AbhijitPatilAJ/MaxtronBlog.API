using MaxtronBlog.API.Models.Domain;
using MaxtronBlog.API.Models.DTO;

namespace MaxtronBlog.API.Repos.Interface
{
    public interface IPostsRepo
    {
        Task<bool> AddPost(AddPostDto postDetails);
        Task<IEnumerable<ViewPostsDTO>> GetAllPost();
    }
}
