using Microsoft.EntityFrameworkCore;

namespace AgriNov.Models
{
    public class ServiceProduction : IServiceProduction
    {
        private BDDContext _DBContext;

        public ServiceProduction()
        {
            _DBContext = new BDDContext();
        }

        public void CreateDeleteDatabase()
        {
            _DBContext.Database.EnsureDeleted();
            _DBContext.Database.EnsureCreated();
        }

        public void Initializetable()
        {

            var productions = new List<Production>
    {
        new Production
        {
            CompanyName = "La ferme des blés",
            ProductType = ProductType.VEGETABLES,
            VolumePerDelivery = 50,
            Price = 3,
            DeliveryFrequency = DeliveryFrequency.MONTHLY,
            Seasons = Seasons.WINTER,
            Years = Years._2024,
            ValidationStatus = ValidationStatus.APPROVED,
            Description ="Carottes, Radis",
            UserAccountId= 5
        },
        new Production
        {
            CompanyName = "La ferme des blés",
            ProductType = ProductType.VEGETABLES,
            VolumePerDelivery = 20,
            Price = 4,
            DeliveryFrequency = DeliveryFrequency.WEEKLY,
            Seasons = Seasons.WINTER,
            Years = Years._2024,
            ValidationStatus = ValidationStatus.APPROVED,
            Description="Navets, Panets",
            UserAccountId= 5
        },
        new Production
        {   CompanyName = "Fruits du Pays",
            ProductType = ProductType.FRUITS,
            VolumePerDelivery = 30,
            Price = 3,
            DeliveryFrequency = DeliveryFrequency.WEEKLY,
            Seasons = Seasons.WINTER,
            Years = Years._2024,
            ValidationStatus = ValidationStatus.APPROVED,
            Description ="Pommes",
            UserAccountId= 6
        },
        new Production
        {   CompanyName = "Les Fermiers de Vertou",
            ProductType = ProductType.VEGETABLES,
            VolumePerDelivery = 50,
            Price = 4,
            DeliveryFrequency = DeliveryFrequency.BIWEEKLY,
            Seasons = Seasons.WINTER,
            Years = Years._2024,
            ValidationStatus = ValidationStatus.APPROVED,
           Description ="Navets",
            UserAccountId= 20
        },
        new Production
        {   CompanyName = "Maraîchage Bernard",
            ProductType = ProductType.VEGETABLES,
            VolumePerDelivery = 25,
            Price = 3,
            DeliveryFrequency = DeliveryFrequency.MONTHLY,
            Seasons = Seasons.WINTER,
            Years = Years._2024,
            ValidationStatus = ValidationStatus.APPROVED,
            Description ="Carottes",
            UserAccountId= 19
        },

        new Production
        {   CompanyName = "La ferme des blés",
            ProductType = ProductType.MEAT,
            VolumePerDelivery = 20,
            Price = 21,
            DeliveryFrequency = DeliveryFrequency.WEEKLY,
            Seasons = Seasons.WINTER,
            Years = Years._2024,
            ValidationStatus = ValidationStatus.APPROVED,
            Description ="Boeufs",
            UserAccountId= 5
        },

        new Production
        {   CompanyName = "Légumes Bio",
            ProductType = ProductType.VEGETABLES,
            VolumePerDelivery = 60,
            Price = 4,
            DeliveryFrequency = DeliveryFrequency.MONTHLY,
            Seasons = Seasons.SPRING,
            Years = Years._2025,
            ValidationStatus = ValidationStatus.APPROVED,
           Description ="Navets",
            UserAccountId= 8
        },
        new Production
        {   CompanyName = "Légumes Bio",
            ProductType = ProductType.VEGETABLES,
            VolumePerDelivery = 40,
            Price = 3,
            DeliveryFrequency = DeliveryFrequency.WEEKLY,
            Seasons = Seasons.SPRING,
            Years = Years._2025,
            ValidationStatus = ValidationStatus.APPROVED,
           Description ="Carottes, Oignons, Laitues, Épinards",
            UserAccountId= 8
        },
        new Production
        {   CompanyName = "Fruits du Pays",
            ProductType = ProductType.FRUITS,
            VolumePerDelivery = 40,
            Price = 4,
            DeliveryFrequency = DeliveryFrequency.WEEKLY,
            Seasons = Seasons.SPRING,
            Years = Years._2025,
            ValidationStatus = ValidationStatus.APPROVED,
            Description ="Pommes",
            UserAccountId= 6
        },
        new Production
        {   CompanyName = "Fruits et Légumes Bio",
            ProductType = ProductType.VEGETABLES,
            VolumePerDelivery = 30,
            Price = 20,
            DeliveryFrequency = DeliveryFrequency.BIWEEKLY,
            Seasons = Seasons.SPRING,
            Years = Years._2025,
            ValidationStatus = ValidationStatus.REFUSED,
            Description ="Épinards",
            UserAccountId= 18
        },
        new Production
        {   CompanyName = "Les Fermiers de Vertou",
            ProductType = ProductType.VEGETABLES,
            VolumePerDelivery = 35,
            Price = 3,
            DeliveryFrequency = DeliveryFrequency.MONTHLY,
            Seasons = Seasons.SPRING,
            Years = Years._2025,
            ValidationStatus = ValidationStatus.APPROVED,
            Description ="Laitues",
            UserAccountId= 20
        },

        new Production
        {   CompanyName = "La ferme des blés",
            ProductType = ProductType.MEAT,
            VolumePerDelivery = 25,
            Price = 23,
            DeliveryFrequency = DeliveryFrequency.WEEKLY,
            Seasons = Seasons.SPRING,
            Years = Years._2025,
            ValidationStatus = ValidationStatus.APPROVED,
            Description ="Volailles",
            UserAccountId= 5
        },

        new Production
        {   CompanyName = "Les récoltes de Claire",
            ProductType = ProductType.VEGETABLES,
            VolumePerDelivery = 20,
            Price = 8,
            DeliveryFrequency = DeliveryFrequency.WEEKLY,
            Seasons = Seasons.SPRING,
            Years = Years._2024,
            ValidationStatus = ValidationStatus.APPROVED,
            Description ="Mâche, Chou-fleur, Carottes, Poireau",
            UserAccountId= 7
        },

        new Production
        {   CompanyName = "Les récoltes de Claire",
            ProductType = ProductType.VEGETABLES,
            VolumePerDelivery = 40,
            Price = 10,
            DeliveryFrequency = DeliveryFrequency.WEEKLY,
            Seasons = Seasons.SPRING,
            Years = Years._2024,
            ValidationStatus = ValidationStatus.REFUSED,
            Description ="Carottes, Poireau",
            UserAccountId= 7
        },

        new Production
        {   CompanyName = "Les récoltes de Claire",
            ProductType = ProductType.VEGETABLES,
            VolumePerDelivery = 40,
            Price = 10,
            DeliveryFrequency = DeliveryFrequency.WEEKLY,
            Seasons = Seasons.SPRING,
            Years = Years._2024,
            ValidationStatus = ValidationStatus.APPROVED,
            Description ="Mâche, Chou-fleur",
            UserAccountId= 7
        },

        new Production
        {   CompanyName = "Les récoltes de Claire",
            ProductType = ProductType.VEGETABLES,
            VolumePerDelivery = 50,
            Price = 12,
            DeliveryFrequency = DeliveryFrequency.WEEKLY,
            Seasons = Seasons.SUMMER,
            Years = Years._2024,
            ValidationStatus = ValidationStatus.APPROVED,
            Description ="Tomate, Concombre, Epinard",
            UserAccountId= 7
        },

        new Production
        {   CompanyName = "Les récoltes de Claire",
            ProductType = ProductType.VEGETABLES,
            VolumePerDelivery = 50,
            Price = 12,
            DeliveryFrequency = DeliveryFrequency.WEEKLY,
            Seasons = Seasons.SUMMER,
            Years = Years._2024,
            ValidationStatus = ValidationStatus.APPROVED,
            Description ="Carotte, Radis, Mâche",
            UserAccountId= 7
        },

        new Production
        {   CompanyName = "Les récoltes de Claire",
            ProductType = ProductType.VEGETABLES,
            VolumePerDelivery = 60,
            Price = 12,
            DeliveryFrequency = DeliveryFrequency.WEEKLY,
            Seasons = Seasons.AUTUMN,
            Years = Years._2024,
            ValidationStatus = ValidationStatus.APPROVED,
            Description ="Poireau, Epinard, Courge, Mâche",
            UserAccountId= 7
        },

        new Production
        {   CompanyName = "Les récoltes de Claire",
            ProductType = ProductType.VEGETABLES,
            VolumePerDelivery = 60,
            Price = 12,
            DeliveryFrequency = DeliveryFrequency.WEEKLY,
            Seasons = Seasons.WINTER,
            Years = Years._2024,
            ValidationStatus = ValidationStatus.APPROVED,
            Description ="Courge, Carotte, Betterave, Chou-fleur, Mâche",
            UserAccountId= 7
        }
    };


            foreach (var production in productions)
            {
                InsertProduction(production);
            }
        }

