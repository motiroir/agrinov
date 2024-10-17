using AgriNov.Models;
using AgriNov.Models.ActivityModel;
using AgriNov.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace AgriNov.Controllers
{
    public class ActivityController : Controller
    {


        public IActionResult Index()
        {
            return View();
        }

        public IActionResult CreateActivity()
        {
            return View();
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
                    return RedirectToAction("ShowAllActivities", "Activity");
                }
            }
            return View();
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
                        sA.UpdateActivity(activity);
                        return RedirectToAction("ShowAllActivities", "Activity");
                    }
                }
            }
            return View(activity);
        }

        public IActionResult ShowAllActivities()
        {
            using (ServiceActivity sA = new ServiceActivity())
            {
                List<Activity> activities = sA.GetAllActivities();
                return View(activities);
            }
        }

        public IActionResult ShowActivityDetails(int id)
        {
            using (ServiceActivity sA = new ServiceActivity())
            {
                Activity activity = sA.GetActivity(id);
                ActivityViewModel aVM = new ActivityViewModel();
                aVM.Activity = activity;

                using (ServiceUserAccount sUA = new ServiceUserAccount())
                {
                    UserAccount organizer = sUA.GetUserAccountByIDEager(activity.OrganizerId);
                    aVM.OrganizerName = sUA.GetUserFullName(organizer);
                }
                return View(aVM);
            }
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
            return RedirectToAction("ShowAllActivities", "Activity");
        }


    }
}
