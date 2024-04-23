using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata;
using System.Xml.Linq;
using WebSunFlower.Data;
using WebSunFlower.Models;
using WebSunFlower.Models.Interfaces;
using WebSunFlower.ViewModel;

namespace WebSunFlower.Controllers
{
    public class ProductController : Controller
    {
        private IProductRepository productRepository;
        private IBlogRepository blogRepository;
        private ICommentRepository commentRepository;
        private IProductComment productComment;
        private SunFlowerDbContext dbContext;
        public ProductController(IProductRepository productRepository, IBlogRepository blogRepository,SunFlowerDbContext dbContext,ICommentRepository commentRepository,IProductComment productComment)
        {
            this.productRepository = productRepository;
            this.blogRepository = blogRepository;
            this.commentRepository = commentRepository;
            this.dbContext = dbContext;
            this.productComment = productComment;
        }
        public IActionResult Shop(string productName)
        {
            IEnumerable<Product> products;
            if (!string.IsNullOrEmpty(productName))
            {
                products = productRepository.SearchProductsByName(productName);
            }
            else
            {
                products = productRepository.GetAllProducts();
            }
            return View(products);
        }

        public IActionResult Detail(int id)
        {
            var product = productRepository.GetProductDetail(id);
            var comment = productRepository.GetCommentsForProduct(id);

            var model = new MyViewModels
            {
                Product = product,
                Comments = comment,
                Comment = new ProductComment { ProductId = id },
            };
            return View(model);
        }

        public IActionResult Index()
        {
            var viewModel = new MyViewModels
            {
                Blogs = blogRepository.GetAllBlog(),
                Products = productRepository.GetAllProducts()
            };
            return View(viewModel);
        }
        [HttpGet]
        public IActionResult Search(string query)
        {
            var productSearchResults = productRepository.SearchProductsByName(query);
            var blogSearchResults = blogRepository.SearchBlogsByName(query);

            var viewModel = new SearchViewModel
            {
                ProductSearchResults = productSearchResults,
                BlogSearchResults = blogSearchResults
            };

            return View("Search", viewModel);
        }
        [HttpPost] 
        public IActionResult Subscribe(EmailSubscribe emailSubscribe)
        {
            if (ModelState.IsValid)
            {
                productRepository.AddEmailSubscription(emailSubscribe);
                var viewModel = new MyViewModels
                {
                    Blogs = blogRepository.GetAllBlog(),
                    Products = productRepository.GetAllProducts(),
                    Emails = new List<EmailSubscribe> { emailSubscribe }
                };
                ViewBag.SuccessMessage = "Cảm ơn bạn đã đăng ký!";
                return View("Index", viewModel);
            }
            return View("Index", new MyViewModels
            {
                Blogs = blogRepository.GetAllBlog(),
                Products = productRepository.GetAllProducts()
            });
        }

        [HttpPost]
        public IActionResult AddCommentProducts(ProductComment comment)
        {
            if (ModelState.IsValid)
            {
                productComment.AddCommentProduct(comment, comment.ProductId);
                return RedirectToAction("Detail", new { id = comment.ProductId });
            }
            return View("Detail", comment);
        }
    }
}
