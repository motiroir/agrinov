namespace AgriNov.Models
{
    public interface IServiceVolunteer : IDisposable
    {
        void CreateDeleteDatabase();
        void InitializeTable();
        List<Volunteer> GetVolunteers();
        User GetVolunteerByID(int volunteerID);
        User GetVolunteerByID(string volunteerIDStr);
        void InsertVolunteer(Volunteer volunteer);
        void UpdateVolunteer(Volunteer volunteer);
        void DeleteVolunteer(int volunteerID);
        void Save();
    }
    
}
