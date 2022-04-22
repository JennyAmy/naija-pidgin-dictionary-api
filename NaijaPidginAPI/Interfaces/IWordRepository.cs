using NaijaPidginAPI.DTOs;
using NaijaPidginAPI.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NaijaPidginAPI.Interfaces
{
    public interface IWordRepository
    {
        void AddWord(Word word);

        void DeleteWord(int id);

        Task<IEnumerable<Word>> GetWordsAync();
        Task<Word> GetWordByIdAsync(int id);
    }
}
