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
        //No need to insert directly a shopping cart, it is done when a new one is created with an user account
        void Save();

    }
}