
using AgriNov.Models.SharedStatus;
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

        public void DeleteProduction(int productionID)
        {
            Production production = _DBContext.Productions.Find(productionID);
            if (production != null)
            {
                this._DBContext.Productions.Remove(production);
            }
            this.Save();

            UpdateStockQuantity(production);
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

        public void Initializetable()
        {
            Production p1 = new Production() 
            { 
                ProductType = ProductType.VEGETABLES,
                VolumePerDelivery = 50,
                Price = 150,
                DeliveryFrequency = DeliveryFrequency.MONTHLY,
                Seasons = SharedStatus.Seasons.WINTER,
                Years = SharedStatus.Years._2024,
                DateLimitForReview = new DateTime(2024,11,01) 
            };
            InsertProduction(p1);
        }

        public void InsertProduction(Production production)
        {
            production.ValidationStatus = SharedStatus.ValidationStatus.WAITING;
            production.DateCreated = DateTime.Now;
            production.DateLastModified = DateTime.Now;
            _DBContext.Productions.Add(production);
            Save();

            UpdateStockQuantity(production);
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

            UpdateStockQuantity(newProduction);
        }

        private void UpdateStockQuantity(Production production)
        {
            Stock stock = _DBContext.Stocks
                .FirstOrDefault(s => s.Season.Name == production.Seasons && s.ProductType == production.ProductType);

            if (stock != null)
            {
                stock.TotalQuantity = _DBContext.Productions
                    .Where(p => p.Seasons == production.Seasons && p.ProductType == production.ProductType)
                    .Sum(p => p.VolumePerDelivery * (int)p.DeliveryFrequency); 

                Save();
            } else
            {
                InsertStock(production);
            }
        }
        public void InsertStock(Production production)
        {
            Season Season = _DBContext.Seasons
                .FirstOrDefault(s => s.Name == production.Seasons && s.Year == production.Years);

            // insert a new season if season is null

            Stock stock = new Stock()
            {
                ProductType = production.ProductType,
                Season = Season
            };

            _DBContext.Stocks.Add(stock);
            Save();
        }
    }
}
