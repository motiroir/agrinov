
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
                Seasons = Seasons.WINTER,
                Years = Years._2024,
                DateLimitForReview = new DateTime(2024, 11, 01)
            };
            InsertProduction(p1);
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


    }
}
