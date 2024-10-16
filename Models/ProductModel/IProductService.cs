using AgriNov.Models.ProductionModel;

namespace AgriNov.Models.ProductModel
{
    public interface IProductService : IDisposable
    {
        void CreateDeleteDatabase();
        void Initializetable();

        List<Product> GetProducts();

        Product GetProductByID(int ProductID);

        Product GetProductByID(string ProductID);

        void InsertProduct(Product product);

        void UpdateProduct(Product product);

        void DeleteProduct(int ProductID);

        void Save();


    }
}
