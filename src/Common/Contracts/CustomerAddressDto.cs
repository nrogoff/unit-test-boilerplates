using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Abstractions;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Resolvers;
using Newtonsoft.Json.Serialization;

namespace Contracts;

/// <summary>
/// Data Transfer Object (DTO) for Customer Address. This is a flattened DTO combining data from SalesLT_CustomerAddress and SalesLT_Address
/// </summary>
[OpenApiExample(typeof(CustomerAddressDtoExample))]
public class CustomerAddressDto
{
    [OpenApiProperty(Description = "Customer Address Type")]
    public string AddressType { get; set; } // AddressType (length: 50)

    [OpenApiProperty(Description = "Customer Unique Address ID")]
    public int AddressId { get; set; } // AddressID (Primary key)

    [OpenApiProperty(Description = "Address Line 1")]
    public string AddressLine1 { get; set; } // AddressLine1 (length: 60)

    [OpenApiProperty(Description = "Address Line 2")]
    public string AddressLine2 { get; set; } // AddressLine2 (length: 60)

    [OpenApiProperty(Description = "City")]
    public string City { get; set; } // City (length: 30)

    [OpenApiProperty(Description = "State or Province")]
    public string StateProvince { get; set; } // StateProvince (length: 50)

    [OpenApiProperty(Description = "Country or Region")]
    public string CountryRegion { get; set; } // CountryRegion (length: 50)

    [OpenApiProperty(Description = "Postal Code")]
    public string PostalCode { get; set; } // PostalCode (length: 15)

}

/// <summary>
/// An OpenAPI example for <see cref="CustomerAddressDto"/>
/// </summary>
public class CustomerAddressDtoExample : OpenApiExample<CustomerAddressDto>
{
    public override IOpenApiExample<CustomerAddressDto> Build(NamingStrategy namingStrategy = null)
    {
        this.Examples.Add(OpenApiExampleResolver.Resolve("Example 1", new CustomerAddressDto
        {
            AddressType = "Home",
            AddressId = 1,
            AddressLine1 = "123 Main Street",
            AddressLine2 = "Apt 4B",
            City = "New York",
            StateProvince = "New York",
            CountryRegion = "USA",
            PostalCode = "10001"
        }));

        return this;
    }
}
