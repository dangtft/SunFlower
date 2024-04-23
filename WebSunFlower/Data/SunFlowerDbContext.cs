using Microsoft.EntityFrameworkCore;
using WebSunFlower.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;


namespace WebSunFlower.Data
{
    public class SunFlowerDbContext : IdentityDbContext
    {
        public SunFlowerDbContext(DbContextOptions<SunFlowerDbContext> options) : base(options)
        {
        }
        public DbSet<Product> Products { get; set; }
        public DbSet<ShoppingCartItem> ShoppingCartItems { get; set;}
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetail {  get; set; } 
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<Blog> Blogs { get; set; }
        public DbSet<Comment> Comments {  get; set; }
        public DbSet<EmailSubscribe> EmailSubscriptions { get; set; }
        public DbSet<Authorities> Authoritiess { get; set; }
        public DbSet<ProductComment> ProductComments { get; set; }

    }
}
