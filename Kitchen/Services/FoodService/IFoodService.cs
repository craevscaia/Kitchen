using Kitchen.Models;

namespace Kitchen.Services.FoodService;

public interface IFoodService
{
    public void GenerateFood();
    public IList<Food> GetFood();
    public Food? GetFoodById(int id);
    public Task<IList<Food>> GetFoodFromOrder(IEnumerable<int> foodList);
    public Task<IEnumerable<Food>> ArrangeFoodByComplexity(IEnumerable<Food> foods);
}