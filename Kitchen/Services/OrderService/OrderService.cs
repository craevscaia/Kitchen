using System.Collections.Concurrent;
using System.Collections.ObjectModel;
using System.Net;
using System.Text;
using Kitchen.Helper;
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
    private Semaphore _semaphore;

    public OrderService(IOrderRepository orderRepository, IFoodService foodService, ICookService cookService)
    {
        _orderRepository = orderRepository;
        _foodService = foodService;
        _cookService = cookService;
        _semaphore = new Semaphore(2, 2);
    }

    public void InsertOrder(Order order)
    {
        _orderRepository.InsertOrder(order);
    }

    public Task<Order?> GetOrderByTableId(int tableId)
    {
       return _orderRepository.GetOrderByTableId(tableId);
    }

    public ObservableCollection<Order> GetAllOrders()
    {
        return _orderRepository.GetAllOrders();
    }
    
    public async Task PrepareOrder()
    {
        while (true)
        {
            var orders = await _orderRepository.GetOrdersToPrepare();
            if (orders.Any())
            {
                var order = orders.FirstOrDefault();
                if (order != null)
                {
                    var foodList = await _foodService.GetFoodFromOrder(order.FoodList);
                    var foodsByComplexity = await _foodService.ArrangeFoodByComplexity(foodList);
                    await Task.Run(async () =>
                    {
                        _semaphore.WaitOne();
                        Console.WriteLine($"I started order with id {order.Id}, food list size: {foodsByComplexity.Count()}");
                        await _cookService.SplitOrderToCooks(order, foodList, new Dictionary<int, List<Task>>());
                        Console.WriteLine("I am released");
                        order.FinishedOnUtc = DateTime.Now; // order time finished
                        await SendOrder(order);
                         await RemoveOrder(order);
                         _semaphore.Release();

                    });
                    
                }
            }
            else
            {
                // ConsoleHelper.Print("There are no orders");
                await SleepingGenerator.Delay(1);
                await PrepareOrder();
            }
        }
    }

    private Task RemoveOrder(Order order)
    {
        _orderRepository.Orders.Remove(order);
        return Task.FromResult(Task.CompletedTask);
    }

    private async Task SendOrder(Order order)
    {
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
    }

    private static IEnumerable<Order> ArrangeOrderByPriority(IEnumerable<Order> orders)
    {
        return orders.OrderBy(order => order.Priority);
    }


}