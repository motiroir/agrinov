using AgriNov.Models.ProductionModel;

namespace AgriNov.Models
{
	public interface IServiceBoxSubscription : IDisposable
	{
		void CreateDeleteDatabase();
        //void InitializeTable();

		//bool CheckStockAvailability(string boxSize, ICollection<ProductType> productTypes, int seasonId, DeliveryFrequency frequency);

  //      List<BoxContract> GetBoxContracts();
		//BoxContract GetBoxContractByID(int boxContractID);
		//BoxContract GetBoxContractByID(string boxContractIDStr);
		//void InsertBoxContract(BoxContract boxContract);
		//void UpdateBoxContract(BoxContract boxContract);
		//void DeleteBoxContract(int boxContractID);
		void Save();
	}
}
