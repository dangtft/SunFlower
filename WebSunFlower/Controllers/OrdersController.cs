using Microsoft.AspNetCore.Mvc;
using WebSunFlower.Models.Interfaces;
using WebSunFlower.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System.Security.Claims;

namespace WebSunFlower.Controllers
{
    [Authorize]
    public class OrdersController : Controller
    {
        private IOrderRepository orderRepository;
        private IShoppingCartRepository shoppingCartRepository;
        public OrdersController(IOrderRepository oderRepository, IShoppingCartRepository shoppingCartRepossitory)
        {
            this.orderRepository = oderRepository;
            this.shoppingCartRepository = shoppingCartRepossitory;
        }
        public IActionResult Checkout()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Checkout(Order order)
        {
            orderRepository.PlaceOrder(order);
            shoppingCartRepository.ClearCart();
            HttpContext.Session.SetInt32("CartCount", 0);
            return RedirectToAction("CheckoutComplete");
        }
        public IActionResult CheckoutComplete()
        {

            return View();
        }

        public IActionResult AllOrder()
        {
            var allOrders = orderRepository.GetAllOrders();
            return View(allOrders);
        }
        public IActionResult CompletedOrders()
        {
            var userId = User.Identity.Name;
            
            var completedOrders = orderRepository.GetCompletedOrders(userId);

            return View(completedOrders);
        }
    }
}
