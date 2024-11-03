
namespace AgriNov.Models
{
    public interface IProductService : IDisposable
    {
        void CreateDeleteDatabase();
        void InitializeTable();
        List<Product> GetProducts();
        Product GetProductByID(int id);
        Product GetProductByID(string ProductIDStr);
        string GetSupplierName(int supplierId);
        void InsertProduct(Product product);
        void UpdateProduct(Product product);
        void DeleteProduct(int productId);
        void Save();
    }
}
