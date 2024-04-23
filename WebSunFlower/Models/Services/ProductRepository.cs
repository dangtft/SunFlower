using WebSunFlower.Models.Interfaces;
using WebSunFlower.Data;
using Microsoft.EntityFrameworkCore;

namespace WebSunFlower.Models.Services
{
    public class ProductRepository : IProductRepository
    {
        private SunFlowerDbContext dbContext;
        public ProductRepository(SunFlowerDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public IEnumerable<Product> GetAllProducts()
        {
            return dbContext.Products.ToList();
        }
        public Product? GetProductDetail(int id)
        {
            return dbContext.Products.FirstOrDefault(p => p.Id == id);

        }
        public IEnumerable<Product> GetSalingProducts()
        {
            return dbContext.Products.Where(p => p.IsSalingProduct == true);
        }
        public IEnumerable<Product> SearchProductsByName(string productName)
        {
            return dbContext.Products.Where(p => EF.Functions.Like(p.Name, $"%{productName}%")).ToList();
        }

        public void AddProduct(Product product)
        {
            dbContext.Products.Add(product);
            dbContext.SaveChanges();
        }

        public void UpdateProduct(Product product)
        {
            var existingProduct = dbContext.Products.Find(product.Id);
            if (existingProduct != null)
            {
                existingProduct.Name = product.Name;
                existingProduct.Detail = product.Detail;
                existingProduct.ImageUrl = product.ImageUrl;
                existingProduct.Price = product.Price;
                existingProduct.IsSalingProduct = product.IsSalingProduct;

                dbContext.SaveChanges();
            }
        }

        public void DeleteProduct(int id)
        {
            var product = dbContext.Products.Find(id);
            if (product != null)
            {
                dbContext.Products.Remove(product);
                dbContext.SaveChanges();
            }
        }

        public void AddEmailSubscription(EmailSubscribe emailSubscribe)
        {
            dbContext.EmailSubscriptions.Add(emailSubscribe);
            dbContext.SaveChanges();
        }

        public IEnumerable<ProductComment> GetCommentsForProduct(int id)
        {
            return dbContext.ProductComments.Where(p => p.ProductId == id).ToList();
        }
    }
}
