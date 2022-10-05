using Kitchen.Models;

namespace Kitchen.Services.CookService;

public interface ICookService
{
    public void GenerateCooker();
    public IList<Cooker> GetCooker();
    public Task<Cooker?> GetCookerById(int id);
    Task SplitOrderToCooks(List<Food> foodList, List<Task> tasks);
}