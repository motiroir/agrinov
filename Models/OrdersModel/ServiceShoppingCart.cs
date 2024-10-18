using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgriNov.Models
{
    public class ServiceShoppingCart : IServiceShoppingCart
    {
         private BDDContext _DBContext;

        public ServiceShoppingCart()
        {
            _DBContext = new BDDContext();
        }
        public void Dispose()
        {
            _DBContext.Dispose();
        }

        public ShoppingCart GetShoppingCartForUserAccount(string userAccountIdStr)
        {
            int id;
            if (int.TryParse(userAccountIdStr, out id))
            {
                return GetShoppingCartForUserAccount(id);
            }
            return null;
        }

        public ShoppingCart GetShoppingCartForUserAccount(int userAccountId)
        {
            throw new NotImplementedException();
        }

        public void InitializeTable()
        {
            throw new NotImplementedException();
        }

        public void InsertShoppingCart(ShoppingCart shoppingCart)
        {
            throw new NotImplementedException();
        }

        public void Save()
        {
            _DBContext.SaveChanges();
        }
    }
}