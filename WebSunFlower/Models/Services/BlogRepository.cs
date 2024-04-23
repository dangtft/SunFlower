using WebSunFlower.Models.Interfaces;
using WebSunFlower.Data;
using Microsoft.EntityFrameworkCore;
namespace WebSunFlower.Models.Services
{
    public class BlogRepository : IBlogRepository
    {
        private SunFlowerDbContext dbContext;
        private ICommentRepository commentRepository;
        public BlogRepository(SunFlowerDbContext dbContext, ICommentRepository commentRepository)
        {
            this.dbContext = dbContext;
            this.commentRepository = commentRepository; 
        }
       
        public IEnumerable<Blog> GetAllBlog()
        {
            return dbContext.Blogs.Include(b => b.Comments).ToList();
        }
        public IEnumerable<Comment> GetCommentsForBlog(int bBlogId)
        {
            return dbContext.Comments.Where(c => c.BlogId == bBlogId).ToList();
        }
        public Blog? GetBlogById(int bBlogId)
        {
            return dbContext.Blogs.Include(b => b.Comments).FirstOrDefault(b => b.BlogId == bBlogId);
        }

        public IEnumerable<Blog> GetHotBlog()
        {
            return dbContext.Blogs.Where(p => p.IsHot == true);
        }
        public IEnumerable<Blog> SearchBlogsByName(string blogName)
        {
            return dbContext.Blogs.Where(b => EF.Functions.Like(b.Title, $"%{blogName}%")).ToList();
        }

        public void AddBlog(Blog blog)
        {
            blog.CreatedAt = DateTime.Now;
            dbContext.Blogs.Add(blog);
            dbContext.SaveChanges();
        }
        public void UpdateBlog(Blog blog)
        {
            var existingBlog = dbContext.Blogs.Find(blog.BlogId);
            if (existingBlog != null)
            {
                existingBlog.ImgUrl = blog.ImgUrl;
                existingBlog.Title = blog.Title;
                existingBlog.Content = blog.Content;
                existingBlog.ContentDetail = blog.ContentDetail;
                existingBlog.Author = blog.Author;
                existingBlog.CreatedAt = blog.CreatedAt;
                existingBlog.IsHot = blog.IsHot;

                dbContext.SaveChanges();
            }
        }

        public void DeleteBlog(int id)
        {
            var blog = dbContext.Blogs.Find(id);
            if (blog != null)
            {
                dbContext.Blogs.Remove(blog);
                dbContext.SaveChanges();
            }
        }
    }
}
