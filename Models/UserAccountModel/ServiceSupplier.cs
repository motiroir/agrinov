using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgriNov.Models
{
    public class ServiceSupplier : IServiceSupplier
    {
        private BDDContext _DBContext;

        public ServiceSupplier()
        {
            _DBContext = new BDDContext();
        }
        public void CreateDeleteDatabase()
        {
            _DBContext.Database.EnsureDeleted();
            _DBContext.Database.EnsureCreated();
        }

        public void DeleteSupplier(int supplierID)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            _DBContext.Dispose();
        }

        public Supplier GetSupplierByID(int supplierID)
        {
            return _DBContext.Suppliers.Find(supplierID);
        }

        public Supplier GetSupplierByID(string supplierIDStr)
        {
            int id;
            if (int.TryParse(supplierIDStr, out id))
            {
                return GetSupplierByID(id);
            }
            return null;
        }

        public List<Supplier> GetSuppliers()
        {
            return _DBContext.Suppliers.ToList();
        }

        public void InitializeTable()
        {
            throw new NotImplementedException();
        }

        public void InsertSupplier(Supplier supplier)
        {
            supplier.UserAccount.DateLastModified = DateTime.Now;
            supplier.UserAccount.UserAccountLevel = UserAccountLevel.USER;
            //_Update and track the user account that is part of user
            _DBContext.UserAccounts.Update(supplier.UserAccount);
            _DBContext.Suppliers.Add(supplier);
            Save();
        }

        public void Save()
        {
            _DBContext.SaveChanges();
        }

        public void UpdateSupplier(Supplier supplier)
        {
            throw new NotImplementedException();
        }
    }
}