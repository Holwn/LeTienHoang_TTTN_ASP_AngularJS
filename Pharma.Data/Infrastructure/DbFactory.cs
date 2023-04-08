namespace Pharma.Data.Infrastructure
{
    public class DbFactory : Disposable, IDbFactory
    {
        private PharmaDbContext dbContext;

        public PharmaDbContext Init()
        {
            return dbContext ?? (dbContext = new PharmaDbContext());
        }

        protected override void DisposeCore()
        {
            if (dbContext != null)
                dbContext.Dispose();
        }
    }
}