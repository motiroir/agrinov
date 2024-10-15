
using AgriNov.Models.SharedStatus;
using Microsoft.EntityFrameworkCore;
using System.Xml.Linq;

namespace AgriNov.Models.BoxModel
{
	public class ServiceBoxContract : IServiceBoxContract
	{
		private BDDContext _DBContext;

		public ServiceBoxContract()
		{
			_DBContext = new BDDContext();
		}
		public void CreateDeleteDatabase()
		{
			_DBContext.Database.EnsureDeleted();
			_DBContext.Database.EnsureCreated();
		}

		public void DeleteBoxContract(int boxContractID)
		{
			BoxContract boxContract = _DBContext.BoxContracts.Find(boxContractID);
			if (boxContract != null)
			{
				_DBContext.BoxContracts.Remove(boxContract);
				Save();
			}
			
		}

		public BoxContract GetBoxContractByID(int boxContractID)
		{
			return this._DBContext.BoxContracts.Find(boxContractID);
		}

		public BoxContract GetBoxContractByID(string boxContractIDStr)
		{
			int id;
			if (int.TryParse(boxContractIDStr, out id))
			{
				return this.GetBoxContractByID(id);
			}
			return null;
		}

		public List<BoxContract> GetBoxContracts()
		{
			return _DBContext.BoxContracts.ToList();
		}

		public void InitializeTable()
		{
			BoxContract boxC1 = new BoxContract() {
				Name = "Contrat LHS",
				ContentDescription = "Ce contrat comprend la livraison hebdomadaire de paniers contenant 3kg de légumes de saison." ,
				Price = 10M,
				DeliveryFrequency = DeliveryFrequency.WEEKLY
				};
			InsertBoxContract(boxC1);
			
			BoxContract boxC2 = new BoxContract() {
				Name = "Contrat LMM",
				ContentDescription = "Ce contrat comprend la livraison mensuelle de paniers contenant 5kg de légumes de saison.",
				Price = 13M,
				DeliveryFrequency = DeliveryFrequency.BIWEEKLY
				};
			InsertBoxContract(boxC2);
			
			BoxContract boxC3 = new BoxContract() {
				Name = "Contrat LBHL",
				ContentDescription = "Ce contrat comprend la livraison bihebdomadaire de paniers contenant 7kg de légumes de saison.",
				Price = 16M,
				DeliveryFrequency = DeliveryFrequency.MONTHLY
				};
			InsertBoxContract(boxC3);

			UpdateBoxContract(0,"Contrat LVBHL", "Ce contrat comprend la livraison hebdomadaire de paniers contenant 7kg de légumes de saison et de viande.",18M, DeliveryFrequency.WEEKLY);
		}

		public void InsertBoxContract(BoxContract boxContract)
		{
			//boxContract.Name = boxContract.Name;
			//boxContract.ContentDescription = boxContract.ContentDescription;
			//boxContract.Price = boxContract.Price;
			//boxContract.DeliveryFrequency = boxContract.DeliveryFrequency;
			_DBContext.BoxContracts.Add(boxContract);
			Save();
		}

		public void UpdateBoxContract(int id, string name, string contentDescription, Decimal price, DeliveryFrequency deliveryFrequency)
		{
			BoxContract boxContract = this.GetBoxContractByID(id);
			if (boxContract != null)
			{
				boxContract.Name = name;
				boxContract.ContentDescription = contentDescription;
				boxContract.Price = price;
				boxContract.DeliveryFrequency = deliveryFrequency;
				_DBContext.Entry(boxContract).State = EntityState.Modified;
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
