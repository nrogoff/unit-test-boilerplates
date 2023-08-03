// <auto-generated>
// ReSharper disable All

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SampleDAL
{
    // Product
    public class SalesLT_ProductConfiguration : IEntityTypeConfiguration<SalesLT_Product>
    {
        public void Configure(EntityTypeBuilder<SalesLT_Product> builder)
        {
            builder.ToTable("Product", "SalesLT");
            builder.HasKey(x => x.ProductId).HasName("PK_Product_ProductID").IsClustered();

            builder.Property(x => x.ProductId).HasColumnName(@"ProductID").HasColumnType("int").IsRequired().ValueGeneratedOnAdd().UseIdentityColumn();
            builder.Property(x => x.Name).HasColumnName(@"Name").HasColumnType("nvarchar(50)").IsRequired().HasMaxLength(50);
            builder.Property(x => x.ProductNumber).HasColumnName(@"ProductNumber").HasColumnType("nvarchar(25)").IsRequired().HasMaxLength(25);
            builder.Property(x => x.Color).HasColumnName(@"Color").HasColumnType("nvarchar(15)").IsRequired(false).HasMaxLength(15);
            builder.Property(x => x.StandardCost).HasColumnName(@"StandardCost").HasColumnType("money").IsRequired();
            builder.Property(x => x.ListPrice).HasColumnName(@"ListPrice").HasColumnType("money").IsRequired();
            builder.Property(x => x.Size).HasColumnName(@"Size").HasColumnType("nvarchar(5)").IsRequired(false).HasMaxLength(5);
            builder.Property(x => x.Weight).HasColumnName(@"Weight").HasColumnType("decimal(8,2)").HasPrecision(8,2).IsRequired(false);
            builder.Property(x => x.ProductCategoryId).HasColumnName(@"ProductCategoryID").HasColumnType("int").IsRequired(false);
            builder.Property(x => x.ProductModelId).HasColumnName(@"ProductModelID").HasColumnType("int").IsRequired(false);
            builder.Property(x => x.SellStartDate).HasColumnName(@"SellStartDate").HasColumnType("datetime").IsRequired();
            builder.Property(x => x.SellEndDate).HasColumnName(@"SellEndDate").HasColumnType("datetime").IsRequired(false);
            builder.Property(x => x.DiscontinuedDate).HasColumnName(@"DiscontinuedDate").HasColumnType("datetime").IsRequired(false);
            builder.Property(x => x.ThumbNailPhoto).HasColumnName(@"ThumbNailPhoto").HasColumnType("varbinary(max)").IsRequired(false);
            builder.Property(x => x.ThumbnailPhotoFileName).HasColumnName(@"ThumbnailPhotoFileName").HasColumnType("nvarchar(50)").IsRequired(false).HasMaxLength(50);
            builder.Property(x => x.Rowguid).HasColumnName(@"rowguid").HasColumnType("uniqueidentifier").IsRequired();
            builder.Property(x => x.ModifiedDate).HasColumnName(@"ModifiedDate").HasColumnType("datetime").IsRequired();

            // Foreign keys
            builder.HasOne(a => a.SalesLT_ProductCategory).WithMany(b => b.SalesLT_Products).HasForeignKey(c => c.ProductCategoryId).OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK_Product_ProductCategory_ProductCategoryID");
            builder.HasOne(a => a.SalesLT_ProductModel).WithMany(b => b.SalesLT_Products).HasForeignKey(c => c.ProductModelId).OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK_Product_ProductModel_ProductModelID");

            builder.HasIndex(x => x.Name).HasDatabaseName("AK_Product_Name").IsUnique();
            builder.HasIndex(x => x.ProductNumber).HasDatabaseName("AK_Product_ProductNumber").IsUnique();
            builder.HasIndex(x => x.Rowguid).HasDatabaseName("AK_Product_rowguid").IsUnique();
        }
    }

}
// </auto-generated>
