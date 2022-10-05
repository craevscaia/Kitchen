using Kitchen.Models;

namespace Kitchen.Services.FoodService;

public interface IFoodService
{
    public void GenerateFood();
    public IList<Food> GetFood();
    public Food? GetFoodById(int id);
    public IList<Food> GetFoodFromOrder(IList<int> foodList);
    public IEnumerable<Food> ArrangeFoodByComplexity(IList<Food> foods);
}