

using System.Security.Cryptography;

namespace AgriNov.Models
{
    public class ServiceBoxContract : IServiceBoxContract
    {
        private BDDContext _DBContext;

        public ServiceBoxContract()
        {
            _DBContext = new BDDContext();
        }

        public void Save()
        {
            _DBContext.SaveChanges();
        }

        public void Dispose()
        {
            _DBContext.Dispose();
        }

        public void CreateDeleteDatabase()
        {
            _DBContext.Database.EnsureDeleted();
            _DBContext.Database.EnsureCreated();
        }

        public void InitializeTable()
        {
            BoxContract bc1 = new BoxContract() { ProductType = ProductType.VEGETABLES, Seasons = Seasons.SPRING, Years = Years._2024, Price = 8M, MaxSubscriptions = 20, ForSale = false };
            BoxContract bc2 = new BoxContract() { ProductType = ProductType.DAYRIS, Seasons = Seasons.SPRING, Years = Years._2024, Price = 5M, MaxSubscriptions = 12, ForSale = false };
            BoxContract bc3 = new BoxContract() { ProductType = ProductType.MEAT, Seasons = Seasons.SPRING, Years = Years._2024, Price = 16M, MaxSubscriptions = 10, ForSale = false };
            BoxContract bc4 = new BoxContract() { ProductType = ProductType.FISH, Seasons = Seasons.SPRING, Years = Years._2024, Price = 12M, MaxSubscriptions = 5, ForSale = false };
            BoxContract bc5 = new BoxContract() { ProductType = ProductType.EGGS, Seasons = Seasons.SPRING, Years = Years._2024, Price = 2M, MaxSubscriptions = 12, ForSale = false };

            BoxContract bc6 = new BoxContract() { ProductType = ProductType.VEGETABLES, Seasons = Seasons.AUTUMN, Years = Years._2024, Price = 8M, MaxSubscriptions = 25, ForSale = false };
            BoxContract bc7 = new BoxContract() { ProductType = ProductType.DAYRIS, Seasons = Seasons.AUTUMN, Years = Years._2024, Price = 5M, MaxSubscriptions = 10, ForSale = false };
            BoxContract bc8 = new BoxContract() { ProductType = ProductType.MEAT, Seasons = Seasons.AUTUMN, Years = Years._2024, Price = 16M, MaxSubscriptions = 10, ForSale = false };
            BoxContract bc9 = new BoxContract() { ProductType = ProductType.FISH, Seasons = Seasons.AUTUMN, Years = Years._2024, Price = 12M, MaxSubscriptions = 10, ForSale = false };
            BoxContract bc10 = new BoxContract() { ProductType = ProductType.EGGS, Seasons = Seasons.AUTUMN, Years = Years._2024, Price = 2M, MaxSubscriptions = 12, ForSale = false };

            BoxContract bc11 = new BoxContract() { ProductType = ProductType.VEGETABLES, Seasons = Seasons.AUTUMN, Years = Years._2024, Price = 8M, MaxSubscriptions = 28, ForSale = true };
            BoxContract bc12 = new BoxContract() { ProductType = ProductType.DAYRIS, Seasons = Seasons.AUTUMN, Years = Years._2024, Price = 5M, MaxSubscriptions = 12, ForSale = true };
            BoxContract bc13 = new BoxContract() { ProductType = ProductType.MEAT, Seasons = Seasons.AUTUMN, Years = Years._2024, Price = 16M, MaxSubscriptions = 12, ForSale = true };
            BoxContract bc14 = new BoxContract() { ProductType = ProductType.FISH, Seasons = Seasons.AUTUMN, Years = Years._2024, Price = 12M, MaxSubscriptions = 12, ForSale = true };
            BoxContract bc15 = new BoxContract() { ProductType = ProductType.EGGS, Seasons = Seasons.AUTUMN, Years = Years._2024, Price = 2M, MaxSubscriptions = 12, ForSale = true };
            
            InsertBoxContract(bc1);
            InsertBoxContract(bc2);
            InsertBoxContract(bc3);
            InsertBoxContract(bc4);
            InsertBoxContract(bc5);
            InsertBoxContract(bc6);
            InsertBoxContract(bc7);
            InsertBoxContract(bc8);
            InsertBoxContract(bc9);
            InsertBoxContract(bc10);
            InsertBoxContract(bc11);
            InsertBoxContract(bc12);
            InsertBoxContract(bc13);
            InsertBoxContract(bc14);
            InsertBoxContract(bc15);
        }

        public List<BoxContract> GetAllBoxContracts()
        {
            return _DBContext.BoxContracts.ToList();
        }
        public List<BoxContract> GetAllBoxContractsToSale()
        {
            return _DBContext.BoxContracts.Where(bc => bc.ForSale == true).ToList();
        }

