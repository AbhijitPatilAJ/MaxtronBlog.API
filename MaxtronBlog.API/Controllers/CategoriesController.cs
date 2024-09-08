using MaxtronBlog.API.Data;
using MaxtronBlog.API.Models.Domain;
using MaxtronBlog.API.Models.DTO;
using MaxtronBlog.API.Repos.Interface;
using MaxtronBlog.API.Repos.Services;
using Microsoft.AspNetCore.Mvc;

namespace MaxtronBlog.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryRepo categoryRepo;

        public CategoriesController(ICategoryRepo categoryRepo)
        {
            this.categoryRepo = categoryRepo;
        }

        public CategoryService CategoryService { get; }

        [HttpPost]
        [Route("AddCategory")]
        public async Task <IActionResult> CreateCategory([FromBody] CreateCategoryRequestDto request) 
        {
            var category = new Category
            {
                Name = request.Name,
                UrlHandle = request.UrlHandle
            };
            var response = await categoryRepo.CreateCategory(category);
            return Ok(response);
        }


        [HttpGet]
        [Route("GetAllCategories")]
        public async Task<IActionResult> GetAllCategories()
        {
            var response = await categoryRepo.GetAllCategories();
            return Ok(response);
        }

        [HttpGet]
        [Route("GetCategoryById/{Id:guid}")]
        public async Task<IActionResult> GetCategoryById([FromRoute] Guid Id)
        {
            var existingCategory = await categoryRepo.GetCategoryById(Id);
            if(existingCategory == null)
            {
                return NotFound();
            }
            var response = new CategoryDto
            {
                Id = existingCategory.Id,
                Name = existingCategory.Name,
                UrlHandle = existingCategory.UrlHandle
            };
            return Ok(response);
        }

        [HttpPut]
        [Route("UpdateCategory")]
        public async Task<IActionResult> UpdateCategory([FromBody] Category request)
        {
            var response = await categoryRepo.UpdateCategory(request);
            return Ok(response);
        }

        [HttpDelete]
        [Route("DeleteCategoryById/{Id:guid}")]
        public async Task<IActionResult> DeleteCategoryById([FromRoute] Guid Id)
        {
            var response = await categoryRepo.DeleteCategoryById(Id);
            return Ok(response);
        }
    }
}
