using WebSunFlower.Models;
using System.Collections.Generic;

namespace WebSunFlower.ViewModel
{
    public class MyViewModels
    {
        public IEnumerable<Product> Products { get; set; }
        public Product Product { get; set; }
        public IEnumerable<Blog> Blogs { get; set; }
        public IEnumerable<EmailSubscribe> Emails { get; set; }
        public IEnumerable<ProductComment> Comments { get; set; }
        public ProductComment Comment { get; set; }
    }
    public class SearchViewModel
    {
        public IEnumerable<Product> ProductSearchResults { get; set; }
        public IEnumerable<Blog> BlogSearchResults { get; set; }
    }
    public class BlogViewModel
    { 
        public IEnumerable< Blog> Blogs { get; set; }
        public Blog Blog { get; set; }
        public IEnumerable<Comment> Comments { get; set; }
        public Comment Comment { get; set; }

        public int TotalCommentsCount { get; set; }
    }
    public class ContactVM
    {
        public IEnumerable<Contact> Contacts { get; set; }
        public Contact contact { get; set; }

        public IEnumerable<EmailSubscribe> subscribes { get; set; }
        public EmailSubscribe subscribe { get; set; }

    }

}
