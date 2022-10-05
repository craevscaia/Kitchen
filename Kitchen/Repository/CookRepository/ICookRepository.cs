using Kitchen.Models;
namespace Kitchen.Repository.CookRepository;

public interface ICookRepository
{
    public void GenerateCooker();
    public IList<Cooker> GetCooker();
    public Task<Cooker?> GetCookerById(int id);
    Task<Cooker?> GetCookerByRand(int foodComplexity);
}