using Project.Domain.Models.Base;
using Project.Domain.Models.Carts;
using Project.Domain.Models.Categories;
using Project.Domain.Models.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
 
namespace Project.Domain.Models.CartItems
{
    public class CartItem : Entity, IAuditableEntity, ISoftDeletableEntity
    {
     
        public Guid CartId { get; set; }
        public virtual Cart Cart { get; set; } 

        public Guid ProductId { get; set; }
        public virtual Product Product { get; set; }

        public int Quantity { get; set; }

    }
}