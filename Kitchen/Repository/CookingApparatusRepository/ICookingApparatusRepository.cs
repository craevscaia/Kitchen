using Kitchen.Models;

namespace Kitchen.Repository.CookingApparatusRepository;

public interface ICookingApparatusRepository
{
    Task GenerateCookingApparatus();
    Task<CookingApparatus> GetCookingApparatusByName(string name);
}