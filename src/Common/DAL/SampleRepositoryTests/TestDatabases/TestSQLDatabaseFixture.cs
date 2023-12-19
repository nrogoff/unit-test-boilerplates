using Bogus;
// Add regionalization UK support for the Bogus Faker
using Bogus.Extensions.UnitedKingdom;
using Microsoft.EntityFrameworkCore;
using SampleDAL;

namespace SampleRepositoryTests.TestDatabases;

/// <summary>
/// The TestSqlDatabaseFixture class is used to create a SQL database for testing.
/// </summary>
public class TestSqlDatabaseFixture
{
    private const string ConnectionString = @"Server=(localdb)\MSSQLLocalDB;Database=EFUnitTestDb;Trusted_Connection=True";

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

                    // Seed the database with test data
                    SeedCustomerData(context);
                    // You can seed everything in one go or split the seeding into separate methods.
                    // This can allow you to create a set of targeted test data that can be used for a subset tests.
                    //SeedProductData(context);
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

    #region Seeding Data

    /// <summary>
    /// Seed the database with test data for the Customer and Address table.
    /// </summary>
    /// <param name="context"></param>
    private static void SeedCustomerData(MyDbContext context)
    {
        // Set the randomizer seed if you wish to generate repeatable data sets.
        Randomizer.Seed = new Random(8675309);

        // Create a Bogus Faker for Address
        var addressFaker = new Faker<SalesLT_Address>()
            .RuleFor(a => a.Rowguid, f => Guid.NewGuid())
            .RuleFor(a => a.AddressLine1, f => f.Address.StreetAddress())
            .RuleFor(a => a.AddressLine2, f => f.Address.SecondaryAddress().OrNull(f, 0.5F))
            .RuleFor(a => a.City, f => f.Address.City())
            .RuleFor(a => a.StateProvince, f => f.Address.State())
            .RuleFor(a => a.CountryRegion, f => f.Address.CountryOfUnitedKingdom())
            .RuleFor(a => a.PostalCode, f => f.Address.ZipCode())
            .RuleFor(a => a.ModifiedDate, f => f.Date.Past());

        var addressList = addressFaker.Generate(50);
        context.SalesLT_Addresses.AddRange(addressList);
        context.SaveChanges();

        // Create a Bogus Faker for CustomerAddress
        var customerAddressFaker = new Faker<SalesLT_CustomerAddress>()
            .RuleFor(c => c.Rowguid, f => Guid.NewGuid())
            .RuleFor(c => c.AddressType, f => f.PickRandomParam("Home", "Work", "Other"))
            .RuleFor(c => c.ModifiedDate, f => f.Date.Past())
            //add a rule to generate a random address
            .RuleFor(c => c.SalesLT_Address, f => f.PickRandom(addressList));

        // Create a Bogus Faker for Customer
        var customerFaker = new Faker<SalesLT_Customer>()
            .RuleFor(c => c.Rowguid, f => Guid.NewGuid())
            .RuleFor(c => c.Title, f => f.Name.Prefix().OrNull(f, 0.2F))
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

    /// <summary>
    /// Seed the database with test data for the Product table.
    /// </summary>
    /// <param name="context"></param>
    private static void SeedProductData(MyDbContext context)
    {

        string xmlSample =
            $"<?xml-stylesheet href=\"ProductDescription.xsl\" type=\"text/xsl\"?><p1:ProductDescription xmlns:p1=\"http://schemas.microsoft.com/sqlserver/2004/07/adventure-works/ProductModelDescription\" xmlns:wm=\"http://schemas.microsoft.com/sqlserver/2004/07/adventure-works/ProductModelWarrAndMain\" xmlns:wf=\"http://www.adventure-works.com/schemas/OtherFeatures\" xmlns:html=\"http://www.w3.org/1999/xhtml\" ProductModelID=\"19\" ProductModelName=\"Mountain 100\"><p1:Summary><html:p>Our top-of-the-line competition mountain bike.\r\n \t\t\t\tPerformance-enhancing options include the innovative HL Frame,\r\n\t\t\t\tsuper-smooth front suspension, and traction for all terrain.\r\n                        </html:p></p1:Summary><p1:Manufacturer><p1:Name>AdventureWorks</p1:Name><p1:Copyright>2002</p1:Copyright><p1:ProductURL>HTTP://www.Adventure-works.com</p1:ProductURL></p1:Manufacturer><p1:Features>These are the product highlights.\r\n                 <wm:Warranty><wm:WarrantyPeriod>3 years</wm:WarrantyPeriod><wm:Description>parts and labor</wm:Description></wm:Warranty><wm:Maintenance><wm:NoOfYears>10 years</wm:NoOfYears><wm:Description>maintenance contract available through your dealer or any AdventureWorks retail store.</wm:Description></wm:Maintenance><wf:wheel>High performance wheels.</wf:wheel><wf:saddle><html:i>Anatomic design</html:i> and made from durable leather for a full-day of riding in comfort.</wf:saddle><wf:pedal><html:b>Top-of-the-line</html:b> clipless pedals with adjustable tension.</wf:pedal><wf:BikeFrame>Each frame is hand-crafted in our Bothell facility to the optimum diameter\r\n\t\t\t\tand wall-thickness required of a premium mountain frame.\r\n\t\t\t\tThe heat-treated welded aluminum frame has a larger diameter tube that absorbs the bumps.</wf:BikeFrame><wf:crankset> Triple crankset; alumunim crank arm; flawless shifting. </wf:crankset></p1:Features><!-- add one or more of these elements... one for each specific product in this product model --><p1:Picture><p1:Angle>front</p1:Angle><p1:Size>small</p1:Size><p1:ProductPhotoID>118</p1:ProductPhotoID></p1:Picture><!-- add any tags in <specifications> --><p1:Specifications> These are the product specifications.\r\n                   <Material>Almuminum Alloy</Material><Color>Available in most colors</Color><ProductLine>Mountain bike</ProductLine><Style>Unisex</Style><RiderExperience>Advanced to Professional riders</RiderExperience></p1:Specifications></p1:ProductDescription>";

        // Set the randomizer seed if you wish to generate repeatable data sets.
        Randomizer.Seed = new Random(8675309);

        // Create the top level Product Categories
        var productCategoryList = new List<SalesLT_ProductCategory>
        {
            new SalesLT_ProductCategory { Name = "Bikes", ModifiedDate = DateTime.Now, Rowguid = Guid.NewGuid()},
            new SalesLT_ProductCategory { Name = "Components", ModifiedDate = DateTime.Now, Rowguid = Guid.NewGuid() },
            new SalesLT_ProductCategory { Name = "Clothing", ModifiedDate = DateTime.Now, Rowguid = Guid.NewGuid() },
            new SalesLT_ProductCategory { Name = "Accessories", ModifiedDate = DateTime.Now, Rowguid = Guid.NewGuid() }
        };
        context.SalesLT_ProductCategories.AddRange(productCategoryList);
        context.SaveChanges();

        // Create a Bogus Faker for Product Category 2nd level
        var productCategoryFaker = new Faker<SalesLT_ProductCategory>()
            .RuleFor(c => c.Rowguid, f => Guid.NewGuid())
            .RuleFor(c => c.Name, f => f.Commerce.Categories(1).First())
            .RuleFor(c => c.ParentProductCategory, f => f.PickRandom(context.SalesLT_ProductCategories.ToList()))
            .RuleFor(c => c.ModifiedDate, f => f.Date.Past());
        var productCategoriesList = productCategoryFaker.Generate(40);

        // Create a Bogus Faker for Product Model
        var productModelFaker = new Faker<SalesLT_ProductModel>()
            .RuleFor(m => m.Rowguid, f => Guid.NewGuid())
            .RuleFor(m => m.Name, f => f.Commerce.ProductName())
            .RuleFor(m => m.CatalogDescription, f => f.Lorem.Paragraph())
            .RuleFor(m => m.ModifiedDate, f => f.Date.Past());
        var productModelList = productModelFaker.Generate(40);

        var productDescriptionFaker = new Faker<SalesLT_ProductDescription>()
            .RuleFor(d => d.Rowguid, f => Guid.NewGuid())
            .RuleFor(d => d.Description, f => f.Lorem.Paragraph())
            .RuleFor(d => d.ModifiedDate, f => f.Date.Past());
        var productDescriptionList = productDescriptionFaker.Generate(40);

        var productDescriptionDeFaker = new Faker<SalesLT_ProductDescription>("de")
            .RuleFor(d => d.Rowguid, f => Guid.NewGuid())
            .RuleFor(d => d.Description, f => f.Lorem.Paragraph())
            .RuleFor(d => d.ModifiedDate, f => f.Date.Past());
        var productDescriptionDeList = productDescriptionFaker.Generate(40);

        var productModelProductDescriptionFaker = new Faker<SalesLT_ProductModelProductDescription>()
            .RuleFor(m => m.Rowguid, f => Guid.NewGuid())
            .RuleFor(m => m.SalesLT_ProductModel, f => f.PickRandom(productModelList))
            .RuleFor(m => m.SalesLT_ProductDescription, f => f.PickRandom(productDescriptionList))
            .RuleFor(m => m.Culture, f => f.PickRandomParam("en", "fr", "es", "ja", "zh", "ru", "sv", "nl", "it", "pl", "fi", "da", "el", "no", "pt", "ar", "tr", "he", "id", "hu", "cs", "ko", "th", "ca", "vi", "ro", "sk", "uk", "hr", "hi", "lt", "sl", "bg", "sr", "et", "lv", "fa", "bs", "is", "mk", "ms", "sw", "sq", "cy", "ur", "eu", "ga", "hy", "kk", "ku", "mi", "mt", "qu", "te", "be", "tg", "tk", "uz", "xh", "am", "az", "bn", "bo", "dz", "gl", "gu", "ha", "ig", "kn", "km", "lo", "mn", "ne", "or", "pa", "rw", "si", "so", "ti", "ug", "wo", "yo", "zu").OrNull(f, 0.1F))
            .RuleFor(m => m.ModifiedDate, f => f.Date.Past());  
        var productModelProductDescriptionEnList = productModelProductDescriptionFaker.Generate(40);
        context.SalesLT_ProductModelProductDescriptions.AddRange(productModelProductDescriptionEnList);
        context.SaveChanges();

        var productModelProductDescriptionDeFaker = new Faker<SalesLT_ProductModelProductDescription>()
            .RuleFor(m => m.Rowguid, f => Guid.NewGuid())
            .RuleFor(m => m.SalesLT_ProductModel, f => f.PickRandom(productModelList))
            .RuleFor(m => m.SalesLT_ProductDescription, f => f.PickRandom(productDescriptionDeList))
            .RuleFor(m => m.Culture, f => f.PickRandomParam("de").OrNull(f, 0.1F))
            .RuleFor(m => m.ModifiedDate, f => f.Date.Past());  
        var productModelProductDescriptionDeList = productModelProductDescriptionFaker.Generate(40);
        context.SalesLT_ProductModelProductDescriptions.AddRange(productModelProductDescriptionDeList);
        context.SaveChanges();

        // Create a Bogus Faker for Product
        var productFaker = new Faker<SalesLT_Product>()
            .RuleFor(p => p.Rowguid, f => Guid.NewGuid())
            .RuleFor(p => p.Name, f => f.Commerce.ProductName())
            .RuleFor(p => p.ProductNumber, f => f.Commerce.Ean13())
            .RuleFor(p => p.Color, f => f.Commerce.Color())
            .RuleFor(p => p.Size, f => f.PickRandomParam("S", "M", "L", "XL", "XXL").OrNull(f, 0.1F))
            .RuleFor(p => p.Weight, f => f.Random.Decimal(0.1M, 100M))
            .RuleFor(p => p.SalesLT_ProductCategory, f => f.PickRandom(productCategoryList))
            .RuleFor(p => p.SalesLT_ProductModel, f => f.PickRandom(productModelList))
            .RuleFor(p => p.SellStartDate, f => f.Date.Past())
            .RuleFor(p => p.SellEndDate, f => f.Date.Future().OrNull(f, 0.2F))
            .RuleFor(p => p.DiscontinuedDate, f => f.Date.Past().OrNull(f, 0.5F))
            .RuleFor(p => p.ModifiedDate, f => f.Date.Past());
        var productList = productFaker.Generate(200);

        context.SalesLT_Products.AddRange(productList);
        context.SaveChanges();

    }

    #endregion
    
}