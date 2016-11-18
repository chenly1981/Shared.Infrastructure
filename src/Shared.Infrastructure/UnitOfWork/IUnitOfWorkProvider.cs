namespace Shared.Infrastructure.UnitOfWork
{
    public interface IUnitOfWorkProvider
    {
        IUnitOfWork CreateUnitOfWork(string name);
    }
}
