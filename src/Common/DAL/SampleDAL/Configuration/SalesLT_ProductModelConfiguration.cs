// <auto-generated>
// ReSharper disable All

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SampleDAL
{
    // ProductModel
    public partial class SalesLT_ProductModelConfiguration : IEntityTypeConfiguration<SalesLT_ProductModel>
    {
        public void Configure(EntityTypeBuilder<SalesLT_ProductModel> builder)
        {
            builder.ToTable("ProductModel", "SalesLT");
            builder.HasKey(x => x.ProductModelId).HasName("PK_ProductModel_ProductModelID").IsClustered();

            builder.Property(x => x.ProductModelId).HasColumnName(@"ProductModelID").HasColumnType("int").IsRequired().ValueGeneratedOnAdd().UseIdentityColumn();
            builder.Property(x => x.Name).HasColumnName(@"Name").HasColumnType("nvarchar(50)").IsRequired().HasMaxLength(50);
            builder.Property(x => x.CatalogDescription).HasColumnName(@"CatalogDescription").HasColumnType("xml").IsRequired(false);
            builder.Property(x => x.Rowguid).HasColumnName(@"rowguid").HasColumnType("uniqueidentifier").IsRequired();
            builder.Property(x => x.ModifiedDate).HasColumnName(@"ModifiedDate").HasColumnType("datetime").IsRequired();

            builder.HasIndex(x => x.Name).HasDatabaseName("AK_ProductModel_Name").IsUnique();
            builder.HasIndex(x => x.Rowguid).HasDatabaseName("AK_ProductModel_rowguid").IsUnique();

            InitializePartial(builder);
        }

        partial void InitializePartial(EntityTypeBuilder<SalesLT_ProductModel> builder);
    }

}
// </auto-generated>