        public void DeleteProduction(int productionID)
        {
            Production production = _DBContext.Productions.Find(productionID);
            if (production != null)
            {
                this._DBContext.Productions.Remove(production);
            }
            this.Save();

        }

        public void Dispose()
        {
            _DBContext.Dispose();
        }

        public Production GetProductionByID(int productionID)
        {
            return this._DBContext.Productions.Find(productionID);
        }

        public Production GetProductionByID(string productionIDStr)
        {
            int id;
            if (int.TryParse(productionIDStr, out id))
            {
                return this.GetProductionByID(id);
            }
            return null;
        }

        public List<Production> GetProductions()
        {
            return _DBContext.Productions.ToList();
        }

        public List<Production> GetProductionsWithSupplierPdfProof()
        {
            return _DBContext.Productions.Include(p => p.UserAccount).ThenInclude(ua => ua.Supplier).ToList();
        }

        public List<Production> GetProductionsBySupplier(int supplierID)
        {
            return _DBContext.Productions.Where(production => production.UserAccountId == supplierID).ToList();
        }


        public void InsertProduction(Production production)
        {
            if (production.ValidationStatus != ValidationStatus.APPROVED && production.ValidationStatus != ValidationStatus.REFUSED)
            {

                production.ValidationStatus = ValidationStatus.WAITING;
            }

            production.DateCreated = DateTime.Now;
            production.DateLastModified = DateTime.Now;
            _DBContext.Productions.Add(production);
            Save();
        }

