using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
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
            using (IServiceUserAccount serviceUserAccount = new ServiceUserAccount())
            {
                // Supplier s5
                Address a5 = new Address() { Line1 = "Au bout du chemin", Line2 = "Rue des blés", City = "Clissons", PostCode = "44190" };
                ContactDetails c5 = new ContactDetails() { Name = "Martin", FirstName = "Lefevre", PhoneNumber = "0677413231" };
                CompanyDetails cd5 = new CompanyDetails() { CompanyName = "La ferme des blés", SiretNumber = "01876543232411" };
                Supplier s5 = new Supplier() { UserAccount = serviceUserAccount.GetUserAccountByID(5), Address = a5, ContactDetails = c5, CompanyDetails = cd5 };
                InsertSupplier(s5);
                // Supplier s6
                Address a6 = new Address()
                {
                    Line1 = "Clos de la Vigne",
                    Line2 = "12 rue des Vignerons",
                    City = "Nantes",
                    PostCode = "44000"
                };
                ContactDetails c6 = new ContactDetails()
                {
                    Name = "Dubois",
                    FirstName = "Emeline",
                    PhoneNumber = "0645236789"
                };
                CompanyDetails cd6 = new CompanyDetails()
                {
                    CompanyName = "Fruits du Pays",
                    SiretNumber = "1234567890123489"
                };
                Supplier s6 = new Supplier()
                {
                    UserAccount = serviceUserAccount.GetUserAccountByID(6),
                    Address = a6,
                    ContactDetails = c6,
                    CompanyDetails = cd6
                };
                InsertSupplier(s6);

                // Supplier s7
                Address a7 = new Address()
                {
                    Line1 = "Les Champs Dorés",
                    Line2 = "8 avenue des Champs",
                    City = "Angers",
                    PostCode = "49000"
                };
                ContactDetails c7 = new ContactDetails()
                {
                    Name = "Moreau",
                    FirstName = "Claire",
                    PhoneNumber = "0654321987"
                };
                CompanyDetails cd7 = new CompanyDetails()
                {
                    CompanyName = "Les Récoltes de Claire",
                    SiretNumber = "9876543210987645"
                };
                Supplier s7 = new Supplier()
                {
                    UserAccount = serviceUserAccount.GetUserAccountByID(7),
                    Address = a7,
                    ContactDetails = c7,
                    CompanyDetails = cd7
                };
                InsertSupplier(s7);

                // Supplier s8
                Address a8 = new Address()
                {
                    Line1 = "Ferme du Moulin",
                    Line2 = "10 chemin des Moulins",
                    City = "Sautron",
                    PostCode = "44880"
                };
                ContactDetails c8 = new ContactDetails()
                {
                    Name = "Lambert",
                    FirstName = "Simone-Gertrude",
                    PhoneNumber = "0666541234"
                };
                CompanyDetails cd8 = new CompanyDetails()
                {
                    CompanyName = "Carottes Bio",
                    SiretNumber = "8765432109876577"
                };
                Supplier s8 = new Supplier()
                {
                    UserAccount = serviceUserAccount.GetUserAccountByID(8),
                    Address = a8,
                    ContactDetails = c8,
                    CompanyDetails = cd8
                };
                InsertSupplier(s8);

            }

        }

        public void InsertSupplier(Supplier supplier)
        {
            supplier.UserAccount.DateLastModified = DateTime.Now;
            supplier.UserAccount.UserAccountLevel = UserAccountLevel.SUPPLIER;
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
            supplier.UserAccount.DateLastModified = DateTime.Now;
            _DBContext.UserAccounts.Update(supplier.UserAccount);
            _DBContext.Suppliers.Update(supplier);
            Save();
        }
    }
}