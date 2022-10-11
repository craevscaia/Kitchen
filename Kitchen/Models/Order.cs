namespace Kitchen.Models;

public class Order
{
    public int Id { get; set; }
    public int TableId { get; set; }
    public int WaiterId { get; set; }
    public int Priority { get; set; }
    public int MaxWait { get; set; }
    public bool OrderIsComplete { get; set; }
    public IList<int> FoodList { get; set; }
    public Status Status { get; set; } 
    public DateTime CreatedOnUtc { get; set; }
    public DateTime FinishedOnUtc { get; set; }
}