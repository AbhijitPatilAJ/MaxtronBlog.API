using MaxtronBlog.API.Models.Domain;
using MaxtronBlog.API.Models.DTO;

namespace MaxtronBlog.API.Repos.Interface
{
    public interface ICategoryRepo
    {
        Task<Category> CreateCategory(Category category);
        Task<IEnumerable<CategoryDto>> GetAllCategories();
        Task<Category?> GetCategoryById(Guid id);
        Task<bool> UpdateCategory(Category category);

        Task<bool> DeleteCategoryById(Guid id);
    }
}
