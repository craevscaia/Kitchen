using System.Net;
using System.Text;
using Kitchen.Models;
using Kitchen.Repository.OrderRepository;
using Kitchen.Services.CookService;
using Kitchen.Services.FoodService;
using Newtonsoft.Json;

namespace Kitchen.Services.OrderService;

public class OrderService : IOrderService
{
    private readonly IOrderRepository _orderRepository;
    private readonly IFoodService _foodService;
    private readonly ICookService _cookService;

    public OrderService(IOrderRepository orderRepository, IFoodService foodService, ICookService cookService)
    {
        _orderRepository = orderRepository;
        _foodService = foodService;
        _cookService = cookService;
    }

    public void InsertOrder(Order order)
    {
        _orderRepository.InsertOrder(order);
    }

    public Task<Order?> GetOrderByTableId(int tableId)
    {
       return _orderRepository.GetOrderByTableId(tableId);
    }

    public IList<Order> GetAllOrders()
    {
        return _orderRepository.GetAllOrders();
    }
    
    public async Task PrepareOrder()
    {
        Console.WriteLine($"I received an order");
        // here will be an mutex
        var orders = GetAllOrders(); //1,2,3,4
        var arrangedList = ArrangeOrderByPriority(orders).ToList(); // dupa priority;

        foreach (var order in arrangedList)
        {
            Console.WriteLine($"I will cook order with {order.Id} that has {order.FoodList.Count}");
            var foods = _foodService.GetFoodFromOrder(order.FoodList);
            var arrangedFoodList = _foodService.ArrangeFoodByComplexity(foods).ToList();
            await _cookService.SplitOrderToCooks(arrangedFoodList, new List<Task>());
            await SendOrder(order);
        }
    }

    private async Task SendOrder(Order order)
    {
        await Task.Run(async () => { 
            try
            {
                var serializeObject = JsonConvert.SerializeObject(order);
                var data = new StringContent(serializeObject, Encoding.UTF8, "application/json");

                const string url = Setting.DiningHallUrl;
                using var client = new HttpClient();

                var response = await client.PostAsync(url, data);

                if (response.StatusCode == HttpStatusCode.Accepted)
                {
                    Console.WriteLine($"The order with id {order.Id} was driven in the kitchen");
                }
            }
            catch (Exception e)
            { 
                Console.WriteLine($"Failed to send order {order.Id}", ConsoleColor.Red);
            }
        });
    }

    private static IEnumerable<Order> ArrangeOrderByPriority(IEnumerable<Order> orders)
    {
        return orders.OrderBy(order => order.Priority);
    }


}