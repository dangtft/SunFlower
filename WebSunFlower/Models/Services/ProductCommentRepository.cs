using Microsoft.EntityFrameworkCore;
using WebSunFlower.Data;
using WebSunFlower.Models.Interfaces;
using WebSunFlower.ViewModel;
using System;

namespace WebSunFlower.Models.Services
{
    public class ProductCommentRepository : IProductComment
    {
        private readonly SunFlowerDbContext dbContext;
        public ProductCommentRepository(SunFlowerDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public void AddCommentProduct(ProductComment comment, int pId)
        {
            ProductComment newComment = new ProductComment
            {
                UserName = comment.UserName,
                Text = comment.Text,
                Email = comment.Email,
                CreatedAt = DateTime.UtcNow,
                ProductId = pId,
            };

            dbContext.ProductComments.Add(newComment);
            dbContext.SaveChanges();
        }
        public IEnumerable<ProductComment> GetCommentsByProductId(int pId)
        {
            return dbContext.ProductComments.Where(c => c.ProductId == pId).ToList();
        }
        public ProductComment? GetCommentById(int commentId)
        {
            return dbContext.ProductComments.FirstOrDefault(c => c.ProductId == commentId);
        }
    }
}
