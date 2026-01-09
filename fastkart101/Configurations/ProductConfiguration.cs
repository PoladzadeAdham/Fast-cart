using fastkart101.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace fastkart101.Configurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.Property(p => p.Name).IsRequired().HasMaxLength(256);

            builder.Property(p => p.Description).IsRequired(false).HasMaxLength(256);

            builder.Property(p => p.Stock).IsRequired();


            builder.Property(x=> x.Price).IsRequired();

            builder.Property(p => p.DiscounedPrice).IsRequired(false);
            builder.Property(p => p.ImagePath).IsRequired().HasMaxLength(512);

            builder.ToTable(x=>x.HasCheckConstraint("CK_Product_Stock", "[Stock] >= 0"));
            builder.ToTable(x=>x.HasCheckConstraint("CK_Product_Price", "[Price] >= 0"));
            builder.ToTable(x=>x.HasCheckConstraint("CK_Product_DiscounedPrice", "[DiscounedPrice] >= 0"));


            builder.HasMany(x => x.BasketItems).WithOne(x => x.Product).HasForeignKey(x => x.ProductId);

        }
    }
}


