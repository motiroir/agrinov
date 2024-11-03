namespace AgriNov.Models
{
    public interface IServiceSupplier : IDisposable
    {
        void CreateDeleteDatabase();
        void InitializeTable();
        List<Supplier> GetSuppliers();
        Supplier GetSupplierByID(int supplierID);
        Supplier GetSupplierByID(string supplierIDStr);
        void InsertSupplier(Supplier supplier);
        void UpdateSupplier(Supplier supplier);
        void DeleteSupplier(int supplierID);
        void Save();
    }
}