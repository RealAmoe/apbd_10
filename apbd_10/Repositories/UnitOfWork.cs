using apbd_10.Context;

namespace apbd_10.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private MedicalContext _medicalContext;

    public UnitOfWork(MedicalContext medicalContext)
    {
        _medicalContext = medicalContext;
    }
    public async Task BeginTransactionAsync()
    {
        await _medicalContext.Database.BeginTransactionAsync();
    }

    public async Task CommitTransactionAsync()
    {
        await _medicalContext.Database.CommitTransactionAsync();
    }

    public async Task RollbackTransactionAsync()
    {
        await _medicalContext.Database.RollbackTransactionAsync();
    }
}