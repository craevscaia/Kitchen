using Kitchen.Models;

namespace Kitchen.Services.CookingAparattusService;

public interface ICookingApparatusService
{
    Task GenerateCookingApparatus();
    Task<CookingApparatus> GetCookingApparatusByName(string name);
}