        public void Save()
        {
            _DBContext.SaveChanges();
        }

        public void UpdateProduction(Production newProduction)
        {
            Production oldProduction = this.GetProductionByID(newProduction.Id);
            if (oldProduction == null)
            {
                return;
            }
            newProduction.DateLastModified = DateTime.Now;

            _DBContext.Entry(oldProduction).CurrentValues.SetValues(newProduction);
            Save();

        }

        public int CalculateStock(ProductType productType, Seasons seasons, Years years)
        {
            List<Production> productions = _DBContext.Productions
                .Where(p => p.ProductType == productType &&
                            p.Seasons == seasons &&
                            p.Years == years)
                .ToList();

            int stock = productions.Sum(p => p.VolumePerDelivery * GetNumberOfDelivery(p.DeliveryFrequency));

            return stock;
        }
        public int GetNumberOfDelivery(DeliveryFrequency frequency)
        {
            switch (frequency)
            {
                case DeliveryFrequency.WEEKLY:
                    return 13;
                case DeliveryFrequency.BIWEEKLY:
                    return 6;
                case DeliveryFrequency.MONTHLY:
                    return 3;
                default:
                    throw new ArgumentOutOfRangeException(nameof(frequency), frequency, "Frequency is not recognized");
            }
        }

    }
}
