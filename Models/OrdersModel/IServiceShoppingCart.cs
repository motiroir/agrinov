using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgriNov.Models
{
    public interface IServiceShoppingCart : IDisposable
    {
        void InitializeTable();
        // Get
        ShoppingCart GetShoppingCartForUserAccount(string userAccountIdStr);
        ShoppingCart GetShoppingCartForUserAccount(int userAccountId);
        // Info
        public bool IsAMemberShipFeeInTheCart(int userAccountId);
        public int GetQuantityByProductInCart(int productId, int userId);
        // Add
        //No need to insert directly a shopping cart, it is done when a new one is created with an user account
        public void AddProductToShoppingCart(int productId, int quantity, int shoppingCartId);
        public void AddMemberShipFeeToShoppingCart(int shoppingCartId, ShoppingCartItem shoppingCartItem);
        public void AddBoxContractToShoppingCart(int boxContractId, int quantity, int shoppingCartId);

        // Update
        public void UpdateShoppingCartItem(ShoppingCartItem shoppingCartItem);
        // Empty
        public void EmptyShoppingCart(int shoppingCartId);
        public void EmptyShoppingCartExceptMemberShipFee(string shoppingCartIdStr);
        public void EmptyShoppingCartExceptMemberShipFee(int shoppingCartId);
        // Save
        void Save();

    }
}