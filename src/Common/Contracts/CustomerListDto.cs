using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Abstractions;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Resolvers;
using Newtonsoft.Json.Serialization;

namespace Contracts;

/// <summary>
/// Data Transfer Object (DTO) for Customer List Response
/// </summary>
[OpenApiExample(typeof(CustomerListDtoExample))]
public class CustomerListDto
{
    /// <summary>
    /// Total count of records in the result set
    /// </summary>
    [OpenApiProperty(Nullable=false, Default = 0, Description = "Total count of records in the result set")]
    public int TotalCount { get; set; }

    /// <summary>
    /// List of Customers in this page
    /// </summary>
    [OpenApiProperty(Description = "List of Customers and their addresses")]
    public List<CustomerDto> Customers { get; set; }

    /// <summary>
    /// Link to the next page
    /// </summary>
    [OpenApiProperty(Description = "Link to the next page")]
    public string NextLink { get; set; }
}

/// <summary>
/// An OpenAPI example for <see cref="CustomerListDto"/>
/// </summary>
public class CustomerListDtoExample : OpenApiExample<CustomerListDto>
{
    public override IOpenApiExample<CustomerListDto> Build(NamingStrategy namingStrategy = null)
    {
        this.Examples.Add(OpenApiExampleResolver.Resolve("Example 1", new CustomerListDto
        {
            TotalCount = 1,
            Customers = new List<CustomerDto>
            {
                new CustomerDto
                {
                    CustomerId = 1,
                    Title = "Mr.",
                    FirstName = "John",
                    MiddleName = "A",
                    LastName = "Smith",
                    Suffix = "Jr",
                    CompanyName = "Contoso, Ltd.",
                    SalesPerson = "John Smith",
                    EmailAddress = "john.smith@email.com",
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
                }
            },
            NextLink = "https://localhost:7071/api/GetCustomers?top=1&skip=1"
        }));
        return this;
    }
}