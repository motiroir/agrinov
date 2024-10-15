using AgriNov.Models.SharedStatus;

namespace AgriNov.Models.BoxModel
{
	public interface IServiceBoxContract : IDisposable
	{
		void CreateDeleteDatabase();
		void InitializeTable();
		List<BoxContract> GetBoxContracts();
		BoxContract GetBoxContractByID(int boxContractID);
		BoxContract GetBoxContractByID(string boxContractIDStr);
		void InsertBoxContract(BoxContract boxContract);
		void UpdateBoxContract(int id, string name, string contentDescription, Decimal price, DeliveryFrequency deliveryFrequency);
		void DeleteBoxContract(int boxContractID);
		void Save();
	}
}
