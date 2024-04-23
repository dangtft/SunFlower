using Microsoft.AspNetCore.Mvc;
using WebSunFlower.Models.Interfaces;
using WebSunFlower.Models;
using WebSunFlower.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WebSunFlower.Models.Services;
using System.Security.Claims;

namespace WebSunFlower.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {

        private readonly UserManager<IdentityUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private IProductRepository productRepository;
        private IBlogRepository blogRepository;
        private IContactRepository contactRepository;
        private IOrderRepository orderRepository;
        public AdminController(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager, IProductRepository productRepository, IBlogRepository blogRepository, IContactRepository contactRepository, IOrderRepository orderRepository, SignInManager<IdentityUser> signInManager)
        {
            this.productRepository = productRepository;
            this.blogRepository = blogRepository;
            this.contactRepository = contactRepository;
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.orderRepository = orderRepository;
            _signInManager = signInManager;
        }
        public async Task<IActionResult> AllUsersAndRoles()
        {
            var usersWithRoles = await userManager.Users
                .ToListAsync();

            var result = new List<UserWithRolesVM>();

            foreach (var user in usersWithRoles)
            {
                var roles = await userManager.GetRolesAsync(user);

                result.Add(new UserWithRolesVM
                {
                    UserId = user.Id,
                    UserName = user.UserName,
                    Email = user.Email,
                    Roles = roles.ToList()
                });
            }

            return View(result);
        }
        public async Task<IActionResult> EditUserRoles(string userId)
        {
            var user = await userManager.FindByIdAsync(userId);

            if (user == null)
            {
                return NotFound();
            }

            var allRoles = roleManager.Roles.ToList();
            var userRoles = await userManager.GetRolesAsync(user);

            var model = new EditUserRoleVM
            {
                UserId = userId,
                UserName = user.UserName,
                AllRoles = allRoles,
                UserRoles = userRoles
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> EditUserRoles(EditUserRoleVM model)
        {
            try
            {
                var user = await userManager.FindByIdAsync(model.UserId);

                if (user == null)
                {
                    return NotFound();
                }

                // Lấy vai trò hiện tại của người dùng
                var userRoles = await userManager.GetRolesAsync(user);

                // Nếu không có quyền nào được chọn, thêm vai trò "User"
                if (model.UserRoles == null || !model.UserRoles.Any())
                {
                    model.UserRoles = new List<string> { "User" };
                }

                // Tìm vai trò cần loại bỏ
                var rolesToRemove = userRoles.Except(model.UserRoles);

                // Tìm vai trò cần thêm
                var rolesToAdd = model.UserRoles.Except(userRoles);

                // Loại bỏ vai trò không còn sử dụng
                await userManager.RemoveFromRolesAsync(user, rolesToRemove);

                // Thêm vai trò mới cho người dùng
                await userManager.AddToRolesAsync(user, rolesToAdd);

                return RedirectToAction("AllUsersAndRoles");
            }
            catch (Exception ex)
            {
                return View("AllUsersAndRoles");
            }
        }

        public async Task<IActionResult> DeleteUser(string userId)
        {
            var user = await userManager.FindByIdAsync(userId);

            if (user != null)
            {
                var result = await userManager.DeleteAsync(user);

                if (result.Succeeded)
                {
                    return RedirectToAction("AllUsersAndRoles");
                }
                else
                {
                    
                }
            }

            return NotFound();
        }

        [HttpGet]
        public async Task<IActionResult> UserDetails(string userId)
        {
            var user = await userManager.FindByIdAsync(userId);

            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }


        public IActionResult Index()
        {
            return View();
        }
        // Action để hiển thị danh sách sản phẩm 
        public IActionResult AdminDashboard()
        {
            var viewModel = new MyViewModels
            {
                Products = productRepository.GetAllProducts(),
                Product = new Product()
            };

            return View(viewModel);
        }
        public IActionResult AllContact()
        {
            var viewModel = new ContactVM
            {
                Contacts = contactRepository.GetAllContacts(),
            };

            return View(viewModel);
        }
        public IActionResult AllSubscriber()
        {
            var viewModel = new ContactVM
            {
                subscribes = contactRepository.GetAllSubscriber(),
            };

            return View(viewModel);
        }


        // Action để hiển thị danh sách blog

        public IActionResult AdminBlogDashboard()
        {
            var viewModel = new BlogViewModel
            {
                Blogs = blogRepository.GetAllBlog(),
                Blog = new Blog()
            };

            return View(viewModel);
        }

        // Action để hiển thị chi tiết sản phẩm
        public IActionResult ProductDetails(int id)
        {
            var product = productRepository.GetProductDetail(id);
            if (product != null)
            {
                return View("ProductDetails", product);
            }
            return NotFound();
        }
        // Action để hiển thị form thêm mới sản phẩm
        [HttpGet]
        public IActionResult AddProduct()
        {
            return View(new Product());
        }

        // Action để xử lý thêm mới sản phẩm
        [HttpPost]
        public IActionResult AddProduct(Product product)
        {
            if (ModelState.IsValid)
            {
                productRepository.AddProduct(product);
                return RedirectToAction("AdminDashboard");
            }
            return View(product);
        }

        // Action chỉnh sửa sản phẩm
        [HttpGet]
        public IActionResult EditProduct(int id)
        {
            var product = productRepository.GetProductDetail(id);
            if (product != null)
            {
                return View("EditProduct", product);
            }
            return NotFound();
        }

        [HttpPost]
        public IActionResult EditProduct(Product product)
        {
            if (ModelState.IsValid)
            {
                productRepository.UpdateProduct(product);
                return RedirectToAction("AdminDashboard");
            }
            return View("AdminDashboard", new MyViewModels { Products = productRepository.GetAllProducts() });
        }

        // Action xóa sản phẩm
        public IActionResult DeleteProduct(int id)
        {
            productRepository.DeleteProduct(id);
            return RedirectToAction("AdminDashboard");
        }
        /// <summary>
        /// /////////////////////
        /// </summary>
        /// <returns></returns>
        /// 



        // Action để hiển thị form thêm mới blog
        [HttpGet]
        public IActionResult AddBlog()
        {
            return View(new Blog());
        }

        // Action để xử lý thêm mới blog
        [HttpPost]
        public IActionResult AddBlog(Blog blog)
        {
            if (ModelState.IsValid)
            {
                blogRepository.AddBlog(blog);
                return RedirectToAction("AdminBlogDashboard");
            }
            return View(blog);
        }
        // Action để hiển thị chi tiết blog
        public IActionResult BlogDetails(int id)
        {
            var blog = blogRepository.GetBlogById(id);
            if (blog != null)
            {
                return View("BlogDetails", blog);
            }
            return NotFound();
        }

        //Action chỉnh sửa blog
        [HttpGet]
        public IActionResult EditBlog(int id)
        {
            var blog = blogRepository.GetBlogById(id);
            if (blog != null)
            {
                return View("EditBlog", blog);
            }
            return NotFound();
        }

        // Action để xử lý chỉnh sửa blog
        [HttpPost]
        public IActionResult EditBlog(Blog blog)
        {
            if (ModelState.IsValid)
            {
                blogRepository.UpdateBlog(blog);
                return RedirectToAction("AdminBlogDashboard");
            }
            return View(blog);
        }

        // Action xóa blog
        public IActionResult DeleteBlog(int id)
        {
            blogRepository.DeleteBlog(id);
            return RedirectToAction("AdminBlogDashboard");
        }

        public IActionResult ManageOrders()
        {
            var orders = orderRepository.GetAllOrders();
            return View(orders);
        }

        public IActionResult OrderDetails(int orderId)
        {

            var orderDetails = orderRepository.GetOrderById(orderId);

            if (orderDetails != null)
            {
                return View(orderDetails);
            }

            return NotFound();
        }
        public IActionResult DeleteOrder(int orderId)
        {
            orderRepository.DeleteOrder(orderId);
            return RedirectToAction("ManageOrders");
        }

        public IActionResult LogOut()
        {
            _signInManager.SignOutAsync().Wait();

            return RedirectToAction("Index", "Product");
        }

       

    }
}

