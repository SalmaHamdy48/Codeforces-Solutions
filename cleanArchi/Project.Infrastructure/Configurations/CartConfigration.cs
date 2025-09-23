using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Project.Domain.Models;
using Project.Domain.Models.CartItems;
using Project.Domain.Models.Carts;

namespace Project.Infrastructure.Configurations
{
    public class CartConfiguration : IEntityTypeConfiguration<Cart>
    {
        public void Configure(EntityTypeBuilder<Cart> builder)
        {
            builder
                .Property(x => x.UserId)
                .IsRequired();

            builder
                .Property(x => x.CreatedAt)
                .HasDefaultValueSql("getdate()");

            builder
                .Property(x => x.UpdatedAt)
                .HasDefaultValueSql("getdate()");

            builder
                .HasMany<CartItem>(c => c.Items)
                .WithOne(ci => ci.Cart)
                .HasForeignKey(ci => ci.CartId)
                .OnDelete(DeleteBehavior.Cascade);

            builder
                .HasQueryFilter(c => !c.IsDeleted);
        }
    }
}