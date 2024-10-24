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
            return _DBContext.ShoppingCarts.Include(cart => cart.ShoppingCartItems).ThenInclude(shoppingCartItem => shoppingCartItem.MemberShipFee)
                                            .Include(cart => cart.ShoppingCartItems).ThenInclude(shoppingCartItem => shoppingCartItem.Product)
                                            .Include(cart => cart.ShoppingCartItems).ThenInclude(shoppingCartItem => shoppingCartItem.BoxContract)
                                            .FirstOrDefault(s => s.UserAccountId == userAccountId);
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
            shoppingCartItem.Total = shoppingCartItem.MemberShipFee.Price * shoppingCartItem.Quantity;
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
            ShoppingCart sC = GetShoppingCartForUserAccount(shoppingCartId);
            if(sC == null)
            {
                return;
            }
            // if product already in shopping cart
            if (sC.ShoppingCartItems.Any())
            {
                foreach (ShoppingCartItem item in sC.ShoppingCartItems)
                {
                    if (item.Product != null && item.Product.Id == productId)
                    {
                        if (quantity > 0)
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
                        else
                        {
                            _DBContext.ShoppingCartItems.Remove(item);
                            Save();
                            return;
                        }
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

        public void AddBoxContractToShoppingCart(int boxContractId, int quantity, int shoppingCartId)
        {
            ShoppingCart sC = GetShoppingCartForUserAccount(shoppingCartId);
            if(sC == null)
            {
                return;
            }
            // if (sC.ShoppingCartItems.Any())
            // {
            //     //check if box contract already in cart
            //     // for now impossible to add twice the same contract
            // }
            ShoppingCartItem shoppingCartItem = new ShoppingCartItem() { BoxContractId = boxContractId};
            shoppingCartItem.Quantity = quantity;
            using(IServiceBoxContract sBC = new ServiceBoxContract())
            {
                // 13 is the number of weeks in a contract
                shoppingCartItem.Total = 13 * sBC.GetBoxContractById(boxContractId).Price * shoppingCartItem.Quantity;
            }
            AddShoppingCartItemToShoppingCart(shoppingCartId, shoppingCartItem);
        }

        public void UpdateShoppingCartItem(ShoppingCartItem shoppingCartItem)
        {
            ShoppingCartItem oldShoppingCartItem = _DBContext.ShoppingCartItems.FirstOrDefault(sCI => sCI.Id == shoppingCartItem.Id);
            _DBContext.Entry(oldShoppingCartItem).CurrentValues.SetValues(shoppingCartItem);
            Save();
            ShoppingCart shoppingCart = _DBContext.ShoppingCarts.FirstOrDefault(sc => sc.UserAccountId == oldShoppingCartItem.ShoppingCartId);
            shoppingCart.CalculateTotal();
            shoppingCart.DateLastModified = DateTime.Now;
            Save();
        }

        public int GetQuantityByProductInCart(int productId, int userId)
        {
            ShoppingCart cart = GetShoppingCartForUserAccount(userId);
            if (cart == null)
            {
                return 0;
            }
            ShoppingCartItem productItem = cart.ShoppingCartItems.FirstOrDefault(item => item.Product != null && item.Product.Id == productId);
            return productItem != null ? productItem.Quantity : 0;
        }


        public void InitializeTable()
        {
            // AddMemberShipFeeToShoppingCart(1, new ShoppingCartItem());
            // AddMemberShipFeeToShoppingCart(2, new ShoppingCartItem());
            // AddMemberShipFeeToShoppingCart(3, new ShoppingCartItem());
            AddProductToShoppingCart(1, 2, 1);
            AddProductToShoppingCart(1, 3, 1);
            AddProductToShoppingCart(1, 1, 2);
            AddProductToShoppingCart(2, 1, 2);
            AddProductToShoppingCart(3, 1, 3);
            AddProductToShoppingCart(3, 1, 4);
            AddProductToShoppingCart(3, 1, 12);
            AddBoxContractToShoppingCart(1,1,12);
        }

        //Do not call when order was not placed, this would remove membership fee
        public void EmptyShoppingCart(int shoppingCartId)
        {
            List<ShoppingCartItem> shoppingCartItems = _DBContext.ShoppingCartItems.Where(item => item.ShoppingCartId == shoppingCartId).ToList();
            _DBContext.ShoppingCartItems.RemoveRange(shoppingCartItems);
            Save();
            //Set ShoppingCartTotal to 0
            ShoppingCart shoppingCart = _DBContext.ShoppingCarts.FirstOrDefault(sc => sc.UserAccountId == shoppingCartId);
            shoppingCart.CalculateTotal();
            shoppingCart.DateLastModified = DateTime.Now;
            Save();
        }

        public void EmptyShoppingCartExceptMemberShipFee(string shoppingCartIdStr)
        {
            int id;
            if (int.TryParse(shoppingCartIdStr, out id))
            {
                EmptyShoppingCartExceptMemberShipFee(id);
            }
            return;
        }

        public void EmptyShoppingCartExceptMemberShipFee(int shoppingCartId)
        {
            List<ShoppingCartItem> shoppingCartItems = _DBContext.ShoppingCartItems.Where(item => (item.ShoppingCartId == shoppingCartId && item.MemberShipFeeId == null)).ToList();
            _DBContext.ShoppingCartItems.RemoveRange(shoppingCartItems);
            Save();
            //Set ShoppingCartTotal to 0
            ShoppingCart shoppingCart = _DBContext.ShoppingCarts.FirstOrDefault(sc => sc.UserAccountId == shoppingCartId);
            shoppingCart.CalculateTotal();
            shoppingCart.DateLastModified = DateTime.Now;
            Save();
        }


        public void Save()
        {
            _DBContext.SaveChanges();
        }
    }
}