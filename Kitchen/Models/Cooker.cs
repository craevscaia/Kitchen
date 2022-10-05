namespace Kitchen.Models;

public class Cooker
{
    public int Id { get; set; }
    public int Rank { get; set; }
    public int Proficiency { get; set; }
    public string Name { get; set; }
    public string CatchPhrase { get; set; }
    public List<Food> CookingList { get; set; }
}