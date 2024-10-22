using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgriNov.Models
{
    public interface IServiceOrder : IDisposable
    {
        void InitializeTable();
        void SaveShoppingCartAsAnOrder(ShoppingCart shoppingCart);
        void UpdateOrder(Order order);
        void Save();
    }
}