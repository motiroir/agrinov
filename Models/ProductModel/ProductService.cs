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
            string dirPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "img", "products");
            _DBContext.Products.Add(new Product()
            {
                Name = "Pot de miel moyen",
                Category = "Miel",
                CreationDate = DateTime.Now,
                Price = 3,
                ExpirationDate = new DateTime(2024, 12, 12),
                Description = "Ingrédients : miel de fleurs 99.96 %, mélange d'huiles essentielles d'eucalyptus, de menthe poivrée, de citron, de thym et de lavande 0.04% tous les ingrédients sont issus de l'agriculture biologique",
                Stock = 10,
                SupplierId = 1,
                ImgProduct = File.ReadAllBytes(Path.Combine(dirPath, "miel_product.jpg"))
            });

            _DBContext.Products.Add(new Product()
            {
                Name = "Pâtes de blé entier",
                Category = "Pâtes",
                CreationDate = DateTime.Now,
                Price = 3.5M,
                ExpirationDate = new DateTime(2025, 1, 30),
                Description = "Ingrédients : Semoule de blé dur entière, eau, tous les ingrédients sont issus de l'agriculture biologique",
                Stock = 50,
                SupplierId = 1,
                ImgProduct = File.ReadAllBytes(Path.Combine(dirPath, "pates_blé_product.jpg"))
            });

            _DBContext.Products.Add(new Product()
            {
                Name = "Huile d'olive extra vierge",
                Category = "Huiles",
                CreationDate = DateTime.Now,
                Price = 10.5M,
                ExpirationDate = new DateTime(2026, 5, 15),
                Description = "Huile d'olive de première pression à froid. Ingrédients : Olives, tous les ingrédients sont issus de l'agriculture biologique",
                Stock = 20,
                SupplierId= 1,
                ImgProduct = File.ReadAllBytes(Path.Combine(dirPath, "huile_olive_product.jpg"))
            });

            _DBContext.Products.Add(new Product()
            {
                Name = "Confiture de myrtilles",
                Category = "Confitures",
                CreationDate = DateTime.Now,
                Price = 5.5M,
                ExpirationDate = new DateTime(2025, 8, 12),
                Description = "Ingrédients : Myrtilles sauvages, sucre de canne, tous les ingrédients sont issus de l'agriculture biologique",
                Stock = 30,
                SupplierId = 3,
                ImgProduct = File.ReadAllBytes(Path.Combine(dirPath, "confiture_myrtilles_product.jpg"))
            });

            _DBContext.Products.Add(new Product()
            {
                Name = "Coulis de tomates",
                Category = "Conserves",
                CreationDate = DateTime.Now,
                Price = 4,
                ExpirationDate = new DateTime(2024, 11, 10),
                Description = "Ingrédients : Tomates issues de l'Agriculture Biologique (99,6%), sel de Guérande, tous les ingrédients sont issus de l'agriculture biologique",
                Stock = 15,
                SupplierId = 2,
                ImgProduct = File.ReadAllBytes(Path.Combine(dirPath, "coulis_tomates_product.jpg"))
            });

            _DBContext.Products.Add(new Product()
            {
                Name = "Lait de vache",
                Category = "Produit laitier",
                CreationDate = DateTime.Now,
                Price = 3,
                ExpirationDate = new DateTime(2025, 9, 30),
                Description = "Ingrédients : Lait entier pasteurisé de vache (origine France), tous les ingrédients sont issus de l'agriculture biologique",
                Stock = 25,
                SupplierId = 3,
                ImgProduct = File.ReadAllBytes(Path.Combine(dirPath, "lait_product.jpg"))
            });

            _DBContext.Products.Add(new Product()
            {
                Name = "Jus d'orange",
                Category = "Fruits",
                CreationDate = DateTime.Now,
                Price = 3.5M,
                ExpirationDate = new DateTime(2026, 3, 15),
                Description = "Ingrédients : Jus d'orange (96%), pulpe d'orange (4%), tous les ingrédients sont issus de l'agriculture biologique",
                Stock = 40,
                SupplierId = 2,
                ImgProduct = File.ReadAllBytes(Path.Combine(dirPath, "jus_orange_product.jpg"))
            });
            
            _DBContext.Products.Add(new Product()
            {
                Name = "Jus de pommes",
                Category = "Fruits",
                CreationDate = DateTime.Now,
                Price = 4,
                ExpirationDate = new DateTime(2026, 3, 15),
                Description = "Ingrédients : Pommes, eau, tous les ingrédients sont issus de l'agriculture biologique",
                Stock = 40,
                SupplierId = 2,
                ImgProduct = File.ReadAllBytes(Path.Combine(dirPath, "jus_pommes_product.jpg"))
            });

            _DBContext.Products.Add(new Product()
            {
                Name = "Confiture de fraises",
                Category = "Confitures",
                CreationDate = DateTime.Now,
                Price = 4.5M,
                ExpirationDate = new DateTime(2025, 6, 20),
                Description = "Ingrédients : Fraises, sucre de canne, tous les ingrédients sont issus de l'agriculture biologique",
                Stock = 12,
                SupplierId = 3,
                ImgProduct = File.ReadAllBytes(Path.Combine(dirPath, "confiture_fraises_product.jpg"))
            });
            
            _DBContext.Products.Add(new Product()
            {
                Name = "Bière blonde",
                Category = "Boisson",
                CreationDate = DateTime.Now,
                Price = 6.5M,
                ExpirationDate = new DateTime(2025, 6, 20),
                Description = "Lot de 3. Ingrédients : Eau, malt d'orge, malt de blé, sucre de canne, houblon, levure, tous les ingrédients sont issus de l'agriculture biologique",
                Stock = 12,
                SupplierId = 5,
                ImgProduct = File.ReadAllBytes(Path.Combine(dirPath, "pack_biere_blonde_product.jpg"))
            });
            
            _DBContext.Products.Add(new Product()
            {
                Name = "Bière IPA",
                Category = "Boisson",
                CreationDate = DateTime.Now,
                Price = 8M,
                ExpirationDate = new DateTime(2025, 6, 20),
                Description = "Lot de 3. Ingrédients : Eau, malt d'orge, malt de blé, sucre de canne, houblon, levure, tous les ingrédients sont issus de l'agriculture biologique",
                Stock = 12,
                SupplierId = 5,
                ImgProduct = File.ReadAllBytes(Path.Combine(dirPath, "pack_biere_ipa_product.jpg"))
            });
            
            _DBContext.Products.Add(new Product()
            {
                Name = "Bière brune",
                Category = "Boisson",
                CreationDate = DateTime.Now,
                Price = 7.5M,
                ExpirationDate = new DateTime(2025, 6, 20),
                Description = "Lot de 3. Ingrédients : Eau, malt d'orge, malt de blé, sucre de canne, houblon, levure, tous les ingrédients sont issus de l'agriculture biologique",
                Stock = 12,
                SupplierId = 5,
                ImgProduct = File.ReadAllBytes(Path.Combine(dirPath, "pack_biere_brune_product.jpg"))
            });
            
            _DBContext.Products.Add(new Product()
            {
                Name = "Vin Rouge 2022",
                Category = "Boisson",
                CreationDate = DateTime.Now,
                Price = 6.5M,
                ExpirationDate = new DateTime(2025, 6, 20),
                Description = "Ingrédients : raisins  issus du cépage Melon de Bourgogne, tous les ingrédients sont issus de l'agriculture biologique",
                Stock = 12,
                SupplierId = 2,
                ImgProduct = File.ReadAllBytes(Path.Combine(dirPath, "vin_rouge_product.jpg"))
            });
            
            _DBContext.Products.Add(new Product()
            {
                Name = "Vin Blanc 2022",
                Category = "Boisson",
                CreationDate = DateTime.Now,
                Price = 7.5M,
                ExpirationDate = new DateTime(2025, 6, 20),
                Description = "Ingrédients : raisins  issus du cépage Melon de Bourgogne, tous les ingrédients sont issus de l'agriculture biologique",
                Stock = 12,
                SupplierId = 2,
                ImgProduct = File.ReadAllBytes(Path.Combine(dirPath, "vin_blanc_product.jpg"))
            });
            
            _DBContext.Products.Add(new Product()
            {
                Name = "Cuvée Chateau-Thébaud 2022",
                Category = "Boisson",
                CreationDate = DateTime.Now,
                Price = 18.5M,
                ExpirationDate = new DateTime(2025, 6, 20),
                Description = "Lot de 3, vin rouge, rosé et blanc. Ingrédients : raisins  issus du cépage Melon de Bourgogne, tous les ingrédients sont issus de l'agriculture biologique",
                Stock = 12,
                SupplierId = 2,
                ImgProduct = File.ReadAllBytes(Path.Combine(dirPath, "lot_vins_product.jpg"))
            });

            _DBContext.Products.Add(new Product()
            {
                Name = "Totebag agrinov",
                Category = "Sac",
                CreationDate = DateTime.Now,
                Price = 4,
                ExpirationDate = new DateTime(2025, 1, 1),
                Description = "Totebag en coton bio 100%, réutilisable",
                Stock = 20,
                SupplierId = 4,
                ImgProduct = File.ReadAllBytes(Path.Combine(dirPath, "tote_bag_product.jpg"))
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

