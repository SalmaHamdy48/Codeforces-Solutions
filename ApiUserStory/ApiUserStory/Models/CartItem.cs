namespace ApiUserStory.Models
{
    public class CartItem
    {
        public int Id { get; set; }
        public string UserId { get; set; } = "";
        public int ProductId { get; set; }
    }
}