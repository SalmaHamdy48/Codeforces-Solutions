namespace ShopTask.Models;

public class Category
{
    public int CategoryId { get; set; }
    public string Name { get; set; } = string.Empty;

    // Navigation Property
    public ICollection<Product> Products { get; set; } = new List<Product>();
}