using Microsoft.AspNetCore.Mvc;
using WebSunFlower.Models;
using WebSunFlower.Models.Interfaces;
using WebSunFlower.Models.Services;
using WebSunFlower.ViewModel;

namespace WebSunFlower.Controllers
{
    public class BlogController : Controller
    {
        private IBlogRepository blogRepository;
        private ICommentRepository commentRepository;
        public BlogController(IBlogRepository blogRepository, ICommentRepository commentRepository)
        {
            this.blogRepository = blogRepository;
            this.commentRepository = commentRepository;
        }
        public IActionResult Index(string blogName)
        {
            IEnumerable<Blog> blogs;

            if (!string.IsNullOrEmpty(blogName))
            {
                blogs = blogRepository.SearchBlogsByName(blogName);
            }
            else
            {
                blogs = blogRepository.GetAllBlog();
            }
            var model = new BlogViewModel
            {
                Blogs = blogs,
            };

            return View(model);
        }
        public IActionResult Detail(int bBlogId)
        {
            var blog = blogRepository.GetBlogById(bBlogId);
            var comments = blogRepository.GetCommentsForBlog(bBlogId);
            int totalCommentsCount = commentRepository.GetTotalCommentsCountForBlog(bBlogId);

            var model = new BlogViewModel
            {
                Blog = blog,
                Comments = comments ,
                Comment = new Comment { BlogId = bBlogId },
                TotalCommentsCount = totalCommentsCount
            };
            return View(model);

        }

        [HttpPost]
        public IActionResult AddComment(Comment comment)
        {
            if (ModelState.IsValid)
            {
                commentRepository.AddComment(comment,comment.BlogId);
                return RedirectToAction("Detail", new { bBlogId = comment.BlogId });
            }
            return View("Detail", comment);
        }

        public int GetTotalCommentsCount(int bBlogId)
        {
            int totalCommentsCount = commentRepository.GetTotalCommentsCountForBlog(bBlogId);
            return totalCommentsCount;
        }
    }
}
