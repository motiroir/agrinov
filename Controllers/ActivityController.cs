using AgriNov.Models;
using AgriNov.Models.ActivityModel;
using AgriNov.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace AgriNov.Controllers
{
    public class ActivityController : Controller
    {


        public IActionResult Index()
        {
            return View();
        }

        public IActionResult CreateActivity(ActivityViewModel aVM)
        {
            return View(aVM);
        }

        [HttpPost]
        public IActionResult CreateActivity(Activity activity)
        {
            if(ModelState.IsValid)
            {
                using(ServiceActivity sA = new ServiceActivity()) 
                {
                    activity.OrganizerId = Int32.Parse(HttpContext.User.Identity.Name);
                    sA.InsertActivity(activity);
                    return RedirectToAction("ActivityDashboard", "Activity", new { activeTab = "ShowMyActivities" });
                }
            }
            ActivityViewModel aVM = new ActivityViewModel();
            //update the activity info to aVM that will be sent back to ActivityDashboard so that errors will be displayed
            aVM.Activity = activity;  
            ViewData["ActiveTab"] = "CreateActivity";
            return View("ActivityDashboard", aVM);  
        

        }

        public IActionResult UpdateActivity(int id)
        {
            if (id > 0)
            {
                using (ServiceActivity sA = new ServiceActivity())
                {
                    Activity oldActivity = sA.GetActivity(id);
                    if (oldActivity != null)
                    {
                        return View(oldActivity);
                    }
                }
            }
            return View("Error");
        }

        [HttpPost]
        public IActionResult UpdateActivity(Activity activity)
        {
            if (ModelState.IsValid)
            {
                if (activity.Id > 0)
                {
                    using (ServiceActivity sA = new ServiceActivity())
                    {
                        activity.OrganizerId = Int32.Parse(HttpContext.User.Identity.Name);
                        sA.UpdateActivity(activity);
                        return RedirectToAction("ActivityDashboard", "Activity", new { activeTab = "ShowMyActivities" });
                    }
                }
            }
            return View(activity);
        }

        public IActionResult ActivityDashboard(string activeTab = "ShowAllActivities")
        {
            ActivityViewModel aVM = new ActivityViewModel();
            int userId = int.Parse(HttpContext.User.Identity.Name);

            using (ServiceActivity sA = new ServiceActivity())
            {
                // getting all activities in viewmodel
                aVM.AllActivities = sA.GetAllActivities();

                //get a list of activities organized by user
                aVM.ActivitiesByOrganizer = sA.GetActivitiesByOrganizer(userId);
                //get a list of activities booked by user
                aVM.ActivitiesBookedByUser = sA.GetActivitiesByUserBooking(userId);
            }

            ViewData["ActiveTab"]=activeTab;
            return View(aVM);
        }

        public IActionResult ShowAllActivities(ActivityViewModel aVM)
        {
             return View(aVM);
        }

        public IActionResult ShowMyActivities(ActivityViewModel aVM)
        {
            return View(aVM);
        }

        public IActionResult ShowActivityDetails(int id, string returnUrl = null)
        {
            ActivityViewModel aVM = new ActivityViewModel();
            //getting the activity info + organizer name
            using (ServiceActivity sA = new ServiceActivity())
            {
                Activity activity = sA.GetActivity(id);
                aVM.Activity = activity;

                using (ServiceUserAccount sUA = new ServiceUserAccount())
                {
                    UserAccount organizer = sUA.GetUserAccountByIDEager(activity.OrganizerId);
                    aVM.OrganizerName = sUA.GetUserFullName(organizer);
                }
            }
            // getting number booking left
            using(ServiceBooking sB = new ServiceBooking())
            {
                aVM.NbBookingsLeft = sB.NbBookingsLeft(id);
            }

            Console.WriteLine(returnUrl);
            return View(aVM);
        }

        [HttpPost]
        public IActionResult BookActivity(Activity activity)
        {
            int userId = int.Parse(HttpContext.User.Identity.Name);
            using (ServiceBooking sB = new ServiceBooking())
            {
                if (!sB.CanUserBookActivity(userId, activity.Id))
                {
                    //put a message to indicate that actvity's already been booked by user ? 
                }
                if (!sB.IsActivityFull(activity.Id))
                {
                    //put a message to indicate that activity's already full ? Maybe juste make the button unclickable ? 
                }

                sB.InsertBooking(userId, activity.Id);
                //show message success on booking ! + redirect to my bookings
            }
            return RedirectToAction("ActivityDashboard", "Activity", new { activeTab = "ShowMyActivities" });
        }


    }
}
