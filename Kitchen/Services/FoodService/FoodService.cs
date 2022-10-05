using Kitchen.Models;
using Kitchen.Repository.FoodRepository;

namespace Kitchen.Services.FoodService;

public class FoodService : IFoodService
{
    private readonly IFoodRepository _foodRepository;

    public FoodService(IFoodRepository foodRepository)
    {
        _foodRepository = foodRepository;
    }


    public void GenerateFood()
    {
        _foodRepository.GenerateFood();
    }

    public IList<Food> GetFood()
    {
        return _foodRepository.GetFood();
    }
    
    //Return the object by id
    public Food? GetFoodById(int id)
    {
        return _foodRepository.GetFoodById(id);
    }

    public IList<Food> GetFoodFromOrder(IList<int> foodList)
    {
        var foods = new List<Food>();
        foreach (var foodId in foodList)
        {
            var food = GetFoodById(foodId);
            if (food != null) foods.Add(food);
        }

        return foods;
    }
    
    public IEnumerable<Food> ArrangeFoodByComplexity(IList<Food> foods)
    {
        return foods.OrderByDescending(food => food.Complexity);
    }
}