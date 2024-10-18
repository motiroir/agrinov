
namespace AgriNov.Models
{
    public class ProductService : IProductService
    {

        private BDDContext _DBContext;
        private bool disposedValue;

        public ProductService()
        {
            _DBContext = new BDDContext();
        }
        public void CreateDeleteDatabase()
        {
            _DBContext.Database.EnsureDeleted();
            _DBContext.Database.EnsureCreated();

        }
        public void DeleteProduct(int id)
        {
            throw new NotImplementedException();
        }
        public Product GetProduct(int id)
        {
            return this._DBContext.Products.Find(id);
        }

        public List<Product> GetAllProduct()
        {
            return _DBContext.Products.ToList();
        }
        public Product GetProductByID(int ProductID)
        {
            throw new NotImplementedException();
        }

        public Product GetProductByID(string ProductID)
        {
            throw new NotImplementedException();
        }

        public List<Product> GetProducts()
        {
            throw new NotImplementedException();
        }

        public void InitializeTable()
        {
            _DBContext.Products.Add(new Product()
            {
                Name = "Pot de miel moyen",
                Category = "Miel",
                CreationDate = new DateTime(2024, 12, 12),
                Price = 3,
                ExpirationDate = new DateTime(2024, 12, 12),
                Description = "azaz",
                Stock = 10
            });
            
            _DBContext.SaveChanges();
        }
         
        public void InsertProduct(Product product)
        {
            _DBContext.Products.Add(product);
                _DBContext.SaveChanges();
        }

        public void Save()
        {
            throw new NotImplementedException();
        }

        public void UpdateProduct(Product product)
        {
            throw new NotImplementedException();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: supprimer l'état managé (objets managés)
                }

                // TODO: libérer les ressources non managées (objets non managés) et substituer le finaliseur
                // TODO: affecter aux grands champs une valeur null
                disposedValue = true;
            }
        }

        // // TODO: substituer le finaliseur uniquement si 'Dispose(bool disposing)' a du code pour libérer les ressources non managées
        // ~ProductService()
        // {
        //     // Ne changez pas ce code. Placez le code de nettoyage dans la méthode 'Dispose(bool disposing)'
        //     Dispose(disposing: false);
        // }

        public void Dispose()
        {
            // Ne changez pas ce code. Placez le code de nettoyage dans la méthode 'Dispose(bool disposing)'
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

        internal object GetProduct()
        {
            throw new NotImplementedException();
        }
    }

}