using SampleDAL;
using SampleRepository.Common;

namespace SampleRepository.Repositories;

/// <summary>
/// Customer Repository Interface
/// </summary>
public interface ICustomerRepository : IRepositoryBase<SalesLT_Customer>
{
    public IQueryable<SalesLT_Customer> GetCustomers(int top, int skip, string filter, string orderBy);
}