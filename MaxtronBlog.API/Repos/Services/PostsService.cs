using Azure;
using MaxtronBlog.API.Data;
using MaxtronBlog.API.Models.Domain;
using MaxtronBlog.API.Models.DTO;
using MaxtronBlog.API.Repos.Interface;
using Microsoft.EntityFrameworkCore;

namespace MaxtronBlog.API.Repos.Services
{
    public class PostsService: IPostsRepo
    {
        private readonly ApplicationDbContext DbContext;
        private readonly ICategoryRepo _category;

        public PostsService(ApplicationDbContext dbContext, ICategoryRepo category)
        {
           DbContext = dbContext;
           _category = category;
        }

        public async Task<bool> AddPost( AddPostDto postDetails)
        {
            var Post = new BlogPost
            {
                Author = postDetails?.Author,
                Content = postDetails?.Content,
                FeaturedImageUrl = postDetails?.FeaturedImageUrl,
                PublishedDate = postDetails.PublishedDate,
                ShortDescription = postDetails?.ShortDescription,
                IsVisible = postDetails.IsVisible,
                Title = postDetails?.Title,
                UrlHandle = postDetails?.UrlHandle,
                Categories = new List<Category>()
            };
            foreach(var cat in postDetails.Categories)
            {
                var existingCat = await _category.GetCategoryById(cat);
                if (existingCat != null)
                {
                    Post.Categories.Add(existingCat);
                }
            }
            await DbContext.BlogPosts.AddAsync(Post);
            await DbContext.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<ViewPostsDTO>> GetAllPost()
        {
            var posts = await DbContext.BlogPosts.Include(x=>x.Categories).ToListAsync();
            var viewPost = new List<ViewPostsDTO>();
            posts.ForEach(post =>
            {
                viewPost.Add(
                    new ViewPostsDTO
                    {
                        Title = post.Title,
                        UrlHandle = post.UrlHandle,
                        Content = post.Content,
                        Author = post.Author,
                        FeaturedImageUrl = post.FeaturedImageUrl,
                        Id = post.Id,
                        IsVisible = post.IsVisible,
                        PublishedDate = post.PublishedDate,
                        ShortDescription = post.ShortDescription,
                        Categories = post.Categories != null ? post.Categories.Select(x => new CategoryDto
                        {
                            Id = x.Id,
                            Name = x.Name,
                            UrlHandle = x.UrlHandle,
                        }).ToList() : [],
                    }) ;
            });
            return viewPost.ToList();
        }
    }
}