        public BoxContract GetBoxContractById(int id)
        {
            return this._DBContext.BoxContracts.Find(id);
        }

        public void InsertBoxContract(BoxContract boxContract)
        {
            boxContract.DateCreated = DateTime.Now;
            boxContract.DateLastModified = DateTime.Now;
            _DBContext.BoxContracts.Add(boxContract);
            Save();
        }

        public void UpdateBoxContract(BoxContract newBoxContract)
        {
            BoxContract oldBoxContract = GetBoxContractById(newBoxContract.Id);
            if (oldBoxContract == null)
            {
                return;
            }
            newBoxContract.DateLastModified = DateTime.Now;

            _DBContext.Entry(oldBoxContract).CurrentValues.SetValues(newBoxContract);
            Save();
        }

        public void DeleteBoxContract(int id)
        {
            BoxContract boxContract = _DBContext.BoxContracts.Find(id);
            if (boxContract != null)
            {
                this._DBContext.BoxContracts.Remove(boxContract);
            }
            this.Save();

        }

        public bool ContractExist(int Id, ProductType productType, Seasons season, Years year)
        {
            return _DBContext.BoxContracts
                .Any(bc => bc.ProductType == productType && bc.Seasons == season && bc.Years == year && bc.Id != Id);
        }

        public bool ContractExist(ProductType productType, Seasons season, Years year)
        {
            return _DBContext.BoxContracts
                .Any(bc => bc.ProductType == productType && bc.Seasons == season && bc.Years == year);
        }

        // Do not call this method for seasons with value none
        public List<DateTime> ComputeSeasonStartAndEnd(Years year, Seasons season)
        {
            DateTime startDateTime;
            DateTime endDateTime;
            if (season == Seasons.AUTUMN)
            {
                startDateTime = new DateTime(int.Parse(year.GetDisplayName()), 9, 21, 0, 0, 0, 0);
                endDateTime = new DateTime(int.Parse(year.GetDisplayName()), 12, 20, 23, 59, 59, 999);
            }
            else if (season == Seasons.WINTER)
            {
                startDateTime = new DateTime(int.Parse(year.GetDisplayName()), 12, 21, 0, 0, 0, 0);
                endDateTime = new DateTime(int.Parse(year.GetDisplayName()) + 1, 3, 20, 23, 59, 59, 999);
            }
            else if (season == Seasons.SPRING)
            {
                startDateTime = new DateTime(int.Parse(year.GetDisplayName()), 3, 21, 0, 0, 0, 0);
                endDateTime = new DateTime(int.Parse(year.GetDisplayName()), 6, 20, 23, 59, 59, 999);
            }
            else // (season == Seasons.SUMMER)
            {
                startDateTime = new DateTime(int.Parse(year.GetDisplayName()), 6, 21, 0, 0, 0, 0);
                endDateTime = new DateTime(int.Parse(year.GetDisplayName()), 9, 20, 23, 59, 59, 999);
            }
            return new List<DateTime>() { startDateTime, endDateTime };
        }

        public bool IsInCurrentSeason(int boxContractId)
        {
            BoxContract boxContract = _DBContext.BoxContracts.FirstOrDefault(bC => bC.Id == boxContractId);
            if (boxContract == null)
            {
                return false;
            }
            List<DateTime> dates = ComputeSeasonStartAndEnd(boxContract.Years, boxContract.Seasons);
            DateTime currentDate = DateTime.Now;
            return currentDate >= dates[0] && currentDate <= dates[1];
        }

        public List<BoxContract> GetCurrentBoxContractsForUser(int userAccountId)
        {
            return _DBContext.BoxContracts
                    .GroupJoin(
                         _DBContext.OrderItems
                            .Join(_DBContext.Orders,
                                    oi => oi.OrderId, // join criteria
                                    o => o.Id, // join criteria
                    (oi, o) => new { oi.BoxContractId, o.UserAccountId }) // fields i want to keep
                .Where(o => o.UserAccountId == userAccountId), // Keep only for my useraccount
                        bc => bc.Id, // criteria for the join from boxcontracts
                        ooi => ooi.BoxContractId, // criteria from the previous join
            (bc, ooi) => bc) // keep box contracts
                    .ToList();
        }

        public List<BoxContract> GetCurrentBoxContractsForUser(string userAccountIdStr)
        {
            int id;
            if(int.TryParse(userAccountIdStr, out id))
            {
                GetCurrentBoxContractsForUser(userAccountIdStr);
            }
            return null;
        }
    }
}
