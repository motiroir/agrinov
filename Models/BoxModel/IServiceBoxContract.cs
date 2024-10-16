namespace AgriNov.Models
{
	public interface IServiceBoxContract : IDisposable
	{
		void CreateDeleteDatabase();
		void InitializeTable();
		List<BoxContract> GetBoxContracts();
		BoxContract GetBoxContractByID(int boxContractID);
		BoxContract GetBoxContractByID(string boxContractIDStr);
		void InsertBoxContract(BoxContract boxContract);
		void UpdateBoxContract(BoxContract boxContract);
		void DeleteBoxContract(int boxContractID);
		void Save();
	}
}
