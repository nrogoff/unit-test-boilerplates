// <auto-generated>
// ReSharper disable All

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SampleDAL
{
    // ****************************************************************************************************
    // This is not a commercial licence, therefore only a few tables/views/stored procedures are generated.
    // ****************************************************************************************************

    // Address
    public class SalesLT_AddressConfiguration : IEntityTypeConfiguration<SalesLT_Address>
    {
        public void Configure(EntityTypeBuilder<SalesLT_Address> builder)
        {
            builder.ToTable("Address", "SalesLT");
            builder.HasKey(x => x.AddressId).HasName("PK_Address_AddressID").IsClustered();

            builder.Property(x => x.AddressId).HasColumnName(@"AddressID").HasColumnType("int").IsRequired().ValueGeneratedOnAdd().UseIdentityColumn();
            builder.Property(x => x.AddressLine1).HasColumnName(@"AddressLine1").HasColumnType("nvarchar(60)").IsRequired().HasMaxLength(60);
            builder.Property(x => x.AddressLine2).HasColumnName(@"AddressLine2").HasColumnType("nvarchar(60)").IsRequired(false).HasMaxLength(60);
            builder.Property(x => x.City).HasColumnName(@"City").HasColumnType("nvarchar(30)").IsRequired().HasMaxLength(30);
            builder.Property(x => x.StateProvince).HasColumnName(@"StateProvince").HasColumnType("nvarchar(50)").IsRequired().HasMaxLength(50);
            builder.Property(x => x.CountryRegion).HasColumnName(@"CountryRegion").HasColumnType("nvarchar(50)").IsRequired().HasMaxLength(50);
            builder.Property(x => x.PostalCode).HasColumnName(@"PostalCode").HasColumnType("nvarchar(15)").IsRequired().HasMaxLength(15);
            builder.Property(x => x.Rowguid).HasColumnName(@"rowguid").HasColumnType("uniqueidentifier").IsRequired();
            builder.Property(x => x.ModifiedDate).HasColumnName(@"ModifiedDate").HasColumnType("datetime").IsRequired();

            builder.HasIndex(x => x.Rowguid).HasDatabaseName("AK_Address_rowguid").IsUnique();
            builder.HasIndex(x => new { x.AddressLine1, x.AddressLine2, x.City, x.StateProvince, x.PostalCode, x.CountryRegion }).HasDatabaseName("IX_Address_AddressLine1_AddressLine2_City_StateProvince_PostalCode_CountryRegion");
            builder.HasIndex(x => x.StateProvince).HasDatabaseName("IX_Address_StateProvince");
        }
    }

}
// </auto-generated>
