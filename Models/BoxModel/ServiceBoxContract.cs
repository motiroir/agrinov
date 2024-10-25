

using System.Data.Entity;
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
            string dirPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "img", "productions");
            BoxContract bc1 = new BoxContract() { ProductType = ProductType.VEGETABLES, Seasons = Seasons.SPRING, Years = Years._2024, Price = 8M, MaxSubscriptions = 20, ForSale = false, ImgSmallBoxContract = File.ReadAllBytes(Path.Combine(dirPath, "vegetablessmall.jpg")), ImgBigBoxContract = File.ReadAllBytes(Path.Combine(dirPath, "vegetablesbig.jpg")) };
            BoxContract bc2 = new BoxContract() { ProductType = ProductType.DAYRIS, Seasons = Seasons.SPRING, Years = Years._2024, Price = 5M, MaxSubscriptions = 12, ForSale = false, ImgSmallBoxContract = File.ReadAllBytes(Path.Combine(dirPath, "dairysmall.jpg")), ImgBigBoxContract = File.ReadAllBytes(Path.Combine(dirPath, "dairybig.jpg")) };
            BoxContract bc3 = new BoxContract() { ProductType = ProductType.MEAT, Seasons = Seasons.SPRING, Years = Years._2024, Price = 16M, MaxSubscriptions = 10, ForSale = false, ImgSmallBoxContract = File.ReadAllBytes(Path.Combine(dirPath, "meatsmall.jpg")), ImgBigBoxContract = File.ReadAllBytes(Path.Combine(dirPath, "meatbig.jpg")) };
            BoxContract bc4 = new BoxContract() { ProductType = ProductType.FRUITS, Seasons = Seasons.SPRING, Years = Years._2024, Price = 12M, MaxSubscriptions = 15, ForSale = false, ImgSmallBoxContract = File.ReadAllBytes(Path.Combine(dirPath, "fruitsmall.jpg")), ImgBigBoxContract = File.ReadAllBytes(Path.Combine(dirPath, "fruitbig.jpg")) };
            BoxContract bc5 = new BoxContract() { ProductType = ProductType.EGGS, Seasons = Seasons.SPRING, Years = Years._2024, Price = 2M, MaxSubscriptions = 12, ForSale = false, ImgSmallBoxContract = File.ReadAllBytes(Path.Combine(dirPath, "eggssmall.jpg")), ImgBigBoxContract = File.ReadAllBytes(Path.Combine(dirPath, "eggsbig.jpg")) };

            BoxContract bc6 = new BoxContract() { ProductType = ProductType.VEGETABLES, Seasons = Seasons.AUTUMN, Years = Years._2024, Price = 8M, MaxSubscriptions = 25, ForSale = false, ImgSmallBoxContract = File.ReadAllBytes(Path.Combine(dirPath, "vegetablessmall.jpg")), ImgBigBoxContract = File.ReadAllBytes(Path.Combine(dirPath, "vegetablesbig.jpg")) };
            BoxContract bc7 = new BoxContract() { ProductType = ProductType.DAYRIS, Seasons = Seasons.AUTUMN, Years = Years._2024, Price = 5M, MaxSubscriptions = 10, ForSale = false, ImgSmallBoxContract = File.ReadAllBytes(Path.Combine(dirPath, "dairysmall.jpg")), ImgBigBoxContract = File.ReadAllBytes(Path.Combine(dirPath, "dairybig.jpg")) };
            BoxContract bc8 = new BoxContract() { ProductType = ProductType.MEAT, Seasons = Seasons.AUTUMN, Years = Years._2024, Price = 16M, MaxSubscriptions = 10, ForSale = false, ImgSmallBoxContract = File.ReadAllBytes(Path.Combine(dirPath, "meatsmall.jpg")), ImgBigBoxContract = File.ReadAllBytes(Path.Combine(dirPath, "meatbig.jpg")) };
            BoxContract bc9 = new BoxContract() { ProductType = ProductType.FRUITS, Seasons = Seasons.AUTUMN, Years = Years._2024, Price = 12M, MaxSubscriptions = 10, ForSale = false, ImgSmallBoxContract = File.ReadAllBytes(Path.Combine(dirPath, "fruitsmall.jpg")), ImgBigBoxContract = File.ReadAllBytes(Path.Combine(dirPath, "fruitbig.jpg")) };
            BoxContract bc10 = new BoxContract() { ProductType = ProductType.EGGS, Seasons = Seasons.AUTUMN, Years = Years._2024, Price = 2M, MaxSubscriptions = 12, ForSale = false, ImgSmallBoxContract = File.ReadAllBytes(Path.Combine(dirPath, "eggssmall.jpg")), ImgBigBoxContract = File.ReadAllBytes(Path.Combine(dirPath, "eggsbig.jpg")) };

            BoxContract bc11 = new BoxContract() { ProductType = ProductType.VEGETABLES, Seasons = Seasons.AUTUMN, Years = Years._2024, Price = 8M, MaxSubscriptions = 20, ForSale = false, ImgSmallBoxContract = File.ReadAllBytes(Path.Combine(dirPath, "vegetablessmall.jpg")), ImgBigBoxContract = File.ReadAllBytes(Path.Combine(dirPath, "vegetablesbig.jpg")) };
            BoxContract bc12 = new BoxContract() { ProductType = ProductType.DAYRIS, Seasons = Seasons.AUTUMN, Years = Years._2024, Price = 5M, MaxSubscriptions = 12, ForSale = false, ImgSmallBoxContract = File.ReadAllBytes(Path.Combine(dirPath, "dairysmall.jpg")), ImgBigBoxContract = File.ReadAllBytes(Path.Combine(dirPath, "dairybig.jpg")) };
            BoxContract bc13 = new BoxContract() { ProductType = ProductType.MEAT, Seasons = Seasons.AUTUMN, Years = Years._2024, Price = 16M, MaxSubscriptions = 12, ForSale = false, ImgSmallBoxContract = File.ReadAllBytes(Path.Combine(dirPath, "meatsmall.jpg")), ImgBigBoxContract = File.ReadAllBytes(Path.Combine(dirPath, "meatbig.jpg")) };
            BoxContract bc14 = new BoxContract() { ProductType = ProductType.FRUITS, Seasons = Seasons.AUTUMN, Years = Years._2024, Price = 12M, MaxSubscriptions = 10, ForSale = false, ImgSmallBoxContract = File.ReadAllBytes(Path.Combine(dirPath, "fruitsmall.jpg")), ImgBigBoxContract = File.ReadAllBytes(Path.Combine(dirPath, "fruitbig.jpg")) };
            BoxContract bc15 = new BoxContract() { ProductType = ProductType.EGGS, Seasons = Seasons.AUTUMN, Years = Years._2024, Price = 2M, MaxSubscriptions = 12, ForSale = false, ImgSmallBoxContract = File.ReadAllBytes(Path.Combine(dirPath, "eggssmall.jpg")), ImgBigBoxContract = File.ReadAllBytes(Path.Combine(dirPath, "eggsbig.jpg")) };
            
            BoxContract bc16 = new BoxContract() { ProductType = ProductType.VEGETABLES, Seasons = Seasons.WINTER, Years = Years._2024, Price = 8M, MaxSubscriptions = 12, ForSale = true, ImgSmallBoxContract = File.ReadAllBytes(Path.Combine(dirPath, "vegetablessmall.jpg")), ImgBigBoxContract = File.ReadAllBytes(Path.Combine(dirPath, "vegetablesbig.jpg")) };
            BoxContract bc17 = new BoxContract() { ProductType = ProductType.FRUITS, Seasons = Seasons.WINTER, Years = Years._2024, Price = 5M, MaxSubscriptions = 12, ForSale = true, ImgSmallBoxContract = File.ReadAllBytes(Path.Combine(dirPath, "fruitsmall.jpg")), ImgBigBoxContract = File.ReadAllBytes(Path.Combine(dirPath, "fruitbig.jpg")) };
            BoxContract bc18 = new BoxContract() { ProductType = ProductType.MEAT, Seasons = Seasons.WINTER, Years = Years._2024, Price = 16M, MaxSubscriptions = 12, ForSale = true, ImgSmallBoxContract = File.ReadAllBytes(Path.Combine(dirPath, "meatsmall.jpg")), ImgBigBoxContract = File.ReadAllBytes(Path.Combine(dirPath, "meatbig.jpg")) };
            BoxContract bc19 = new BoxContract() { ProductType = ProductType.DAYRIS, Seasons = Seasons.WINTER, Years = Years._2024, Price = 12M, MaxSubscriptions = 10, ForSale = true, ImgSmallBoxContract = File.ReadAllBytes(Path.Combine(dirPath, "dairysmall.jpg")), ImgBigBoxContract = File.ReadAllBytes(Path.Combine(dirPath, "dairybig.jpg")) };
            BoxContract bc20 = new BoxContract() { ProductType = ProductType.EGGS, Seasons = Seasons.WINTER, Years = Years._2024, Price = 2M, MaxSubscriptions = 12, ForSale = true, ImgSmallBoxContract = File.ReadAllBytes(Path.Combine(dirPath, "eggssmall.jpg")), ImgBigBoxContract = File.ReadAllBytes(Path.Combine(dirPath, "eggsbig.jpg")) };
            
            BoxContract bc21 = new BoxContract() { ProductType = ProductType.FRUITS, Seasons = Seasons.SPRING, Years = Years._2025, Price = 5M, MaxSubscriptions = 12, ForSale = false, ImgSmallBoxContract = File.ReadAllBytes(Path.Combine(dirPath, "fruitsmall.jpg")), ImgBigBoxContract = File.ReadAllBytes(Path.Combine(dirPath, "fruitbig.jpg")) };
            BoxContract bc22 = new BoxContract() { ProductType = ProductType.MEAT, Seasons = Seasons.SPRING, Years = Years._2025, Price = 16M, MaxSubscriptions = 12, ForSale = false, ImgSmallBoxContract = File.ReadAllBytes(Path.Combine(dirPath, "meatsmall.jpg")), ImgBigBoxContract = File.ReadAllBytes(Path.Combine(dirPath, "meatbig.jpg")) };
            BoxContract bc23 = new BoxContract() { ProductType = ProductType.DAYRIS, Seasons = Seasons.SPRING, Years = Years._2025, Price = 12M, MaxSubscriptions = 10, ForSale = false, ImgSmallBoxContract = File.ReadAllBytes(Path.Combine(dirPath, "dairysmall.jpg")), ImgBigBoxContract = File.ReadAllBytes(Path.Combine(dirPath, "dairybig.jpg")) };
            BoxContract bc24 = new BoxContract() { ProductType = ProductType.EGGS, Seasons = Seasons.SPRING, Years = Years._2025, Price = 2M, MaxSubscriptions = 12, ForSale = false, ImgSmallBoxContract = File.ReadAllBytes(Path.Combine(dirPath, "eggssmall.jpg")), ImgBigBoxContract = File.ReadAllBytes(Path.Combine(dirPath, "eggsbig.jpg")) };
        
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
            InsertBoxContract(bc16);
            InsertBoxContract(bc17);
            InsertBoxContract(bc18);
            InsertBoxContract(bc19);
            InsertBoxContract(bc20);
            InsertBoxContract(bc21);
            InsertBoxContract(bc22);
            InsertBoxContract(bc23);
            InsertBoxContract(bc24);
        }

        public List<BoxContract> GetAllBoxContracts()
        {
            return _DBContext.BoxContracts.ToList();
        }
        public List<BoxContract> GetAllBoxContractsForSale()
        {
            return _DBContext.BoxContracts.Where(bc => bc.ForSale == true && bc.MaxSubscriptions > 0).ToList();
        }

        public BoxContract GetBoxContractById(int id)
        {
            return this._DBContext.BoxContracts.Find(id);
        }

        public void InsertBoxContract(BoxContract boxContract)
        {
            if (boxContract.ProductType.ToString() == "VEGETABLES")
            {
                //add images by default on vegetables
                string dirPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "img", "productions");
                boxContract.ImgSmallBoxContract = File.ReadAllBytes(Path.Combine(dirPath, "vegetablessmall.jpg"));
                boxContract.ImgBigBoxContract = File.ReadAllBytes(Path.Combine(dirPath, "vegetablesbig.jpg"));
            }
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
            if (boxContract == null || boxContract.Seasons == Seasons.None)
            {
                return false;
            }
            List<DateTime> dates = ComputeSeasonStartAndEnd(boxContract.Years, boxContract.Seasons);
            DateTime currentDate = DateTime.Now;
            return currentDate >= dates[0] && currentDate <= dates[1];
        }

        public List<BoxContractSubscription> GetCurrentBoxContractsForUser(int userAccountId)
        {
            List<BoxContractSubscription> contracts =  _DBContext.OrderItems.Include(oi => oi.Order)
                                        .Where(oi => oi.Order.UserAccountId == userAccountId && oi.BoxContract != null)
                                        .Include(oi => oi.BoxContract)
                                        .Select(oi => new BoxContractSubscription() {BoxContract = oi.BoxContract , Quantity = oi.Quantity})
                                        .Distinct()
                                        .ToList();
            return contracts.Where(bcs => IsInCurrentSeason(bcs.BoxContract.Id)).ToList();
        }

        public List<BoxContractSubscription> GetCurrentBoxContractsForUser(string userAccountIdStr)
        {
            int id;
            if(int.TryParse(userAccountIdStr, out id))
            {
                GetCurrentBoxContractsForUser(userAccountIdStr);
            }
            return null;
        }

        public List<BoxContract> GetAllBoxContractsForSaleNotAlreadySubscribed(int userAccountId)
        {
            List<BoxContract> contractsAlreadySubscribed = _DBContext.OrderItems.Include(oi => oi.Order)
                                        .Where(oi => oi.Order.UserAccountId == userAccountId && oi.BoxContract != null)
                                        .Include(oi => oi.BoxContract)
                                        .Select(oi => oi.BoxContract)
                                        .Distinct()
                                        .ToList();

            return GetAllBoxContractsForSale().Where( bc => !contractsAlreadySubscribed.Contains(bc)).ToList();
        }


    }
}
