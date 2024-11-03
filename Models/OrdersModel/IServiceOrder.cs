namespace AgriNov.Models
{
    public interface IServiceOrder : IDisposable
    {
        void InitializeTable();
        void SaveShoppingCartAsAnOrder(int shoppingCartId, Payment payment);
        void SaveShoppingCartAsAnOrder(string shoppingCartIdStr, Payment payment);
        void InsertOrder(Order order);
        List<Order> GetAllOrdersWithoutDetails();
        List<Order> GetAllOrdersWithJustUserDetails();
        List<Order> GetAllOrders();
        List<Order> GetAllOrdersForUserAccount(int userAccountId);
        List<Order> GetAllOrdersForUserAccount(string userAccountIdStr);
        Order GetOrderById(int id);
        Order GetOrderById(string id);
        void UpdateOrder(Order order);
        void Save();
    }
}