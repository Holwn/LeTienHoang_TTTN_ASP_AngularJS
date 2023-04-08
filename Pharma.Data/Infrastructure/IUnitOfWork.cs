namespace Pharma.Data.Infrastructure
{
    public interface IUnitOfWork
    {
        void Commit();
    }
}