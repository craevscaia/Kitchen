namespace Kitchen.Models;

public class CookingApparatus
{
    public int Id;
    public string Name;
    public bool IsBusy;

    public CookingApparatus(int id, string name, bool isBusy)
    {
        Id = id;
        IsBusy = isBusy;
        Name = name;
    }
}