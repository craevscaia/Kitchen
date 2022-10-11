using Kitchen.Models;

namespace Kitchen.Services.CookService;

public interface ICookService
{
    public void GenerateCooker();
    public IList<Cooker> GetCooker();
    public Task<Cooker> GetCookerById(int id);
    Task SplitOrderToCooks(Order order, ICollection<Food> foodList, Dictionary<int, List<Task>> tasks);
}