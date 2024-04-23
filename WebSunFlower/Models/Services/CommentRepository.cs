using WebSunFlower.Models.Interfaces;
using WebSunFlower.Data;
using Microsoft.EntityFrameworkCore;
using WebSunFlower.ViewModel;
namespace WebSunFlower.Models.Services
{
    public class CommentRepository : ICommentRepository
    {
        private readonly SunFlowerDbContext dbContext;

        public CommentRepository(SunFlowerDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public Comment? GetCommentById(int commentId)
        {
            return dbContext.Comments.FirstOrDefault(c => c.CommentId == commentId);
        }

        public IEnumerable<Comment> GetCommentsByBlogId(int blogId)
        {
            return dbContext.Comments.Where(c => c.BlogId == blogId).ToList();
        }

        public void AddComment(Comment comment, int blogId)
        {
            Comment newComment = new Comment
            {
                UserName = comment.UserName,
                Text = comment.Text,
                Email = comment.Email,
                CreatedAt = DateTime.UtcNow,
                BlogId = blogId,
            };

            dbContext.Comments.Add(newComment);
            dbContext.SaveChanges();
        }
        

        public int GetTotalCommentsCountForBlog(int bBlogId)
        {
            int totalCommentsCount = dbContext.Comments.Count(c => c.BlogId == bBlogId);
            return totalCommentsCount;
        }
    }
}
