using AutoMapper;
using UnitTestSampleAPIFunction.Profiles;
using Xunit;

namespace UnitTestSampleAPIFunctionTests.Profiles
{
    public class MappingProfileTests
    {
        /// <summary>
        /// A unit test to verify that the AutoMapper configuration is valid.
        /// </summary>
        [Fact]
        public void AutoMapper_Configuration_IsValid()
        {
            // Arrange
            var configuration = new MapperConfiguration(cfg =>
                cfg.AddProfile<MappingProfile>());

            // Assert that the mappings are valid
            configuration.AssertConfigurationIsValid();
        }
    }
}
