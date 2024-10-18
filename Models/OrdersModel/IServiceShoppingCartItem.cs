using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgriNov.Models
{
    public interface IServiceShoppingCartItem : IDisposable
    {
        void InitializeTable();
        void InsertShoppingCartItem(ShoppingCartItem shoppingCartItem);
        void Save();

    }
}