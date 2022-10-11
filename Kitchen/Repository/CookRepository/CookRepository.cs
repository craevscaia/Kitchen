using Kitchen.Models;
namespace Kitchen.Repository.CookRepository;

public class CookRepository : ICookRepository
{
    private readonly IList<Cooker> _cookers;

    public CookRepository()
    {
        _cookers = new List<Cooker>();
    }
    
    public void GenerateCooker()
    {
        _cookers.Add(new Cooker
        {
            Id = 1,
            Rank = 3,
            Proficiency = 4,
            Name = "Gordon Ramsay",
            CatchPhrase = "Hey, panini head, are you listening to me?",
            CookingList = new List<Food>()
        });
        _cookers.Add(new Cooker
        {
            Id = 2,
            Rank = 2,
            Proficiency = 3,
            Name = "Margaretta Robinson",
            CatchPhrase = "Hey, I will take the order?",
            CookingList = new List<Food>()
        });
        _cookers.Add(new Cooker
        {
            Id = 3,
            Rank = 2,
            Proficiency = 2,
            Name = "Anette Rubik",
            CatchPhrase = "I am busy, don't you see?",
            CookingList = new List<Food>()
        });
        _cookers.Add(new Cooker
        {
            Id = 4,
            Rank = 1,
            Proficiency = 2,
            Name = "Eloi Musetti",
            CatchPhrase = "I will do it!",
            CookingList = new List<Food>()
        });
    }
    
    public IList<Cooker> GetCooker()
    {
        return _cookers;
    }

    public Task<Cooker> GetCookerById(int id)
    {
        return Task.FromResult(_cookers.FirstOrDefault(cooker => cooker.Id.Equals(id)))!;
    }

    public Task<Cooker?> GetCookerByRand(int foodComplexity)
    {
        return Task.FromResult(_cookers.FirstOrDefault(cooker => cooker.Rank.Equals(foodComplexity)));
    }
}

