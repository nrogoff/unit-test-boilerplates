using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Abstractions;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Resolvers;
using Newtonsoft.Json.Serialization;

namespace Contracts;

/// <summary>
/// Data Transfer Object (DTO) for Customer
/// </summary>
[OpenApiExample(typeof(CustomerDtoExample))]
public class CustomerDto
{
    [OpenApiProperty(Description = "Unique Customer ID")]
    public int CustomerId { get; set; } // CustomerID (Primary key)

    [OpenApiProperty(Description = "Customer Title")]
    public string Title { get; set; } // Title (length: 8)

    [OpenApiProperty(Description = "Customer First Name")]
    public string FirstName { get; set; } // FirstName (length: 50)

    [OpenApiProperty(Description = "Customer Middle Name")]
    public string MiddleName { get; set; } // MiddleName (length: 50)

    [OpenApiProperty(Description = "Customer Last Name")]
    public string LastName { get; set; } // LastName (length: 50)

    [OpenApiProperty(Description = "Customer Suffix")]
    public string Suffix { get; set; } // Suffix (length: 10)

    [OpenApiProperty(Description = "Customer Company Name")]
    public string CompanyName { get; set; } // CompanyName (length: 128)

    [OpenApiProperty(Description = "Customer Sales Person")]
    public string SalesPerson { get; set; } // SalesPerson (length: 256)

    [OpenApiProperty(Description = "Customer Email Address")]
    public string EmailAddress { get; set; } // EmailAddress (length: 50)

    [OpenApiProperty(Description = "Customer Phone Number")]
    public string Phone { get; set; } // Phone (length: 25)

    /// <summary>
    /// A flattened list of Customer Addresses
    /// </summary>
    [OpenApiProperty(Description = "A flattened list of Customer Addresses")]
    public ICollection<CustomerAddressDto> CustomerAddresses { get; set; } // CustomerAddress

    [OpenApiProperty(Description = "Customer RowGuid used to validate changes")]
    public Guid Rowguid { get; set; } // rowguid

    [OpenApiProperty(Description = "Customer Modified Date")]
    public DateTime ModifiedDate { get; set; } // ModifiedDate
}

/// <summary>
/// An OpenAPI example for <see cref="CustomerDto"/>
/// </summary>
public class CustomerDtoExample : OpenApiExample<CustomerDto>
{
    public override IOpenApiExample<CustomerDto> Build(NamingStrategy namingStrategy = null)
    {
        this.Examples.Add(OpenApiExampleResolver.Resolve("Example 1", new CustomerDto
        {
            CustomerId = 1,
            Title = "Mr.",
            FirstName = "John",
            MiddleName = "A",
            LastName = "Smith",
            Suffix = "Jr.",
            CompanyName = "Contoso Ltd.",
            SalesPerson = "John Smith",
            EmailAddress = "john.smith@email.com",
            Phone = "123-456-7890",
            CustomerAddresses = new List<CustomerAddressDto>
            {
                new CustomerAddressDto
                {
                    AddressType = "Home",
                    AddressId = 1,
                    AddressLine1 = "123 Main Street",
                    AddressLine2 = "Apt 4B",
                    City = "New York",
                    StateProvince = "New York",
                    CountryRegion = "USA",
                    PostalCode = "10001"
                }
            },
            Rowguid = Guid.NewGuid(),
            ModifiedDate = DateTime.UtcNow
        }));
        return this;
    }
}