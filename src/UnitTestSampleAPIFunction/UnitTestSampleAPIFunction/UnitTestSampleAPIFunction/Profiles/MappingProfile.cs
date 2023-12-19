using AutoMapper;
using Contracts;
using SampleDAL;

namespace UnitTestSampleAPIFunction.Profiles;

/// <summary>
/// This is an AutoMapper Profile class.
/// </summary>
public class MappingProfile : Profile
{
    public MappingProfile()
    {
        // Create a mapping from SalesLT_Customer to CustomerDto
        CreateMap<SalesLT_Customer, CustomerDto>()
            .ForMember(dest => dest.CustomerAddresses, opt => opt.MapFrom(src => src.SalesLT_CustomerAddresses));

        // Create a mapping from SalesLT_CustomerAddress to CustomerAddressDto
        CreateMap<SalesLT_CustomerAddress, CustomerAddressDto>()
            .IncludeMembers(src => src.SalesLT_Address);
        
        // Create a mapping from SalesLT_Address to CustomerAddressDto
        CreateMap<SalesLT_Address, CustomerAddressDto>(MemberList.None);
    }
}