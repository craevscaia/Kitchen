namespace Kitchen.Model;

public class Order
{
    public int Id { get; set; }
    public IList<int> FoodList { get; set; }
    public int Priority { get; set; }
    public int MaxWait { get; set; }
}