using System.Collections.Concurrent;
using System.Collections.ObjectModel;
using Kitchen.Models;

namespace Kitchen.Services.OrderService;

public interface IOrderService
{
    void InsertOrder(Order order);
    Task<Order?> GetOrderByTableId(int tableId);
    ObservableCollection<Order> GetAllOrders();
    Task PrepareOrder();
}