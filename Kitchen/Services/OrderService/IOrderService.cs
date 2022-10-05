using Kitchen.Models;

namespace Kitchen.Services.OrderService;

public interface IOrderService
{
    void InsertOrder(Order order);
    Task<Order?> GetOrderByTableId(int tableId);
    IList<Order> GetAllOrders();
    Task PrepareOrder();
}