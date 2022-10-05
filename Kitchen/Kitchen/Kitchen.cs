using Kitchen.Services;
using Kitchen.Services.CookService;
using Kitchen.Services.FoodService;
using Kitchen.Services.OrderService;

namespace Kitchen.Kitchen;

public class Kitchen : IKitchen
{
    private readonly IFoodService _foodService;
    private readonly ICookService _cookService;

    public Kitchen(IFoodService foodService, ICookService cookService)
    {
        _foodService = foodService;
        _cookService = cookService;
    }

    public async Task InitializeKitchen()
    {
        var taskList = new List<Task>
        {
            Task.Run(() => _foodService.GenerateFood()),
            Task.Run(() => _cookService.GenerateCooker())
        };

        await Task.WhenAll(taskList);
    }
}