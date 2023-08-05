using SampleDAL;
using SampleRepository.Common;

namespace SampleRepository.Repositories;

/// <summary>
/// Customer Repository
/// </summary>
public class CustomerRepository : RepositoryBase<SalesLT_Customer, MyDbContext>, ICustomerRepository
{
    public CustomerRepository(MyDbContext context) : base(context)
    {
    }
}