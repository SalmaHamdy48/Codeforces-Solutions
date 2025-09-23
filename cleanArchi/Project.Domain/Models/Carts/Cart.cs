using Project.Domain.Models.Base;
using Project.Domain.Models.CartItems;

namespace Project.Domain.Models.Carts
{
    public class Cart : Entity, IAuditableEntity, ISoftDeletableEntity
    {
        public Guid UserId { get; set; } 
        
        public ICollection<CartItem> Items { get; set; } = new List<CartItem>();

        private Cart() { } 

        public Cart(Guid userId)
        {
            UserId = userId;
        }
    }
}