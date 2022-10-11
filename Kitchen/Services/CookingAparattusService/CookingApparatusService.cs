using Kitchen.Models;
using Kitchen.Repository.CookingApparatusRepository;

namespace Kitchen.Services.CookingAparattusService;

public class CookingApparatusService : ICookingApparatusService
{
    private readonly ICookingApparatusRepository _cookingApparatusRepository;

    public CookingApparatusService(ICookingApparatusRepository cookingApparatusRepository)
    {
        _cookingApparatusRepository = cookingApparatusRepository;
    }

    public Task GenerateCookingApparatus()
    {
        return _cookingApparatusRepository.GenerateCookingApparatus();
    }
    
    public Task<CookingApparatus> GetCookingApparatusByName(string name)
    {
        return _cookingApparatusRepository.GetCookingApparatusByName(name);
    }
}