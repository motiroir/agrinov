
using AgriNov.Models.ProductionModel;

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
            /* Product product = _DBContext.Products.Find( id);
             if (product != null)
             {
                 this._DBContext.Products.Remove(product);
             }
             this.Save();*/
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
            return _DBContext.Products.Find(ProductID);
        }

        public Product GetProductByID(string ProductID)
        {
            return _DBContext.Products.Find(ProductID);
        }

        public List<Product> GetProducts()
        {
            return _DBContext.Products.ToList();
        }

        public void InitializeTable()
        {
            _DBContext.Products.Add(new Product()
            {
                Name = "Pot de miel moyen",
                Category = "Miel",
                CreationDate = DateTime.Now,
                Price = 3,
                ExpirationDate = new DateTime(2024, 12, 12),
                Description = "azaz",
                Stock = 10,


            });

            _DBContext.Products.Add(new Product()
            {
                Name = "Pâtes de blé entier",
                Category = "Pâtes",
                CreationDate = DateTime.Now,
                Price = 2.5,
                ExpirationDate = new DateTime(2025, 1, 30),
                Description = "Pâtes 100% blé entier pour des repas sains",
                Stock = 50,
            });

            _DBContext.Products.Add(new Product()
            {
                Name = "Huile d'olive extra vierge",
                Category = "Huiles",
                CreationDate = DateTime.Now,
                Price = 7.5,
                ExpirationDate = new DateTime(2026, 5, 15),
                Description = "Huile d'olive de première pression à froid",
                Stock = 20,
            });

            _DBContext.Products.Add(new Product()
            {
                Name = "Vinaigre balsamique",
                Category = "Vinaigres",
                CreationDate = DateTime.Now,
                Price = 4,
                ExpirationDate = new DateTime(2025, 8, 12),
                Description = "Vinaigre balsamique de Modène",
                Stock = 30,
            });

            _DBContext.Products.Add(new Product()
            {
                Name = "Café moulu bio",
                Category = "Cafés",
                CreationDate = DateTime.Now,
                Price = 6,
                ExpirationDate = new DateTime(2024, 11, 10),
                Description = "Café moulu biologique pour un goût riche",
                Stock = 15,
            });

            _DBContext.Products.Add(new Product()
            {
                Name = "Thé vert Matcha",
                Category = "Thés",
                CreationDate = DateTime.Now,
                Price = 9,
                ExpirationDate = new DateTime(2025, 4, 18),
                Description = "Thé vert matcha en poudre",
                Stock = 8,
            });

            _DBContext.Products.Add(new Product()
            {
                Name = "Riz basmati",
                Category = "Céréales",
                CreationDate = DateTime.Now,
                Price = 3,
                ExpirationDate = new DateTime(2025, 9, 30),
                Description = "Riz basmati à grains longs et parfumé",
                Stock = 25,
            });

            _DBContext.Products.Add(new Product()
            {
                Name = "Sel de mer",
                Category = "Épices",
                CreationDate = DateTime.Now,
                Price = 2,
                ExpirationDate = new DateTime(2026, 3, 15),
                Description = "Sel de mer naturel pour assaisonner vos plats",
                Stock = 40,
            });

            _DBContext.Products.Add(new Product()
            {
                Name = "Confiture de fraises",
                Category = "Confitures",
                CreationDate = DateTime.Now,
                Price = 3.5,
                ExpirationDate = new DateTime(2025, 6, 20),
                Description = "Confiture artisanale de fraises",
                Stock = 12,
            });

            _DBContext.Products.Add(new Product()
            {
                Name = "Cacao en poudre",
                Category = "Cacaos",
                CreationDate = DateTime.Now,
                Price = 4,
                ExpirationDate = new DateTime(2025, 1, 1),
                Description = "Cacao en poudre non sucré pour pâtisserie",
                Stock = 20,
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
            Product oldProduct = _DBContext.Products.Find(product.Id);
            if (oldProduct != null)
            {
                oldProduct.Name = product.Name;
                oldProduct.Price = product.Price;
                oldProduct.CreationDate = product.CreationDate;
                oldProduct.ExpirationDate = product.ExpirationDate;
                oldProduct.Description = product.Description;
                oldProduct.Stock = product.Stock;
                oldProduct.Category = product.Category;
                _DBContext.SaveChanges();



                // faire tous les attributs du formulaire
                // oldproduct.  




            }


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

        //internal object GetProduct(int id)

        //return _DBContext.Products.Find(id);
    }
}

