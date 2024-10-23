
namespace AgriNov.Models
{
    public class ServiceVolunteer : IServiceVolunteer
    {
        private BDDContext _DBContext;

        public ServiceVolunteer()
        {
            _DBContext = new BDDContext();
        }

        public void CreateDeleteDatabase()
        {
            _DBContext.Database.EnsureDeleted();
            _DBContext.Database.EnsureCreated();
        }

        public void DeleteVolunteer(int volunteerID)
        {
            // Is cascade delete with user account ok?
            Volunteer volunteer = _DBContext.Volunteers.Find(volunteerID);
            if (volunteer != null)
            {
                this._DBContext.Volunteers.Remove(volunteer);
            }
            this.Save();
        }

        public void Dispose()
        {
            _DBContext.Dispose();
        }
        public List<Volunteer> GetVolunteers()
        {
            return _DBContext.Volunteers.ToList();
        }
        public User GetVolunteerByID(int volunteerID)
        {
            return _DBContext.Users.Find(volunteerID);
        }

        public User GetVolunteerByID(string volunteerIDStr)
        {
            int id;
            if (int.TryParse(volunteerIDStr, out id))
            {
                return GetVolunteerByID(id);
            }
            return null;
        }

        public void InitializeTable()
        {
            using (IServiceUserAccount serviceUserAccount = new ServiceUserAccount())
            {
                // Volunteer v1
                ContactDetails vc1 = new ContactDetails()
                {
                    Name = "BOUTIN",
                    FirstName = "Clara",
                    PhoneNumber = "0671234567"
                };
                Address va1 = new Address()
                {
                    Line1 = "Résidence des Roses",
                    Line2 = "15 boulevard des Jardins",
                    City = "Nantes",
                    PostCode = "44000"
                };
                Volunteer v1 = new Volunteer()
                {
                    UserAccount = serviceUserAccount.GetUserAccountByID(16),
                    ContactDetails = vc1,
                    Address = va1,
                    Availabilities = "Lundi, Mercredi, Vendredi",
                    HoursWorkDone = 12.5
                };
                InsertVolunteer(v1);

                // Volunteer v2
                ContactDetails vc2 = new ContactDetails()
                {
                    Name = "DUPUIS",
                    FirstName = "Maxime",
                    PhoneNumber = "0645671234"
                };
                Address va2 = new Address()
                {
                    Line1 = "Appartement 7B",
                    Line2 = "3 rue du Château",
                    City = "Clisson",
                    PostCode = "44190"
                };
                Volunteer v2 = new Volunteer()
                {
                    UserAccount = serviceUserAccount.GetUserAccountByID(17),
                    ContactDetails = vc2,
                    Address = va2,
                    Availabilities = "Mardi, Jeudi, Samedi",
                    HoursWorkDone = 20.0
                };
                InsertVolunteer(v2);

            }
        }

        public void InsertVolunteer(Volunteer volunteer)
        {
            volunteer.UserAccount.DateLastModified = DateTime.Now;
            volunteer.UserAccount.UserAccountLevel = UserAccountLevel.VOLUNTEER;
            //_Update and track the user account that is part of user
            _DBContext.UserAccounts.Update(volunteer.UserAccount);
            _DBContext.Volunteers.Add(volunteer);
            Save();
        }

        public void Save()
        {
            _DBContext.SaveChanges();
        }

        public void UpdateVolunteer(Volunteer volunteer)
        {
            volunteer.UserAccount.DateLastModified = DateTime.Now;
            _DBContext.UserAccounts.Update(volunteer.UserAccount);
            _DBContext.Volunteers.Update(volunteer);
            Save();
        }
    }
}
