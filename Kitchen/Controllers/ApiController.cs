using System.Text;
using Kitchen.Model;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Kitchen.Controllers;

[ApiController]
[Route("/order")] 
public class ApiController : Controller
{
    [HttpPost]
    public async Task<ContentResult> PostOrder([FromBody] Order order)
    {
        //procesat orderul
        //trimiti orderul inapoi
        try
        { var json = JsonConvert.SerializeObject(order); // c onvert to json
            var data = new StringContent(json, Encoding.UTF8, "application/json");  // convert to data

            var url = Setting.DiningHallUrl; // destination (kitchen)

            using var client = new HttpClient(); //open a portal 

            var response = await client.PostAsync(url, data); //se

        }
        catch (Exception e)
        {
            Console.WriteLine("Failled to send back");
        }
        return Content("Hi");

    }
}