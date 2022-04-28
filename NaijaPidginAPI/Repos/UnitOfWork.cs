using NaijaPidginAPI.DbContexts;
using NaijaPidginAPI.Interfaces;
using System.Threading.Tasks;

namespace NaijaPidginAPI.Repos
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DataContext context;

        public UnitOfWork(DataContext context)
        {
            this.context = context;
        }
        public IWordRepository WordRepository => 
            new WordRepository(context);

        public IUserRepository UserRepository =>
            new UserRepository(context);

        public async Task<bool> SaveAsync()
        {
            return await context.SaveChangesAsync() > 0;
        }
    }
}
