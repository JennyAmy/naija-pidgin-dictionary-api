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
        Task<IEnumerable<Word>> GetNotApprovedWordsAync();

        Task<IEnumerable<Word>> GetApprovedWordsAync();
        Task<Word> GetApprovedWordByIdAsync(int id);
        Task<Word> GetNotApprovedWordByIdAsync(int id);
        Task<Word> GetWordByIdAsync(int id);
        Task<bool> WordAlreadyExists(string word, int? id);
        int CountApprovedWordsByUserId(int userId);
        int CountTotalWordsByUserId(int userId);
        Task<IEnumerable<Word>> GetAllWordsByUserIdAsync(int userId);
        int CountTotalWords();
        Task<IEnumerable<Word>> SearchWords(string keyword);
    }
}
