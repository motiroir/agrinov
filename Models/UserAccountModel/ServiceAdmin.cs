
namespace AgriNov.Models
{
    public class ServiceAdmin : IServiceAdmin
    {
        private BDDContext _DBContext;

        public ServiceAdmin()
        {
            _DBContext = new BDDContext();
        }

        public void CreateDeleteDatabase()
        {
            _DBContext.Database.EnsureDeleted();
            _DBContext.Database.EnsureCreated();
        }

        public void DeleteAdmin(int adminID)
        {
            //Is cascade delete with useraccount ok?
            Admin admin = _DBContext.Admins.Find(adminID);
            if (admin != null)
            {
                this._DBContext.Admins.Remove(admin);
            }
            this.Save();
        }

        public void Dispose()
        {
            _DBContext.Dispose();
        }
        public List<Admin> GetAdmins()
        {
            return _DBContext.Admins.ToList();
        }
        public User GetAdminByID(int adminID)
        {
            return _DBContext.Users.Find(adminID);
           
        }

        public User GetAdminByID(string adminrIDStr)
        {
            int id;
            if (int.TryParse(adminrIDStr, out id))
            {
                return GetAdminByID(id);
            }
            return null;
        }

        public void InitializeTable()
        {
            using (IServiceUserAccount serviceUserAccount = new ServiceUserAccount())
            {
                // Admin adm1
                ContactDetails c1 = new ContactDetails() 
                { 
                    Name = "CAYLUS", 
                    FirstName = "Quentin", 
                    PhoneNumber = "0675453231" 
                };
                Address a1 = new Address() 
                { 
                    Line1 = "Apt B2", 
                    Line2 = "3 route de Paris", 
                    City = "Clissons", 
                    PostCode = "44190" 
                };
                Admin adm1 = new Admin() 
                { 
                    UserAccount = serviceUserAccount.GetUserAccountByID(1), 
                    ContactDetails = c1, 
                    Address = a1 
                };
                InsertAdmin(adm1);

                // Admin adm2
                ContactDetails c2 = new ContactDetails() 
                { 
                    Name = "COSTABELLO", 
                    FirstName = "Florent ", 
                    PhoneNumber = "0659871234" };
                Address a2 = new Address()
                {
                    Line1 = "Building C",
                    Line2 = "10 rue des Fleurs",
                    City = "Nantes",
                    PostCode = "44000"
                };
                Admin adm2 = new Admin()
                {
                    UserAccount = serviceUserAccount.GetUserAccountByID(2),
                    ContactDetails = c2,
                    Address = a2
                };
                InsertAdmin(adm2);

                // Admin adm3
                ContactDetails c3 = new ContactDetails()
                {
                    Name = "LE BRETON",
                    FirstName = "Pauline",
                    PhoneNumber = "0789124567"
                };
                Address a3 = new Address()
                {
                    Line1 = "Villa Solange",
                    Line2 = "7 avenue de la Mer",
                    City = "La Baule",
                    PostCode = "44500"
                };
                Admin adm3 = new Admin()
                {
                    UserAccount = serviceUserAccount.GetUserAccountByID(3),
                    ContactDetails = c3,
                    Address = a3
                };
                InsertAdmin(adm3);

                // Admin adm4
                ContactDetails c4 = new ContactDetails()
                {
                    Name = "ROY",
                    FirstName = "Alexia",
                    PhoneNumber = "0634567890"
                };
                Address a4 = new Address()
                {
                    Line1 = "Maison Verte",
                    Line2 = "25 chemin des Vignes",
                    City = "Sautron",
                    PostCode = "44880"
                };
                Admin adm4 = new Admin()
                {
                    UserAccount = serviceUserAccount.GetUserAccountByID(4),
                    ContactDetails = c4,
                    Address = a4
                };
                InsertAdmin(adm4);

                // Admin adm5
                ContactDetails c5 = new ContactDetails()
                {
                    Name = "TIROIR",
                    FirstName = "Morgane",
                    PhoneNumber = "0634567890"
                };
                Address a5 = new Address()
                {
                    Line1 = "Maison Verte",
                    Line2 = "25 chemin des Vignes",
                    City = "Sautron",
                    PostCode = "44880"
                };
                Admin adm5 = new Admin()
                {
                    UserAccount = serviceUserAccount.GetUserAccountByID(15),
                    ContactDetails = c5,
                    Address = a5
                };
                InsertAdmin(adm5);
            }
        }

        public void InsertAdmin(Admin admin)
        {
            admin.UserAccount.DateLastModified = DateTime.Now;
            admin.UserAccount.UserAccountLevel = UserAccountLevel.ADMIN;
            //_Update and track the user account that is part of user
            _DBContext.UserAccounts.Update(admin.UserAccount);
            _DBContext.Admins.Add(admin);
            Save();
        }

        public void Save()
        {
            _DBContext.SaveChanges();
        }

        public void UpdateAdmin(Admin admin)
        {
            admin.UserAccount.DateLastModified = DateTime.Now;
            _DBContext.UserAccounts.Update(admin.UserAccount);
            _DBContext.Admins.Update(admin);
            Save();
        }
    }
}
