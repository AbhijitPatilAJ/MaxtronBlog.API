using Azure;
using MaxtronBlog.API.Data;
using MaxtronBlog.API.Models.Domain;
using MaxtronBlog.API.Models.DTO;
using MaxtronBlog.API.Repos.Interface;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;

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

        public async Task<ViewPostsDTO?> GetPostById(Guid Id)
        {
            var response = await DbContext.BlogPosts.Include(x => x.Categories).FirstOrDefaultAsync(c => c.Id.Equals(Id));

            var viewPost = new ViewPostsDTO
            {
                Title = response.Title,
                UrlHandle = response.UrlHandle,
                Content = response.Content,
                Author = response.Author,
                FeaturedImageUrl = response.FeaturedImageUrl,
                Id = response.Id,
                IsVisible = response.IsVisible,
                PublishedDate = response.PublishedDate,
                ShortDescription = response.ShortDescription,
                Categories = response.Categories != null ? response.Categories.Select(x => new CategoryDto
                {
                    Id = x.Id,
                    Name = x.Name,
                    UrlHandle = x.UrlHandle,
                }).ToList() : [],
            };
            
            return viewPost;
        }

        public async Task<bool> UpdatePost(AddPostDto postDetails)
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
                Categories = new List<Category>(),
                Id = postDetails.Id
            };
            foreach (var pos in postDetails.Categories)
            {
                var existingpos = await _category.GetCategoryById(pos);
                if (existingpos != null)
                {
                    Post.Categories.Add(existingpos);
                }
            }
            var existingPost = await DbContext.BlogPosts.Include(x=>x.Categories).FirstOrDefaultAsync(c => c.Id == postDetails.Id);
            if (existingPost != null)
            {
                DbContext.Entry(existingPost).CurrentValues.SetValues(Post);
                existingPost.Categories = Post.Categories;
                await DbContext.SaveChangesAsync();
                return true;
            }
            return false;
        }


        public async Task<bool> DeletePostById(Guid Id)
        {
            var response = await DbContext.BlogPosts.FirstOrDefaultAsync(c => c.Id.Equals(Id));
            if (response != null)
            {
                DbContext.BlogPosts.Remove(response);
                await DbContext.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }
}
