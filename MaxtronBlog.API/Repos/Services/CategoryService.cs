using MaxtronBlog.API.Data;
using MaxtronBlog.API.Models.Domain;
using MaxtronBlog.API.Models.DTO;
using MaxtronBlog.API.Repos.Interface;
using Microsoft.EntityFrameworkCore;


namespace MaxtronBlog.API.Repos.Services
{
    public class CategoryService: ICategoryRepo
    {
        private readonly ApplicationDbContext DbContext;
        public CategoryService(ApplicationDbContext dbContext)
        {
            DbContext = dbContext;
        }

        public async Task<Category> CreateCategory(Category category) {

            var cat = new Category
            {
                Name = category.Name,
                UrlHandle = category.UrlHandle
            };
            await DbContext.Categories.AddAsync(cat);
            await DbContext.SaveChangesAsync();

            return cat;
        }

        public async Task<IEnumerable<CategoryDto>> GetAllCategories()
        {
           var response =  await DbContext.Categories.ToListAsync();
            var categories = new List<CategoryDto>();
            response.ForEach(response => { 
                categories.Add(new CategoryDto {Id= response.Id, Name= response.Name, UrlHandle= response.UrlHandle });
                });
            return categories;

        }

        public async Task<Category?> GetCategoryById(Guid Id)
        {
            var response = await DbContext.Categories.FirstOrDefaultAsync(c => c.Id.Equals(Id));
            return response;
        }

        public async Task<bool> UpdateCategory(Category category)
        {
            var existingCategory = await DbContext.Categories.FirstOrDefaultAsync(c=>c.Id == category.Id);
            if(existingCategory != null)
            {
                DbContext.Entry(existingCategory).CurrentValues.SetValues(category);
                await DbContext.SaveChangesAsync();
                return true;
            }
            return false;

        }

        public async Task<bool> DeleteCategoryById(Guid Id)
        {
            var response = await DbContext.Categories.FirstOrDefaultAsync(c => c.Id.Equals(Id));
            if(response != null)
            {
                DbContext.Categories.Remove(response);
                await DbContext.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }
}
