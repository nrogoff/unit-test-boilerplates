using JetBrains.Annotations;
using UnitTestSampleAPIFunction.Functions;
using Xunit;

namespace UnitTestSampleAPIFunctionTests.Functions;

[TestSubject(typeof(GetCustomers))]
public class GetCustomersTest
{
    [Fact]
    public void GetCustomers_NoParams_OK()
    {
        
    }
    
}