using AutoMapper;
using Contracts;
using SampleDAL;
using UnitTestSampleAPIFunction.Profiles;
using Xunit;

namespace UnitTestSampleAPIFunctionTests.Profiles
{
    public class MappingProfileTests
    {

        [Fact]
        public void AutoMapper_Configuration_IsValid()
        {
            // Arrange
            var configuration = new MapperConfiguration(cfg =>
                cfg.AddProfile<MappingProfile>());

            // Assert that the mappings are valid
            configuration.AssertConfigurationIsValid();
        }

        //[Fact]
        //public void TestMapperConfig_SalesLT_Customer_CustomerDto_OK()
        //{
        //    // Arrange
        //    var configuration = new MapperConfiguration(cfg =>
        //        cfg.CreateMap<SalesLT_Customer, CustomerDto>());

        //    // Assert that the mappings are valid
        //    configuration.AssertConfigurationIsValid();
        //}

        //[Fact]
        //public void TestMapperConfig_SalesLT_CustomerAddress_CustomerAddressDto_OK()
        //{
        //    // Arrange
        //    var configuration = new MapperConfiguration(cfg =>
        //        cfg.CreateMap<SalesLT_CustomerAddress, CustomerAddressDto>());

        //    // Assert that the mappings are valid
        //    configuration.AssertConfigurationIsValid();
        //}
    }
}
