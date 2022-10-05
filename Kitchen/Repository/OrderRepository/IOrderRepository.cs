using Kitchen.Models;

namespace Kitchen.Repository.OrderRepository;

public interface IOrderRepository
{
    void InsertOrder(Order order);
    Task<Order?> GetOrderByTableId(int tableId);
    IList<Order> GetAllOrders();
}