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
            ShoppingCartItem sci1 = new ShoppingCartItem() {UserAccountId = 3};
            InsertShoppingCartItemForMemberShipFee(sci1);
            Save();
        }

        public void InsertShoppingCartItem(ShoppingCartItem shoppingCartItem)
        {
            _DBContext.ShoppingCartItems.Add(shoppingCartItem);
            Save();
        }

        public void InsertShoppingCartItemForMemberShipFee(ShoppingCartItem shoppingCartItem)
        {
            using(IServiceMemberShipFee sMF = new ServiceMemberShipFee())
            {

            }
            MemberShipFee m = new MemberShipFee() {UserAccountId = shoppingCartItem.UserAccountId};
            // We could compute different membershipfees here depending on user account
            shoppingCartItem.MemberShipFee = m; 
            //Quantity must be set after setting MemberShipFee, otherwise the total price can't be computed
            shoppingCartItem.Quantity = 1;
            InsertShoppingCartItem(shoppingCartItem);
            Save();
        }

        public void Save()
        {
            _DBContext.SaveChanges();
        }
    }
}