using MaxtronBlog.API.Models.DTO;
using MaxtronBlog.API.Repos.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MaxtronBlog.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        private readonly IPostsRepo postsRepo;

        public PostsController(IPostsRepo postsRepo)
        {
            this.postsRepo = postsRepo;
        }

        [HttpPost]
        [Route("AddPost")]
        public async Task<IActionResult> AddPost([FromBody] AddPostDto postDetails)
        {
            var response = await postsRepo.AddPost(postDetails);
            return Ok(response);
        }

        [HttpGet]
        [Route("GetAllPosts")]
        public async Task<IActionResult> GetAllPost()
        {
            var response = await postsRepo.GetAllPost();
            return Ok(response);
        }
    }
}
