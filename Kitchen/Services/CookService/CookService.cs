using Kitchen.Models;
using Kitchen.Repository.CookRepository;

namespace Kitchen.Services.CookService;

public class CookService : ICookService
{
    private readonly ICookRepository _cookRepository;

    public CookService(ICookRepository cookRepository)
    {
        _cookRepository = cookRepository;
    }

    public void GenerateCooker()
    {
        _cookRepository.GenerateCooker();
    }

    public IList<Cooker> GetCooker()
    {
        return _cookRepository.GetCooker();
    }

    //Return the object by id
    public Task<Cooker?> GetCookerById(int id)
    {
        return _cookRepository.GetCookerById(id);
    }

    public async Task SplitOrderToCooks(List<Food> foodList, List<Task> tasks)
    {
        var foods = new List<Food>(foodList);
        foreach (var food in foodList.ToList())
        {
            switch (food.Complexity)
            {
                case 3:
                {
                    await CookFood(food, tasks, foods);
                }
                    break;

                case 2:
                {
                    await CookFood(food, tasks, foods);
                }
                    break;

                case 1:
                {
                    await CookFood(food, tasks, foods);
                }
                    break;
            }
        }

        if (foods.Any())
        {
            await Task.Delay(TimeSpan.FromSeconds(5000));
        }

        await Task.WhenAll(tasks); //wait till whole food is cooked
        Console.WriteLine("All to food from this order was cooked");

    }

    private async Task CookFood(Food food, List<Task> tasks, List<Food> foods)
    {
        var cooker = await _cookRepository.GetCookerByRand(food.Complexity);

        if (cooker != null)
        {
            if (cooker.CookingList.Count < cooker.Proficiency)
            {
                await CookTheFood(cooker, food, tasks, foods);
            }
        }
    }

    private Task CookTheFood(Cooker cooker, Food food, List<Task> tasks, List<Food> foods)
    {
        cooker.CookingList.Add(food);
        Console.WriteLine($"I am cooker {cooker.Id} and I will cook {food.Name}");
        tasks.Add(WaitingForCookToPrepare(cooker.Id));
        foods.Remove(food);
        return Task.CompletedTask;
    }

    private async Task WaitingForCookToPrepare(int cookerId)
    {
        var cooker = await _cookRepository.GetCookerById(cookerId);
        var food = cooker?.CookingList.First();

        if (food != null)
        {
            await Task.Delay(food.PreparationTime);
            Console.WriteLine($"{food.Name} finished being cooker by {cooker?.Name}");

        }
    }
}