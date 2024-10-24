

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
            BoxContract bc1 = new BoxContract() { ProductType = ProductType.VEGETABLES,Seasons = Seasons.SPRING, Years=Years._2024, Price= 8M, MaxSubscriptions= 20, ForSale=false };
            BoxContract bc2 = new BoxContract() { ProductType = ProductType.DAYRIS, Seasons = Seasons.SPRING, Years = Years._2024, Price = 5M, MaxSubscriptions = 20, ForSale = false };
            BoxContract bc3 = new BoxContract() { ProductType = ProductType.MEAT, Seasons = Seasons.SPRING, Years = Years._2024, Price = 16M, MaxSubscriptions = 20, ForSale = false };
            BoxContract bc4 = new BoxContract() { ProductType = ProductType.FISH, Seasons = Seasons.SPRING, Years = Years._2024, Price = 12M, MaxSubscriptions = 20, ForSale = false };
            BoxContract bc5 = new BoxContract() { ProductType = ProductType.EGGS, Seasons = Seasons.SPRING, Years = Years._2024, Price = 2M, MaxSubscriptions = 20, ForSale = false };
            
            BoxContract bc6 = new BoxContract() { ProductType = ProductType.VEGETABLES,Seasons = Seasons.AUTUMN, Years=Years._2024, Price= 8M, MaxSubscriptions= 20, ForSale = true };
            BoxContract bc7 = new BoxContract() { ProductType = ProductType.DAYRIS, Seasons = Seasons.AUTUMN, Years = Years._2024, Price = 5M, MaxSubscriptions = 20, ForSale = true };
            BoxContract bc8 = new BoxContract() { ProductType = ProductType.MEAT, Seasons = Seasons.AUTUMN, Years = Years._2024, Price = 16M, MaxSubscriptions = 20 , ForSale = true };
            BoxContract bc9 = new BoxContract() { ProductType = ProductType.FISH, Seasons = Seasons.AUTUMN, Years = Years._2024, Price = 12M, MaxSubscriptions = 20 , ForSale = true };
            BoxContract bc10 = new BoxContract() { ProductType = ProductType.EGGS, Seasons = Seasons.AUTUMN, Years = Years._2024, Price = 2M, MaxSubscriptions = 20 , ForSale = true };

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
            if(season == Seasons.AUTUMN)
            {
                startDateTime = new DateTime(int.Parse(year.GetDisplayName()),9,21,0,0,0,0);
                endDateTime = new DateTime(int.Parse(year.GetDisplayName()),12,20,23,59,59,999);
            }
            else if(season == Seasons.WINTER)
            {
                startDateTime = new DateTime(int.Parse(year.GetDisplayName()),12,21,0,0,0,0);
                endDateTime = new DateTime(int.Parse(year.GetDisplayName())+1,3,20,23,59,59,999);
            }
            else if(season == Seasons.SPRING)
            {
                startDateTime = new DateTime(int.Parse(year.GetDisplayName()),3,21,0,0,0,0);
                endDateTime = new DateTime(int.Parse(year.GetDisplayName()),6,20,23,59,59,999);
            }
            else // (season == Seasons.SUMMER)
            {
                startDateTime = new DateTime(int.Parse(year.GetDisplayName()),6,21,0,0,0,0);
                endDateTime = new DateTime(int.Parse(year.GetDisplayName()),9,20,23,59,59,999);
            }
            return new List<DateTime>() {startDateTime, endDateTime};
        }

        public boolean IsInCurrentSeason(int boxContractId){
            BoxContract boxContract = _DBContext.BoxContracts.FirstOrDefault(bC => bC.Id == boxContractId);
            if(boxContract == null)
            {
                return false;
            }
            List<DateTime> dates = ComputeSeasonStartAndEnd(boxContract.Years, boxContract.Seasons);
            DateTime currentDate = DateTime.Now;
            
        }
    }
}
