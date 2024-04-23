using WebSunFlower.Data;
using WebSunFlower.Models.Interfaces;
using WebSunFlower.Models;
using Microsoft.EntityFrameworkCore;

namespace WebSunFlower.Models.Services
{
    public class OrderRepository : IOrderRepository
    {
        private SunFlowerDbContext dbContext;
        private IShoppingCartRepository shoppingCartRepository;
        public OrderRepository(SunFlowerDbContext dbContext, IShoppingCartRepository shoppingCartRepository)
        {
            this.dbContext = dbContext;
            this.shoppingCartRepository = shoppingCartRepository;
        }

        public void PlaceOrder(Order order)
        {
            var shoppingCartItems = shoppingCartRepository.GetAllShoppingCartItems();
            order.OrderDetails = new List<OrderDetail>();
            foreach (var item in shoppingCartItems)
            {
                var orderDetail = new OrderDetail
                {
                    Quantity = item.Qty,
                    ProductId = item.Product.Id,
                    Price = item.Product.Price
                };
                order.OrderDetails.Add(orderDetail);
            }
            order.OrderPlaced = DateTime.Now;
            order.OrderTotal = shoppingCartRepository.GetShoppingCartTotal();
            dbContext.Orders.Add(order);

            dbContext.SaveChanges();
        }
        public IEnumerable<Order> GetAllOrders()
        {
            return dbContext.Orders.ToList();
        }
        public IEnumerable<Order> GetCompletedOrders(string userId)
        {
            return dbContext.Orders
                .Include(o => o.OrderDetails)
                .ThenInclude(od => od.Product)
                .Where(o => o.Email == userId && o.OrderPlaced < DateTime.Now).ToList();

        }
        public Order GetOrderById(int orderId)
        {
            return dbContext.Orders
                .Include(o => o.OrderDetails)
                .ThenInclude(od => od.Product)
                .FirstOrDefault(o => o.Id == orderId);
        }

        public void DeleteOrder(int orderId)
        {
            var orderToDelete = dbContext.Orders.Find(orderId);

            if (orderToDelete != null)
            {
                dbContext.Orders.Remove(orderToDelete);
                dbContext.SaveChanges();
            }
        }

    }
}
