namespace AgriNov.Models
{
	public interface IServiceBoxContract : IDisposable
	{
        void CreateDeleteDatabase();
        void InitializeTable();
        List<BoxContract> GetAllBoxContracts();
        BoxContract GetBoxContract(int id);
        void InsertBoxContract(BoxContract boxContract);
        void UpdateBoxContract(BoxContract boxContract);
        void DeleteBoxContracty(int id);
        void Save();
    }
}
