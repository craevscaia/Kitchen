using Kitchen.Helper;
using Kitchen.Models;
using Kitchen.Repository.CookRepository;

namespace Kitchen.Services.CookService;

public class CookService : ICookService
{
    private readonly ICookRepository _cookRepository;
    private readonly List<Task> _tasks;

    public CookService(ICookRepository cookRepository)
    {
        _cookRepository = cookRepository;
        _tasks = new List<Task>();
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
    public Task<Cooker> GetCookerById(int id)
    {
        return _cookRepository.GetCookerById(id);
    }

    public async Task SplitOrderToCooks(Order order, ICollection<Food> foodList, Dictionary<int, List<Task>> tasks)
    {
        var foods = new List<Food>(foodList);
        foreach (var food in foodList.ToList())
        {
            switch (food.Complexity)
            {
                case 3:
                {
                    await AssignFoodToCooker(order, food, tasks, foods);
                }
                    break;

                case 2:
                {
                    await AssignFoodToCooker(order, food, tasks, foods);
                }
                    break;

                case 1:
                {
                    await AssignFoodToCooker(order, food, tasks, foods);
                }
                    break;
            }
        }

        if (foods.Any())
        {
            await Task.Delay(TimeSpan.FromSeconds(1));
        }

        await Task.WhenAll(tasks[order.Id]); //wait till whole food is cooked
        
        Console.WriteLine("All the food from this order was cooked");
    }

    private async Task AssignFoodToCooker(Order order, Food food, Dictionary<int, List<Task>> tasks, List<Food> foods)
    {
        var cooker = await _cookRepository.GetCookerByRand(food.Complexity);

        if (cooker != null)
        {
            if (cooker.CookingList.Count < cooker.Proficiency)
            {
                await CookTheFood(order, cooker, food, tasks, foods);
            }
        }
    }

    private Task CookTheFood(Order order, Cooker cooker, Food food, Dictionary<int, List<Task>> tasksDictionary,
        List<Food> foods)
    {
        Console.WriteLine($"I am cooker {cooker.Id} and I will cook {food.Name}");
        cooker.CookingList.Add(food);
        var task = WaitingForCookToPrepare(order, cooker.Id); //Here the apparatus is assigned
        if (tasksDictionary.ContainsKey(order.Id))
        {
            _tasks.Add(task);
            tasksDictionary[order.Id] = _tasks;
        }
        else
        {
            _tasks.Add(task);
            tasksDictionary.Add(order.Id, _tasks);
        }

        foods.Remove(food);
        return Task.CompletedTask;
    }

    private async Task WaitingForCookToPrepare(Order order, int cookerId)
    {
        await Task.Run(async () =>
        {
            var cooker = await _cookRepository.GetCookerById(cookerId);
            while (cooker.CookingList.Any()) //something
            {
                var food = cooker.CookingList.FirstOrDefault();
                if (food == null) continue;
                if (food.CookingApparatus != null)
                {
                    if (food.CookingApparatus.IsBusy)
                    {
                        await SleepingGenerator.Delay(1);
                    }
                    else
                    {
                        await CookFoodInCookingApparatus(food, order, cooker, food.CookingApparatus);
                    }
                }
                else
                {
                    await CookFoodWithoutApparatus(food, order, cooker);
                }
            }
        });
    }

    private async Task CookFoodWithoutApparatus(Food food, Order order, Cooker cooker)
    {
        await SleepingGenerator.Delay(food.PreparationTime /2 );
        cooker.CookingList.Remove(food);
        Console.WriteLine($"I am {cooker.Name} and cooked {food.Name} from order {order.Id}", ConsoleColor.Green);
    }

    private async Task CookFoodInCookingApparatus(Food food, Order order, Cooker cooker, CookingApparatus cookingApparatus)
    {
        cookingApparatus.IsBusy = true;
        await SleepingGenerator.Delay(food.PreparationTime /2);
        cookingApparatus.IsBusy = false;
        cooker.CookingList.Remove(food);
        Console.WriteLine($"I am {cooker.Name} and cooked {food.Name} from order {order.Id}", ConsoleColor.Green);
    }
}