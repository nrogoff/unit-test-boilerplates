using FluentAssertions;
using SampleRepository.Repositories;
using SampleRepositoryTests.TestDatabases;

namespace SampleRepositoryTests
{
    /// <summary>
    /// This is an exmaple of a repository test. Here we are using the xUnit fixture to create a SQL database to test against
    /// </summary>
    public class CustomerRepositoryTests : IClassFixture<TestSqlDatabaseFixture>
    {
        public CustomerRepositoryTests(TestSqlDatabaseFixture fixture)
            => Fixture = fixture;

        public TestSqlDatabaseFixture Fixture { get; }

        [Fact]
        public async Task GetAllRecords_Customers_Expect500()
        {
            // Arrange
            using var context = Fixture.CreateContext();
            var customerRepository = new CustomerRepository(context);

            // Act
            var customers = await customerRepository.GetAllAsync();

            // Assert
            customers.Should().NotBeNull();
            customers.Should().HaveCount(500);
        }
    }
}