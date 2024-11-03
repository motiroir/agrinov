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
            string dirPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "img", "proofpdfdoc");
            using (IServiceUserAccount serviceUserAccount = new ServiceUserAccount())
            {
                // Supplier s5
                Address a5 = new Address() 
                { 
                    Line1 = "Au bout du chemin", 
                    Line2 = "Rue des blés", 
                    City = "Clissons", PostCode = "44190" 
                };
                ContactDetails c5 = new ContactDetails() 
                { 
                    Name = "Martin", 
                    FirstName = "Lefevre", 
                    PhoneNumber = "0677413231" 
                };
                CompanyDetails cd5 = new CompanyDetails() 
                { 
                    CompanyName = "La ferme des blés", 
                    SiretNumber = "01876543232411" 
                };
                Supplier s5 = new Supplier() 
                { 
                    UserAccount = serviceUserAccount.GetUserAccountByID(5), 
                    Address = a5, ContactDetails = c5, 
                    CompanyDetails = cd5, 
                    ProofPdfDocument = File.ReadAllBytes(Path.Combine(dirPath, "c_moreau-champ_dores-justificatif.pdf")) 
                };
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
                    CompanyDetails = cd6,
                    ProofPdfDocument = File.ReadAllBytes(Path.Combine(dirPath, "c_moreau-champ_dores-justificatif.pdf"))
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
                    CompanyDetails = cd7,
                    ProofPdfDocument = File.ReadAllBytes(Path.Combine(dirPath, "c_moreau-champ_dores-justificatif.pdf"))
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
                    FirstName = "Simone",
                    PhoneNumber = "0666541234"
                };
                CompanyDetails cd8 = new CompanyDetails()
                {
                    CompanyName = "Légumes Bio",
                    SiretNumber = "8765432109876577"
                };
                Supplier s8 = new Supplier()
                {
                    UserAccount = serviceUserAccount.GetUserAccountByID(8),
                    Address = a8,
                    ContactDetails = c8,
                    CompanyDetails = cd8,
                    ProofPdfDocument = File.ReadAllBytes(Path.Combine(dirPath, "c_moreau-champ_dores-justificatif.pdf"))
                };
                InsertSupplier(s8);

                // Supplier s9
                Address a9 = new Address()
                {
                    Line1 = "Ferme des Prés Verts",
                    Line2 = "15 rue des Fleurs",
                    City = "Nantes",
                    PostCode = "44000"
                };
                ContactDetails c9 = new ContactDetails()
                {
                    Name = "Leclerc",
                    FirstName = "Rémi",
                    PhoneNumber = "0778564321"
                };
                CompanyDetails cd9 = new CompanyDetails()
                {
                    CompanyName = "Fruits et Légumes Bio",
                    SiretNumber = "1234567890123456"
                };
                Supplier s9 = new Supplier()
                {
                    UserAccount = serviceUserAccount.GetUserAccountByID(18),
                    Address = a9,
                    ContactDetails = c9,
                    CompanyDetails = cd9,
                    ProofPdfDocument = File.ReadAllBytes(Path.Combine(dirPath, "c_moreau-champ_dores-justificatif.pdf"))
                };
                InsertSupplier(s9);

                // Supplier s10
                Address a10 = new Address()
                {
                    Line1 = "Domaine de la Grande Forêt",
                    Line2 = "25 avenue des Chênes",
                    City = "Clisson",
                    PostCode = "44190"
                };
                ContactDetails c10 = new ContactDetails()
                {
                    Name = "Bernard",
                    FirstName = "Théo",
                    PhoneNumber = "0657894321"
                };
                CompanyDetails cd10 = new CompanyDetails()
                {
                    CompanyName = "Maraîchage Bernard",
                    SiretNumber = "2345678901234567"
                };
                Supplier s10 = new Supplier()
                {
                    UserAccount = serviceUserAccount.GetUserAccountByID(19),
                    Address = a10,
                    ContactDetails = c10,
                    CompanyDetails = cd10,
                    ProofPdfDocument = File.ReadAllBytes(Path.Combine(dirPath, "c_moreau-champ_dores-justificatif.pdf"))
                };
                InsertSupplier(s10);

                // Supplier s11
                Address a11 = new Address()
                {
                    Line1 = "Ferme de la Rivière",
                    Line2 = "30 route des Berges",
                    City = "Vertou",
                    PostCode = "44120"
                };
                ContactDetails c11 = new ContactDetails()
                {
                    Name = "Fontaine",
                    FirstName = "Adrien",
                    PhoneNumber = "0632549876"
                };
                CompanyDetails cd11 = new CompanyDetails()
                {
                    CompanyName = "Les Fermiers de Vertou",
                    SiretNumber = "3456789012345678"
                };
                Supplier s11 = new Supplier()
                {
                    UserAccount = serviceUserAccount.GetUserAccountByID(20),
                    Address = a11,
                    ContactDetails = c11,
                    CompanyDetails = cd11,
                    ProofPdfDocument = File.ReadAllBytes(Path.Combine(dirPath, "c_moreau-champ_dores-justificatif.pdf"))
                };
                InsertSupplier(s11);
            }

        }

        public void InsertSupplier(Supplier supplier)
        {
            supplier.UserAccount.DateLastModified = DateTime.Now;
            supplier.UserAccount.UserAccountLevel = UserAccountLevel.SUPPLIER;
            supplier.ContactDetails.Name = supplier.ContactDetails.Name.ToUpper();
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