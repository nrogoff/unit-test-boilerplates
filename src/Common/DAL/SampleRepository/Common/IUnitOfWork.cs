namespace SampleRepository.Common;

public class IUnitOfWork
{
    
}

class UnitOfWork : IUnitOfWork, IDisposable
{
    protected virtual void Dispose(bool disposing)
    {
        if (disposing)
        {
        }
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
}