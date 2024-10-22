using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
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
            return _DBContext.ShoppingCarts.Include(cart => cart.ShoppingCartItems).ThenInclude(shoppingCartItem => shoppingCartItem.MemberShipFee).Include(cart => cart.ShoppingCartItems).ThenInclude(shoppingCartItem => shoppingCartItem.Product).FirstOrDefault(s => s.UserAccountId == userAccountId);
        }
        public void AddShoppingCartItemToShoppingCart(int shoppingCartId, ShoppingCartItem shoppingCartItem)
        {
            ShoppingCart shoppingCart = _DBContext.ShoppingCarts.Include("ShoppingCartItems").FirstOrDefault(s => s.UserAccountId == shoppingCartId);
            if (shoppingCart != null)
            {
                shoppingCart.ShoppingCartItems.Add(shoppingCartItem);
                shoppingCart.CalculateTotal();
                shoppingCart.DateLastModified = DateTime.Now;
                Save();
            }
        }
        public void AddMemberShipFeeToShoppingCart(int shoppingCartId, ShoppingCartItem shoppingCartItem)
        {
            //shoppingCartId = userAccountId
            //Check if user already has a valid membership fee, in that case, return and don't add to shopping cart
            using (IServiceUserAccount sUA = new ServiceUserAccount())
            {
                if (sUA.CheckIfMemberShipValid(shoppingCartId))
                {
                    return;
                }
            }
            //If there's already a membershipfee in the cart, return
            if (IsAMemberShipFeeInTheCart(shoppingCartId))
            {
                return;
            }
            //UserACcountId and ShoppingCartId share the same Id
            MemberShipFee m = new MemberShipFee() { UserAccountId = shoppingCartId };
            // We could compute different membershipfees here depending on user account
            shoppingCartItem.MemberShipFee = m;
            //Quantity must be set after setting MemberShipFee, otherwise the total price can't be computed
            shoppingCartItem.Quantity = 1;
            AddShoppingCartItemToShoppingCart(shoppingCartId, shoppingCartItem);
        }
        public bool IsAMemberShipFeeInTheCart(int userAccountId)
        {
            ShoppingCart sC = GetShoppingCartForUserAccount(userAccountId);
            if (sC != null && sC.ShoppingCartItems.Any())
            {
                foreach (ShoppingCartItem item in sC.ShoppingCartItems)
                {
                    if (item.MemberShipFee != null)
                    {
                        return true;
                    }
                }
            }
            return false;
        }
        public void AddProductToShoppingCart(int productId, int quantity, int shoppingCartId)
        {
            // if product already in shopping cart
            ShoppingCart sC = GetShoppingCartForUserAccount(shoppingCartId);
            if (sC != null && sC.ShoppingCartItems.Any())
            {
                foreach (ShoppingCartItem item in sC.ShoppingCartItems)
                {
                    if (item.Product != null && item.Product.Id == productId)
                    {
                        //Updating shopping cart item
                        item.Quantity = quantity;
                        using (IProductService sP = new ProductService())
                        {
                            item.Total = sP.GetProductByID(productId).Price * item.Quantity;
                        }
                        item.DateLastModified = DateTime.Now;
                        UpdateShoppingCartItem(item);
                        return;
                    }
                }
            }
            // if product not already in shopping cart
            ShoppingCartItem shoppingCartItem = new ShoppingCartItem() { ProductId = productId };
            shoppingCartItem.Quantity = quantity;
            using (IProductService sP = new ProductService())
            {
                shoppingCartItem.Total = sP.GetProductByID(productId).Price * shoppingCartItem.Quantity;
            }
            AddShoppingCartItemToShoppingCart(shoppingCartId, shoppingCartItem);
        }
        public void UpdateShoppingCartItem(ShoppingCartItem shoppingCartItem)
        {
            ShoppingCartItem oldShoppingCartItem = _DBContext.ShoppingCartItems.FirstOrDefault(sCI => sCI.Id == shoppingCartItem.Id);
            _DBContext.Entry(oldShoppingCartItem).CurrentValues.SetValues(shoppingCartItem);
            Save();
        }
        public void InitializeTable()
        {
            AddMemberShipFeeToShoppingCart(1, new ShoppingCartItem());
            AddMemberShipFeeToShoppingCart(2, new ShoppingCartItem());
            AddMemberShipFeeToShoppingCart(3, new ShoppingCartItem());
            AddProductToShoppingCart(1, 2, 1);
            AddProductToShoppingCart(1, 3, 1);
            AddProductToShoppingCart(1, 1, 2);
            AddProductToShoppingCart(2, 1, 2);
            AddProductToShoppingCart(3, 1, 3);
        }
        public void Save()
        {
            _DBContext.SaveChanges();
        }
    }
}