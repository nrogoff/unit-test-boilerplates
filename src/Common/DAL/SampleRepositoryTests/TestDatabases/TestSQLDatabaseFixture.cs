using Bogus;
using FizzWare.NBuilder;
using Microsoft.EntityFrameworkCore;
using SampleDAL;

namespace SampleRepositoryTests.TestDatabases;

public class TestSqlDatabaseFixture
{
    private const string ConnectionString = @"Server=(localdb)\mssqllocaldb;Database=EFUnitTestDb;Trusted_Connection=True";

    private static readonly object _lock = new();
    private static bool _databaseInitialized;

    public TestSqlDatabaseFixture()
    {
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
        // Create a Bogus Faker for Customer
        var customerFaker = new Faker<SalesLT_Customer>()
            .RuleFor(c => c.FirstName, f => f.Person.FirstName)
            .RuleFor(c => c.LastName, f => f.Person.LastName)
            .RuleFor(c => c.CompanyName, f => f.Company.CompanyName())
            .RuleFor(c => c.EmailAddress, f => f.Person.Email)
            .RuleFor(c => c.NameStyle, f => f.Random.Bool())
            .RuleFor(c => c.PasswordSalt, f => f.Random.AlphaNumeric(10))
            .RuleFor(c => c.PasswordHash, f => f.Random.AlphaNumeric(10))
            .RuleFor(c => c.ModifiedDate, f => f.Date.Past());

        var customersList = customerFaker.Generate(500);

        context.SalesLT_Customers.AddRange(customersList);


        //// Use nbuilder to create a list of 500 customers
        //var customers = Builder<SalesLT_Customer>
        //    .CreateListOfSize(500)
        //    .All()
        //    .Build();

        //context.SalesLT_Customers.AddRange(
        //               new SalesLT_Customer()
        //               {
        //        FirstName = "John",
        //        LastName = "Doe",
        //        CompanyName = "Acme",
        //        EmailAddress = "",
        //        NameStyle = true,
        //        PasswordSalt = "123",
        //        PasswordHash = "123",
        //        ModifiedDate = DateTime.Now,
        //    },
        //                          new SalesLT_Customer()
        //                          {
        //        FirstName = "Jane",
        //        LastName = "Doe",
        //        CompanyName = "Acme",
        //        EmailAddress = "",
        //        NameStyle = true,
        //        PasswordSalt = "123",
        //        PasswordHash = "123",
        //        ModifiedDate = DateTime.Now,
        //    }
        //                          );
        context.SaveChanges();
    }


    #endregion
    
}