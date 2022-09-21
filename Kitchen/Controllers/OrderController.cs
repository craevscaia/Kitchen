using System.Text;
using Kitchen.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Kitchen.Controllers;

[ApiController]
[Route("/kitchen")]
public class OrderController : Controller
{
    [HttpGet]
    public ContentResult GetOrders()
    {
        return Content("Hello");
    }
  
    private Task Threads()
    {
        var generateOrderThread1 = CreateThread();
        var generateOrderThread2 = CreateThread();
        var generateOrderThread3 = CreateThread();
        var generateOrderThread4 = CreateThread();
        generateOrderThread1.Start();
        generateOrderThread2.Start();
        generateOrderThread3.Start();
        generateOrderThread4.Start();

        return Task.CompletedTask;
    }

    private Task CreateThread()
    {
        return Threads();
    }

    // GET
    [HttpPost]
    public static async Task PostOrder([FromBody] Order? order)
    {
        try
        {
            //process order
            //send finished order object back to dining hall`
            // Thread.Sleep(10000);
            if (order != null)
            {
                order.Status = Status.ReadyToBeServed;
                Console.WriteLine(
                    $"Order with id {order.Id} from table {order.TableId} was brought by waiter with id {order.WaiterId}");
            }


            var json = JsonConvert.SerializeObject(order);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            const string url = Setting.DiningHallUrl;
            using var client = new HttpClient();

            var response = await client.PostAsync(url, data);
            var result = await response.Content.ReadAsStringAsync();
        }   
        catch (Exception e)
        {
            Console.WriteLine($"Failed to send order {order.Id}");
        }
        
    }
}
