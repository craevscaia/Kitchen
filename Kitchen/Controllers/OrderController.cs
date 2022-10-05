using System.Text;
using Kitchen.Models;
using Kitchen.Services.OrderService;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Kitchen.Controllers;

[ApiController]
[Route("/kitchen")]
public class OrderController : Controller
{
  
    private readonly IOrderService _orderService;
    System.Threading.Mutex _mutey = null;
    // GET
    public OrderController(IOrderService orderService)
    {
        _orderService = orderService;
    }
    [HttpPost]
    public Task GetOrderFromKitchen([FromBody] Order? order)
    {
        if (order == null) return Task.CompletedTask;
    
        _orderService.InsertOrder(order);
        _orderService.PrepareOrder();
        try
        {
            Console.WriteLine($"An order with {order.Id} came in the kitchen");
            // _orderService.InsertOrder(order);
        }
        catch (Exception e)
        {
            Console.WriteLine($"Failed to get order {order.Id}", e);
        }

        return Task.CompletedTask;
    }
    

    // public void peMaiTarziu()
    // {
    //     var json = JsonConvert.SerializeObject(order);
    //     var data = new StringContent(json, Encoding.UTF8, "application/json");
    //
    //     const string url = Setting.DiningHallUrl;
    //     using var client = new HttpClient();
    //
    //     var response = await client.PostAsync(url, data);
    //     var result = await response.Content.ReadAsStringAsync();
    // }
}