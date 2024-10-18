using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgriNov.Models
{
    public interface IServiceShoppingCart : IDisposable
    {
        void InitializeTable();
        ShoppingCart GetShoppingCartForUserAccount(string userAccountIdStr);
        ShoppingCart GetShoppingCartForUserAccount(int userAccountId);
        void InsertShoppingCart(ShoppingCart shoppingCart);
        void Save();

    }
}