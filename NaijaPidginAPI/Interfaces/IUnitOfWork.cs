using System.Threading.Tasks;

namespace NaijaPidginAPI.Interfaces
{
    public interface IUnitOfWork
    {
        IWordRepository WordRepository { get; }

        IUserRepository UserRepository { get; }

        Task<bool> SaveAsync();
    }
}
