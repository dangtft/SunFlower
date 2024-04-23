using WebSunFlower.ViewModel;

namespace WebSunFlower.Models.Interfaces
{
    public interface ICommentRepository
    {
        Comment GetCommentById(int commentId);
        IEnumerable<Comment> GetCommentsByBlogId(int blogId);
        void AddComment(Comment comment,int blogId);
       
        int GetTotalCommentsCountForBlog(int blogId);
    }
}
