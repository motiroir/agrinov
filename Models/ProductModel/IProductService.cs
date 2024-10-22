using AgriNov.Models.ProductionModel;

namespace AgriNov.Models
{
    public interface IProductService : IDisposable
    {
        void CreateDeleteDatabase();
        void InitializeTable();

        List<Product> GetProducts();

        Product GetProductByID(int id);

        void InsertProduct(Product product);

        void UpdateProduct(Product product);

        void DeleteProduct(int productId);

        void Save();


    }
}
