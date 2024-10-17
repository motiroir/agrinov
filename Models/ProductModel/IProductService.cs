using AgriNov.Models.ProductionModel;

namespace AgriNov.Models.ProductModel
{
    public interface IProductService : IDisposable
    {
        void CreateDeleteDatabase();
        void InitializeTable();

        List<Product> GetProducts();

        Product GetProductByID(int ProductID);

        void InsertProduct(Product product);

        void UpdateProduct(Product product);

        void DeleteProduct(int ProductID);

        void Save();


    }
}
