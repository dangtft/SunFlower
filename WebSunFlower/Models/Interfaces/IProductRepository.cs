namespace WebSunFlower.Models.Interfaces
{
    public interface IProductRepository
    {
        IEnumerable<Product> GetAllProducts();
        IEnumerable<Product> GetSalingProducts();
        Product GetProductDetail(int id);
        public IEnumerable<Product> SearchProductsByName(string productName);

        void AddProduct(Product product);
        void UpdateProduct(Product product); 
        void DeleteProduct(int id);
        void AddEmailSubscription(EmailSubscribe emailSubscribe);
        IEnumerable<ProductComment> GetCommentsForProduct(int id);
    }
}
