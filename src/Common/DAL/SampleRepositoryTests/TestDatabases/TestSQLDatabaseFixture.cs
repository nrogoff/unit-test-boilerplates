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

                    context.SalesLT_Customers.AddRange(
                        new SalesLT_Customer()
                        {
                            FirstName = "John",
                            LastName = "Doe",
                            CompanyName = "Acme",
                            EmailAddress = "",
                            NameStyle = true,
                            PasswordSalt = "123",
                            PasswordHash = "123",
                            ModifiedDate = DateTime.Now,
                        },
                        new SalesLT_Customer()
                        {
                            FirstName = "Jane",
                            LastName = "Doe",
                            CompanyName = "Acme",
                            EmailAddress = "",
                            NameStyle = true,
                            PasswordSalt = "123",
                            PasswordHash = "123",
                            ModifiedDate = DateTime.Now,
                        }
                        );
                    context.SaveChanges();
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
    
}