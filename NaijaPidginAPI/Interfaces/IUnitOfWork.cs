using System.Threading.Tasks;

namespace NaijaPidginAPI.Interfaces
{
    public interface IUnitOfWork
    {
        IWordRepository WordRepository { get; }

        Task<bool> SaveAsync();
    }
}
