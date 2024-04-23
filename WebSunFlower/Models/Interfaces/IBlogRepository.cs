namespace WebSunFlower.Models.Interfaces
{
    public interface IBlogRepository
    {
        IEnumerable<Blog> GetAllBlog();
        IEnumerable<Blog> GetHotBlog();
        Blog GetBlogById(int bBlogId);
        void AddBlog(Blog blog);
        void UpdateBlog(Blog blog); 
        void DeleteBlog(int bBlogId);
        IEnumerable<Comment> GetCommentsForBlog(int bBlogId);
        IEnumerable<Blog> SearchBlogsByName(string blogName);
    }
}
