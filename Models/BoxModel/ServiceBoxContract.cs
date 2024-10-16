using AgriNov.Models;

namespace AgriNov.Models
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

			boxC1.Name = "Contrat LVHS";
			boxC1.ContentDescription = "Ce contrat comprend la livraison hebdomadaire de paniers contenant 3kg de légumes de saison et 1kg de viande.";
			boxC1.Price = 13M;

			UpdateBoxContract(boxC1);

			DeleteBoxContract(boxC3.Id);
		}

		public void InsertBoxContract(BoxContract boxContract)
		{
			_DBContext.BoxContracts.Add(boxContract);
			Save();
		}

		public void UpdateBoxContract(BoxContract boxContract)
		{
			BoxContract oldBoxContract = this.GetBoxContractByID(boxContract.Id);
			if (oldBoxContract != null)
			{
				_DBContext.Entry(oldBoxContract).CurrentValues.SetValues(boxContract);
				Save();
			}
			Save();
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
