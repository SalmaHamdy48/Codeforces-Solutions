using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Project.Domain.Models;
using Project.Domain.Models.CartItems;
using Project.Domain.Models.Carts;
using Project.Domain.Models.Products;

namespace Project.Infrastructure.Configurations
{
    public class CartItemConfiguration : IEntityTypeConfiguration<CartItem>
    {
        public void Configure(EntityTypeBuilder<CartItem> builder)
        {
            builder
                .Property(x => x.CartId)
                .IsRequired();

            builder
                .Property(x => x.ProductId)
                .IsRequired();

            builder
                .Property(x => x.Quantity)
                .IsRequired();

            builder
                .Property(x => x.CreatedAt)
                .HasDefaultValueSql("getdate()");

            builder
                .Property(x => x.UpdatedAt)
                .HasDefaultValueSql("getdate()");

            builder
                .HasOne<Cart>(ci => ci.Cart)
                .WithMany(c => c.Items)
                .HasForeignKey(ci => ci.CartId)
                .OnDelete(DeleteBehavior.Cascade);

            builder
                .HasOne<Product>(ci => ci.Product)
                .WithMany()
                .HasForeignKey(ci => ci.ProductId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .HasQueryFilter(ci => !ci.IsDeleted);
        }
    }
}