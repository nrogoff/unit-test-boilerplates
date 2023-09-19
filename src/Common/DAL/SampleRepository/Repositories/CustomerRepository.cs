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

    /// <summary>
    /// This is an example query that returns a list of customers as IQueryable.
    /// We have ignored the orderBy parameter for simplicity.
    /// </summary>
    /// <param name="top">The number of records to take</param>
    /// <param name="skip">The number of records to skip</param>
    /// <param name="filter">A term to wildcard search on LastName</param>
    /// <param name="orderBy">IGNORED for simplicity</param>
    /// <returns>An IQueryable List of Customers</returns>
    public IQueryable<SalesLT_Customer> GetCustomers(int top, int skip, string filter, string orderBy)
    {
        return Context.SalesLT_Customers
            .Where(c => c.LastName.Contains(filter))
            .OrderBy(c => c.LastName)
            .ThenBy(c => c.FirstName)
            .Skip(skip).Take(top);
    }

    /// <summary>
    /// This is an example query that returns a count of customers as IQueryable.
    /// </summary>
    /// <param name="filter">A term wildcard search on LastName</param>
    /// <returns></returns>
    public IQueryable<SalesLT_Customer> CountCustomers(string filter)
    {
        return Context.SalesLT_Customers
            .Where(c => c.LastName.Contains(filter));
    }
}