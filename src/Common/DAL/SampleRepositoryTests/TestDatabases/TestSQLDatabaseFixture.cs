using Bogus;
using FizzWare.NBuilder;
using Microsoft.EntityFrameworkCore;
using SampleDAL;

namespace SampleRepositoryTests.TestDatabases;

/// <summary>
/// The TestSqlDatabaseFixture class is used to create a SQL database for testing.
/// </summary>
public class TestSqlDatabaseFixture
{
    private const string ConnectionString = @"Server=(localdb)\mssqllocaldb;Database=EFUnitTestDb;Trusted_Connection=True";

    private static readonly object _lock = new();
    private static bool _databaseInitialized;

    /// <summary>
    /// The constructor for the TestSqlDatabaseFixture class.
    /// </summary>
    public TestSqlDatabaseFixture()
    {
        // To ensure we don't get any threading issues, we will lock the object while we are dropping and creating the database.
        lock (_lock)
        {
            if (!_databaseInitialized)
            {
                using (var context = CreateContext())
                {
                    context.Database.EnsureDeleted();
                    context.Database.EnsureCreated();

                    SeedCustomerData(context);
                }

                _databaseInitialized = true;
            }
        }
    }
    public MyDbContext CreateContext()
            => new MyDbContext(
                new DbContextOptionsBuilder<MyDbContext>()
                    .UseSqlServer(ConnectionString)
                    .Options);

    #region Seed Customer Data

    private static void SeedCustomerData(MyDbContext context)
    {
        // Set the randomizer seed if you wish to generate repeatable data sets.
        Randomizer.Seed = new Random(8675309);

        // Create a Bogus Faker for Address
        var addressFaker = new Faker<SalesLT_Address>()
            .RuleFor(a => a.AddressLine1, f => f.Address.StreetAddress())
            .RuleFor(a => a.AddressLine2, f => f.Address.SecondaryAddress().OrNull(f, 0.5F))
            .RuleFor(a => a.City, f => f.Address.City())
            .RuleFor(a => a.StateProvince, f => f.Address.State())
            .RuleFor(a => a.CountryRegion, f => f.Address.Country())
            .RuleFor(a => a.PostalCode, f => f.Address.ZipCode())
            .RuleFor(a => a.ModifiedDate, f => f.Date.Past());

        var addressList = addressFaker.Generate(50);


        // Create a Bogus Faker for CustomerAddress
        var customerAddressFaker = new Faker<SalesLT_CustomerAddress>()
            .RuleFor(c => c.AddressType, f => f.PickRandomParam("Home", "Work", "Other"))
            .RuleFor(c => c.ModifiedDate, f => f.Date.Past())
            //add a rule to generate a random address
            .RuleFor(c => c.SalesLT_Address, f => f.PickRandom(addressList));

        // Create a Bogus Faker for Customer
        var customerFaker = new Faker<SalesLT_Customer>()
            .RuleFor(c => c.Title, f => f.Name.Prefix())
            .RuleFor(c => c.FirstName, f => f.Person.FirstName)
            .RuleFor(c => c.MiddleName, f => f.Name.FirstName().OrNull(f, 0.5F))
            .RuleFor(c => c.LastName, f => f.Person.LastName)
            .RuleFor(c => c.Suffix, f => f.Name.Suffix().OrNull(f, 0.9F))
            .RuleFor(c => c.CompanyName, f => f.Company.CompanyName())
            .RuleFor(c => c.EmailAddress, f => f.Person.Email)
            .RuleFor(c => c.NameStyle, f => f.Random.Bool())
            .RuleFor(c => c.PasswordSalt, f => f.Random.AlphaNumeric(10))
            .RuleFor(c => c.PasswordHash, f => f.Random.AlphaNumeric(10))
            .RuleFor(c => c.ModifiedDate, f => f.Date.Past())
            .RuleFor(c => c.Phone, f => f.Phone.PhoneNumber())
            .RuleFor(c => c.SalesPerson, f => f.Person.FullName)
            .RuleFor(c => c.SalesLT_CustomerAddresses, f => customerAddressFaker.Generate(1).OrNull(f, 0.1F));



        var customersList = customerFaker.Generate(500);

        context.SalesLT_Customers.AddRange(customersList);


        context.SaveChanges();
    }


    #endregion
    
}