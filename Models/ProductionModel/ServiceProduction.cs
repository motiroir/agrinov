
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
            Production p1 = new Production()
            {
                ProductType = ProductType.VEGETABLES,
                VolumePerDelivery = 50,
                Price = 150,
                DeliveryFrequency = DeliveryFrequency.MONTHLY,
                Seasons = SharedStatus.Seasons.WINTER,
                Years = SharedStatus.Years._2024,
                DateLimitForReview = new DateTime(2024, 11, 01)
            };
            InsertProduction(p1);

            Console.WriteLine(_DBContext.Stocks.ToList());
            Console.WriteLine(_DBContext.WeeklyStocks.ToList());

            p1.ValidationStatus = ValidationStatus.APPROVED;
            UpdateProduction(p1);
            Console.WriteLine(_DBContext.Stocks.ToList());
            Console.WriteLine(_DBContext.WeeklyStocks.ToList());

            p1.VolumePerDelivery = 100;
            UpdateProduction(p1);
            Console.WriteLine(_DBContext.Stocks.ToList());
            Console.WriteLine(_DBContext.WeeklyStocks.ToList());
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
            production.ValidationStatus = SharedStatus.ValidationStatus.WAITING;
            production.DateCreated = DateTime.Now;
            production.DateLastModified = DateTime.Now;
            _DBContext.Productions.Add(production);
            Save();
        }

        private int GetTotalDeliveries(Stock stock)
        {
            return (int)Math.Round((stock.Season.EndDate - stock.Season.StartDate).TotalDays / 7.0, MidpointRounding.AwayFromZero);
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


            // If ValidationStatus is approved
            if (newProduction.ValidationStatus == ValidationStatus.APPROVED)
            {
                // If ValidationStatus was not already approved
                if (oldProduction.ValidationStatus != ValidationStatus.APPROVED)
                {
                    AddProductionQuantities(newProduction);

                // If ValidationStatus was already approved
                }
                else
                {
                    UpdateProductionQuantities(newProduction, oldProduction);
                }
            }
            _DBContext.Entry(oldProduction).CurrentValues.SetValues(newProduction);
            Save();

        }

        private void UpdateProductionQuantities(Production newProduction, Production oldProduction)
        {
            // Associated stock
            Stock Stock = _DBContext.Stocks
                .Include(s => s.WeeklyStocks)
                .FirstOrDefault(s => s == newProduction.Stock);

            if (Stock != null)
            {
                // oldQuantity
                int oldTotalDeliveries = GetTotalDeliveries(oldProduction.Stock);
                int oldQuantity = oldProduction.VolumePerDelivery * oldTotalDeliveries;

                // newQuantity
                int newTotalDeliveries = GetTotalDeliveries(newProduction.Stock);
                int newQuantity = newProduction.VolumePerDelivery * newTotalDeliveries;

                // Update stock TotalQuantity
                Stock.TotalQuantity = Stock.TotalQuantity - oldQuantity + newQuantity;
                
                // Mettre à jour les stocks hebdomadaires
                UpdateWeeklyStocks(Stock, newProduction);
            }

            Save();
        }

        private void AddProductionQuantities(Production newProduction)
        {
            // Stock exist for season and production ?
            Stock existingStock = _DBContext.Stocks
                .FirstOrDefault(s => s.ProductType == newProduction.ProductType && s.Season.Name == newProduction.Seasons);

            if (existingStock != null)
            {
                newProduction.Stock = existingStock;

                // Update stock TotalQuantity
                existingStock.TotalQuantity += newProduction.VolumePerDelivery * GetTotalDeliveries(newProduction.Stock);
                // Update weakly WeeklyStocks
                UpdateWeeklyStocks(existingStock, newProduction);
            }
            else
            {
                // If no existing stock
                Season newSeason = new Season(newProduction.Years, newProduction.Seasons)
                {
                    Year = newProduction.Years,
                    Name = newProduction.Seasons
                };

                Stock newStock = new Stock
                {
                    ProductType = newProduction.ProductType,
                    TotalQuantity = newProduction.VolumePerDelivery * GetTotalDeliveries(newProduction.Stock),
                    Season = newSeason
                };

                // Add new season and stock to database
                _DBContext.Seasons.Add(newSeason);
                _DBContext.Stocks.Add(newStock);
                Save();

                newProduction.Stock = newStock;
                newProduction.Season = newSeason;

                // Init WeeklyStocks
                CreateWeeklyStocks(newStock, newProduction);
            }
            
        }

        private void CreateWeeklyStocks(Stock newStock, Production newProduction)
        {
            int weeksInSeason = GetTotalDeliveries(newStock);

            for (int week = 1; week <= weeksInSeason; week++)
            {
                if (ShouldAddDeliveryForWeek(week, newProduction.DeliveryFrequency))
                {
                    var weeklyStock = new WeeklyStock
                    {
                        Week = week,
                        AvailableQuantity = newProduction.VolumePerDelivery,
                        Stock = newStock
                    };

                    _DBContext.WeeklyStocks.Add(weeklyStock);
                }
            }

            Save();
        }


        private void UpdateWeeklyStocks(Stock existingStock, Production newProduction)
        {
            int weeksInSeason = GetTotalDeliveries(existingStock);

            // update stocks for each week
            foreach (var weeklyStock in existingStock.WeeklyStocks)
            {
                if (weeklyStock.Week <= weeksInSeason)
                {
                    if (ShouldAddDeliveryForWeek(weeklyStock.Week, newProduction.DeliveryFrequency))
                    {
                        weeklyStock.AvailableQuantity += newProduction.VolumePerDelivery;
                    }
                }
            }

            Save();
        }
        private bool ShouldAddDeliveryForWeek(int week, DeliveryFrequency frequency)
        {
            switch (frequency)
            {
                case DeliveryFrequency.WEEKLY:
                    return true;
                case DeliveryFrequency.BIWEEKLY:
                    return week % 2 != 0;
                case DeliveryFrequency.MONTHLY:
                    return week % 4 == 1;
                default:
                    return false;
            }
        }


    }
}
