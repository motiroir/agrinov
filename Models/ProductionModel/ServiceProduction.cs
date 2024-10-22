
using AgriNov.Models.SharedStatus;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace AgriNov.Models.ProductionModel
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
            Price = 150,
            DeliveryFrequency = DeliveryFrequency.MONTHLY,
            Seasons = Seasons.WINTER,
            Years = Years._2024,
            DateLimitForReview = new DateTime(2024, 11, 01)
        },
        new Production
        {
            CompanyName = "La ferme des blés",
            ProductType = ProductType.VEGETABLES,
            VolumePerDelivery = 20,
            Price = 70,
            DeliveryFrequency = DeliveryFrequency.WEEKLY,
            Seasons = Seasons.WINTER,
            Years = Years._2024,
            DateLimitForReview = new DateTime(2024, 11, 01)
        },
        new Production
        {   CompanyName = "Fruits du Pays",
            ProductType = ProductType.FRUITS,
            VolumePerDelivery = 30,
            Price = 100,
            DeliveryFrequency = DeliveryFrequency.WEEKLY,
            Seasons = Seasons.WINTER,
            Years = Years._2024,
            DateLimitForReview = new DateTime(2024, 11, 01)
        },
        new Production
        {   CompanyName = "Les Récoltes de Claire",
            ProductType = ProductType.EGGS,
            VolumePerDelivery = 20,
            Price = 200,
            DeliveryFrequency = DeliveryFrequency.BIWEEKLY,
            Seasons = Seasons.WINTER,
            Years = Years._2024,
            DateLimitForReview = new DateTime(2024, 11, 01)
        },
        new Production
        {   CompanyName = "Les Récoltes de Claire",
            ProductType = ProductType.DAYRIS,
            VolumePerDelivery = 25,
            Price = 180,
            DeliveryFrequency = DeliveryFrequency.MONTHLY,
            Seasons = Seasons.WINTER,
            Years = Years._2024,
            DateLimitForReview = new DateTime(2024, 11, 01)
        },
        new Production
        {   CompanyName = "Le HellFish",
            ProductType = ProductType.FISH,
            VolumePerDelivery = 15,
            Price = 220,
            DeliveryFrequency = DeliveryFrequency.BIWEEKLY,
            Seasons = Seasons.WINTER,
            Years = Years._2024,
            DateLimitForReview = new DateTime(2024, 11, 01)
        },
        new Production
        {   CompanyName = "La ferme des blés",
            ProductType = ProductType.MEAT,
            VolumePerDelivery = 40,
            Price = 250,
            DeliveryFrequency = DeliveryFrequency.WEEKLY,
            Seasons = Seasons.WINTER,
            Years = Years._2024,
            DateLimitForReview = new DateTime(2024, 11, 01)
        },

        new Production
        {   CompanyName = "Carottes Bio",
            ProductType = ProductType.VEGETABLES,
            VolumePerDelivery = 60,
            Price = 160,
            DeliveryFrequency = DeliveryFrequency.MONTHLY,
            Seasons = Seasons.SPRING,
            Years = Years._2025,
            DateLimitForReview = new DateTime(2025, 5, 01)
        },
        new Production
        {   CompanyName = "Carottes Bio",
            ProductType = ProductType.VEGETABLES,
            VolumePerDelivery = 40,
            Price = 100,
            DeliveryFrequency = DeliveryFrequency.WEEKLY,
            Seasons = Seasons.SPRING,
            Years = Years._2025,
            DateLimitForReview = new DateTime(2025, 5, 01)
        },
        new Production
        {   CompanyName = "Fruits du Pays",
            ProductType = ProductType.FRUITS,
            VolumePerDelivery = 40,
            Price = 120,
            DeliveryFrequency = DeliveryFrequency.WEEKLY,
            Seasons = Seasons.SPRING,
            Years = Years._2025,
            DateLimitForReview = new DateTime(2025, 5, 01)
        },
        new Production
        {   CompanyName = "Les Récoltes de Claire",
            ProductType = ProductType.EGGS,
            VolumePerDelivery = 30,
            Price = 200,
            DeliveryFrequency = DeliveryFrequency.BIWEEKLY,
            Seasons = Seasons.SPRING,
            Years = Years._2025,
            DateLimitForReview = new DateTime(2025, 5, 01)
        },
        new Production
        {   CompanyName = "Les Récoltes de Claire",
            ProductType = ProductType.DAYRIS,
            VolumePerDelivery = 35,
            Price = 180,
            DeliveryFrequency = DeliveryFrequency.MONTHLY,
            Seasons = Seasons.SPRING,
            Years = Years._2025,
            DateLimitForReview = new DateTime(2025, 5, 01)
        },
        new Production
        {   CompanyName = "Le HellFish",
            ProductType = ProductType.FISH,
            VolumePerDelivery = 25,
            Price = 220,
            DeliveryFrequency = DeliveryFrequency.BIWEEKLY,
            Seasons = Seasons.SPRING,
            Years = Years._2025,
            DateLimitForReview = new DateTime(2025, 5, 01)
        },
        new Production
        {   CompanyName = "La ferme des blés",
            ProductType = ProductType.MEAT,
            VolumePerDelivery = 50,
            Price = 250,
            DeliveryFrequency = DeliveryFrequency.WEEKLY,
            Seasons = Seasons.SPRING,
            Years = Years._2025,
            DateLimitForReview = new DateTime(2025, 5, 01)
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

        public void InsertProduction(Production production)
        {
            production.ValidationStatus = ValidationStatus.WAITING;
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
