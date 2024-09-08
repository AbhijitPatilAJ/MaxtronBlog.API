using MaxtronBlog.API.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace MaxtronBlog.API.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<BlogPost> BlogPosts { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<PostImage> PostImages { get; set; }
    }
}
