using AgriNov.Models;
using AgriNov.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace AgriNov.Controllers
{
    public class OrderController : Controller
    {
        private readonly ServiceOrder sO = new ServiceOrder();
        private List<SelectListItem> GetEnumSelectListString<T>() where T : Enum
        {
            return Enum.GetValues(typeof(T))
                .Cast<T>()
                .Select(e => new SelectListItem
                {
                    Value = ((int)(object)e).ToString(),
                    Text = e.GetDisplayName()
                }).ToList();
        }

        [HttpGet]
        public IActionResult ShowAllOrders(OrderViewModel viewModel)
        {
            using (ServiceOrder sO = new ServiceOrder())
            {
                viewModel.AllOrders = sO.GetAllOrdersWithoutDetails() ?? new List<Order>();
            }

            viewModel.PaymentOptions = GetEnumSelectListString<PaymentType>() ?? new List<SelectListItem>();

            viewModel.ReceivedOptions = new List<SelectListItem>
            {
                new SelectListItem { Text = "Payé", Value = "true" },
                new SelectListItem { Text = "Non payé", Value = "false" }
            };

            viewModel.WasDeliveredOptions = new List<SelectListItem>
            {
                new SelectListItem { Text = "Livré", Value = "true" },
                new SelectListItem { Text = "En cours", Value = "false" }
            };
            return View(viewModel);
        }

        [HttpGet]
        public IActionResult UpdateOrderValidation(int id)
        {
            if (id <= 0)
            {
                return NotFound();
            }

            var oldOrder = sO.GetOrderById(id);

            if (oldOrder == null)
            {
                return NotFound();
            }

            OrderViewModel viewModel = new OrderViewModel
            {
                Order = oldOrder,
                PaymentOptions = GetEnumSelectListString<PaymentType>(),
                
            };
            viewModel.ReceivedOptions = new List<SelectListItem>
            {
                new SelectListItem { Text = "Payé", Value = "true" },
                new SelectListItem { Text = "Non payé", Value = "false" }
            };

            viewModel.WasDeliveredOptions = new List<SelectListItem>
            {
                new SelectListItem { Text = "Livré", Value = "true" },
                new SelectListItem { Text = "En cours", Value = "false" }
            };

            return View(viewModel);
        }

        [HttpPost]
        public IActionResult UpdateOrderValidation(int id, OrderViewModel viewModel)
        {
            if (viewModel == null || viewModel.Order == null)
            {
                return BadRequest("Données de commande invalides.");
            }

            var orderToUpdate = sO.GetOrderById(id);
            if (orderToUpdate == null)
            {
                return NotFound();
            }

            orderToUpdate.WasDelivered = viewModel.Order.WasDelivered;
            if (viewModel.Order.Payment.Received)
            {
                orderToUpdate.Payment.Date = DateTime.Now;
            }
            else
            {
                orderToUpdate.Payment.Date = DateTime.MinValue;
            }
            orderToUpdate.Payment.PaymentType = viewModel.Order.Payment.PaymentType;
            orderToUpdate.Payment.Received = viewModel.Order.Payment.Received;
            orderToUpdate.DateLastModified = DateTime.Now;

            sO.UpdateOrder(orderToUpdate);

            return RedirectToAction("ShowAllOrders", "Order");
        }


        public IActionResult ShowMyOrders()
        {

            OrderViewModel viewModel = new OrderViewModel();
            int userId = int.Parse(HttpContext.User.Identity.Name);

            using (ServiceOrder sO = new ServiceOrder())
            {
                List<Order> userOrders = sO.GetAllOrdersForUserAccount(userId);
                viewModel.AllOrders = userOrders;
            }
            return View(viewModel);
        }

        public IActionResult OrderDetails()
        {
            return View();
        }
    }
}
