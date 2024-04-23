namespace WebSunFlower.Models.Interfaces
{
    public interface IProductComment
    {
        void AddCommentProduct(ProductComment comment, int pId);
        ProductComment? GetCommentById(int commentId);
        IEnumerable<ProductComment> GetCommentsByProductId(int pId);
    }
}
