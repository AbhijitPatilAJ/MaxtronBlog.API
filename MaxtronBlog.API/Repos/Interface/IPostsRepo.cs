using MaxtronBlog.API.Models.Domain;
using MaxtronBlog.API.Models.DTO;

namespace MaxtronBlog.API.Repos.Interface
{
    public interface IPostsRepo
    {
        Task<bool> AddPost(AddPostDto postDetails);
        Task<IEnumerable<ViewPostsDTO>> GetAllPost();
        Task<ViewPostsDTO> GetPostById(Guid id);
        Task<bool> UpdatePost(AddPostDto postDetails);
        Task<bool> DeletePostById(Guid id);
    }
}
