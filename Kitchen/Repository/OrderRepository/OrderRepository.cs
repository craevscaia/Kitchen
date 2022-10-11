using System.Collections.Concurrent;
using System.Collections.ObjectModel;
using Kitchen.Models;

namespace Kitchen.Repository.OrderRepository;

public class OrderRepository : IOrderRepository
{
    public ObservableCollection<Order> Orders { get; set; }

    public OrderRepository()
    {
        Orders = new ObservableCollection<Order>();
    }

    public void InsertOrder(Order order)
    {
        Orders.Add(order);
    }

    public Task<Order?> GetOrderByTableId(int tableId)
    {
        return Task.FromResult(Orders.FirstOrDefault(order => order.TableId.Equals(tableId)));
    }

    public ObservableCollection<Order> GetAllOrders()
    {
        return Orders;
    }

    public Task<List<Order>> GetOrdersToPrepare()
    {
        return Task.FromResult(Orders.OrderBy(o => o.CreatedOnUtc).ThenBy(o => o.Priority).ToList());
    }

}