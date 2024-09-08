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

        [HttpGet]
        [Route("GetPostById/{Id:guid}")]
        public async Task<IActionResult> GetPostById([FromRoute] Guid Id)
        {
            var existingPost = await postsRepo.GetPostById(Id);
            return Ok(existingPost);
        }

        [HttpPut]
        [Route("UpdatePost")]
        public async Task<IActionResult> UpdatePost([FromBody] AddPostDto postDetails)
        {
            var response = await postsRepo.UpdatePost(postDetails);
            return Ok(response);
        }

        [HttpDelete]
        [Route("DeletePostById/{Id:guid}")]
        public async Task<IActionResult> DeletePostById([FromRoute] Guid Id)
        {
            var response = await postsRepo.DeletePostById(Id);
            return Ok(response);
        }
    }
}
