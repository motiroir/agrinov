

namespace AgriNov.Models
{
    public class ProductService : IProductService
    {
        private BDDContext _DBContext;

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
            Product product = _DBContext.Products.Find(id);
            if (product != null)
            {
                this._DBContext.Products.Remove(product);
            }
            Save();
        }

        public Product GetProductByID(int ProductID)
        {
            return _DBContext.Products.Find(ProductID);
        }

        public Product GetProductByID(string ProductIDStr)
        {
            int id;
            if(int.TryParse(ProductIDStr, out id))
            {
                return this.GetProductByID(id);
            }
            return null;
        }

        public List<Product> GetProducts()
        {
            return _DBContext.Products.ToList();
        }

        public string GetSupplierName(int supplierId)
        {
            using (ServiceSupplier sS = new ServiceSupplier())
            {
                Supplier supplier = sS.GetSupplierByID(supplierId);
                return supplier.ContactDetails.Name + " " + supplier.ContactDetails.FirstName;
            }
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
                Description = "Pot de miel aux fleurs d'acacia",
                Stock = 10,
                SupplierId = 1,
            });

            _DBContext.Products.Add(new Product()
            {
                Name = "Pâtes de blé entier",
                Category = "Pâtes",
                CreationDate = DateTime.Now,
                Price = 2.5M,
                ExpirationDate = new DateTime(2025, 1, 30),
                Description = "Pâtes 100% blé entier pour des repas sains",
                Stock = 50,
                SupplierId = 1,
            });

            _DBContext.Products.Add(new Product()
            {
                Name = "Huile d'olive extra vierge",
                Category = "Huiles",
                CreationDate = DateTime.Now,
                Price = 7.5M,
                ExpirationDate = new DateTime(2026, 5, 15),
                Description = "Huile d'olive de première pression à froid",
                Stock = 20,
                SupplierId= 1,
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
                SupplierId = 3,
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
                SupplierId = 2,
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
                SupplierId = 4,
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
                SupplierId = 3,
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
                SupplierId = 2,
            });

            _DBContext.Products.Add(new Product()
            {
                Name = "Confiture de fraises",
                Category = "Confitures",
                CreationDate = DateTime.Now,
                Price = 3.5M,
                ExpirationDate = new DateTime(2025, 6, 20),
                Description = "Confiture artisanale de fraises",
                Stock = 12,
                SupplierId = 3,
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
                SupplierId = 4,
            });

            Save();
        }

        public void InsertProduct(Product product)
        {
            product.CreationDate = DateTime.Now;
            _DBContext.Products.Add(product);
            Save();
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
                Save();
            }
        }

        public void Save()
        {
            _DBContext.SaveChanges();
        }

        public void Dispose()
        {
            _DBContext.Dispose();
        }
    }
}

