using Kitchen.Models;

namespace Kitchen.Repository.FoodRepository;

public interface IFoodRepository
{
    public Task GenerateFood();
    public IList<Food> GetFood();
    public Food? GetFoodById(int id);
}