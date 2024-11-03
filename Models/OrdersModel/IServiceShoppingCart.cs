namespace AgriNov.Models
{
    public interface IServiceShoppingCart : IDisposable
    {
        void InitializeTable();
        // Get
        ShoppingCart GetShoppingCartForUserAccount(string userAccountIdStr);
        ShoppingCart GetShoppingCartForUserAccount(int userAccountId);
        // Info
        bool IsAMemberShipFeeInTheCart(int userAccountId);
        int GetQuantityByProductInCart(int productId, int userId);
        // Add
        //No need to insert directly a shopping cart, it is done when a new one is created with an user account
        void AddShoppingCartItemToShoppingCart(int shoppingCartId, ShoppingCartItem shoppingCartItem); //internal
        void AddProductToShoppingCart(int productId, int quantity, int shoppingCartId);
        void AddMemberShipFeeToShoppingCart(int shoppingCartId, ShoppingCartItem shoppingCartItem);
        void AddBoxContractToShoppingCart(int boxContractId, int quantity, int shoppingCartId);

        // Update
        void UpdateShoppingCartItem(ShoppingCartItem shoppingCartItem);
        // Empty
        void EmptyShoppingCart(int shoppingCartId);
        void EmptyShoppingCartExceptMemberShipFee(string shoppingCartIdStr);
        void EmptyShoppingCartExceptMemberShipFee(int shoppingCartId);
        // Save
        void Save();

    }
}