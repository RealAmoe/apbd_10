namespace apbd_10.Repositories;

public interface IUnitOfWork
{
    public Task BeginTransactionAsync();
    public Task CommitTransactionAsync();
    public Task RollbackTransactionAsync();
}