// <auto-generated>
// ReSharper disable All

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SampleDAL
{
    // ProductModelProductDescription
    public class SalesLT_ProductModelProductDescriptionConfiguration : IEntityTypeConfiguration<SalesLT_ProductModelProductDescription>
    {
        public void Configure(EntityTypeBuilder<SalesLT_ProductModelProductDescription> builder)
        {
            builder.ToTable("ProductModelProductDescription", "SalesLT");
            builder.HasKey(x => new { x.ProductModelId, x.ProductDescriptionId, x.Culture }).HasName("PK_ProductModelProductDescription_ProductModelID_ProductDescriptionID_Culture").IsClustered();

            builder.Property(x => x.ProductModelId).HasColumnName(@"ProductModelID").HasColumnType("int").IsRequired().ValueGeneratedNever();
            builder.Property(x => x.ProductDescriptionId).HasColumnName(@"ProductDescriptionID").HasColumnType("int").IsRequired().ValueGeneratedNever();
            builder.Property(x => x.Culture).HasColumnName(@"Culture").HasColumnType("nchar(6)").IsRequired().IsFixedLength().HasMaxLength(6).ValueGeneratedNever();
            builder.Property(x => x.Rowguid).HasColumnName(@"rowguid").HasColumnType("uniqueidentifier").IsRequired();
            builder.Property(x => x.ModifiedDate).HasColumnName(@"ModifiedDate").HasColumnType("datetime").IsRequired();

            // Foreign keys
            builder.HasOne(a => a.SalesLT_ProductDescription).WithMany(b => b.SalesLT_ProductModelProductDescriptions).HasForeignKey(c => c.ProductDescriptionId).OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK_ProductModelProductDescription_ProductDescription_ProductDescriptionID");
            builder.HasOne(a => a.SalesLT_ProductModel).WithMany(b => b.SalesLT_ProductModelProductDescriptions).HasForeignKey(c => c.ProductModelId).OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK_ProductModelProductDescription_ProductModel_ProductModelID");

            builder.HasIndex(x => x.Rowguid).HasDatabaseName("AK_ProductModelProductDescription_rowguid").IsUnique();
        }
    }

}
// </auto-generated>
