using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgriNov.Models
{
    public class ServiceShoppingCartItem : IServiceShoppingCartItem
    {
        private BDDContext _DBContext;

        public ServiceShoppingCartItem()
        {
            _DBContext = new BDDContext();
        }
        public void Dispose()
        {
            _DBContext.Dispose();
        }

        public void InitializeTable()
        {
            return;
        }

        

        public void Save()
        {
            _DBContext.SaveChanges();
        }
    }
}