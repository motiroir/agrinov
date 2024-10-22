using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgriNov.Models
{
    public interface IServiceOrder : IDisposable
    {
        void InitializeTable();
        void SaveShoppingCartAsAnOrder(int shoppingCartId);
        void UpdateOrder(Order order);
        void Save();
    }
}