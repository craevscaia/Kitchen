using Kitchen.Models;

namespace Kitchen.Repository.FoodRepository;

public interface IFoodRepository
{
    public void GenerateFood();
    public IList<Food> GetFood();
    public Food? GetFoodById(int id);
}