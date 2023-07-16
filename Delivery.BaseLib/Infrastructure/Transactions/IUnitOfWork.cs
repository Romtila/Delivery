namespace Delivery.BaseLib.Infrastructure.Transactions;

public interface IUnitOfWork
{
    void Commit();

    //TODO: Implement Rollback
    //void Rollback();
}