using AgriNov.Models;
using AgriNov.Models.ProductionModel;
using System.Diagnostics;

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
            BoxContract bc1 = new BoxContract() { ProductType = ProductType.VEGETABLES,Seasons = Seasons.SPRING, Years=Years._2024, Price= 8M, MaxSubscriptions= 20 };
            BoxContract bc2 = new BoxContract() { ProductType = ProductType.DAYRIS, Seasons = Seasons.SPRING, Years = Years._2024, Price = 5M, MaxSubscriptions = 20 };
            BoxContract bc3 = new BoxContract() { ProductType = ProductType.MEAT, Seasons = Seasons.SPRING, Years = Years._2024, Price = 16M, MaxSubscriptions = 20 };
            BoxContract bc4 = new BoxContract() { ProductType = ProductType.FISH, Seasons = Seasons.SPRING, Years = Years._2024, Price = 12M, MaxSubscriptions = 20 };
            BoxContract bc5 = new BoxContract() { ProductType = ProductType.EGGS, Seasons = Seasons.SPRING, Years = Years._2024, Price = 2M, MaxSubscriptions = 20 };
            
            BoxContract bc6 = new BoxContract() { ProductType = ProductType.VEGETABLES,Seasons = Seasons.AUTUMN, Years=Years._2024, Price= 8M, MaxSubscriptions= 20 };
            BoxContract bc7 = new BoxContract() { ProductType = ProductType.DAYRIS, Seasons = Seasons.AUTUMN, Years = Years._2024, Price = 5M, MaxSubscriptions = 20 };
            BoxContract bc8 = new BoxContract() { ProductType = ProductType.MEAT, Seasons = Seasons.AUTUMN, Years = Years._2024, Price = 16M, MaxSubscriptions = 20 };
            BoxContract bc9 = new BoxContract() { ProductType = ProductType.FISH, Seasons = Seasons.AUTUMN, Years = Years._2024, Price = 12M, MaxSubscriptions = 20 };
            BoxContract bc10 = new BoxContract() { ProductType = ProductType.EGGS, Seasons = Seasons.AUTUMN, Years = Years._2024, Price = 2M, MaxSubscriptions = 20 };

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

        public void DeleteBoxContracty(int id)
        {
            throw new NotImplementedException();
        }
    }
}
