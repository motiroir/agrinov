using AgriNov.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AgriNov.ViewModels
{
    public class OrderViewModel
    {
        public Order Order { get; set; }
        public List<SelectListItem> PaymentOptions { get; set; } = new List<SelectListItem>();
        public List<SelectListItem> WasDeliveredOptions { get; set; } = new List<SelectListItem>();
        public List<SelectListItem> ReceivedOptions { get; set; } = new List<SelectListItem>();
        public List<Order> AllOrders { get; set; }

    }
}
