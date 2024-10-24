using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgriNov.Models
{
    public interface IServiceOrder : IDisposable
    {
        void InitializeTable();
        void SaveShoppingCartAsAnOrder(int shoppingCartId, Payment payment);
        void SaveShoppingCartAsAnOrder(string shoppingCartIdStr, Payment payment);
        List<Order> GetAllOrdersWithoutDetails();
        List<Order> GetAllOrders();
        List<Order> GetAllOrdersForUserAccount(int userAccountId);
        List<Order> GetAllOrdersForUserAccount(string userAccountIdStr);
        void UpdateOrder(Order order);
        void Save();
    }
}