using System.Collections.Concurrent;
using System.Collections.ObjectModel;
using Kitchen.Models;

namespace Kitchen.Repository.OrderRepository;

public interface IOrderRepository
{
    ObservableCollection<Order> Orders { get; set; }

    void InsertOrder(Order order);
    Task<Order?> GetOrderByTableId(int tableId);
    ObservableCollection<Order> GetAllOrders();
    Task<List<Order>> GetOrdersToPrepare();
}