namespace MaxtronBlog.API.Models.Domain
{
    public class Category
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string UrlHandle { get; set; }
        public DateTime DateCreated() { return DateTime.Now; }
        public DateTime DateModified() { return DateTime.Now; }
        public ICollection<BlogPost> BlogPosts { get; set; }
    }
}
