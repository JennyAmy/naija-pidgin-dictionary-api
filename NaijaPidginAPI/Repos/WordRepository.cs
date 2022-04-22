using Microsoft.EntityFrameworkCore;
using NaijaPidginAPI.DbContexts;
using NaijaPidginAPI.Entities;
using NaijaPidginAPI.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NaijaPidginAPI.Repos
{
    public class WordRepository : IWordRepository
    {
        private readonly DataContext context;

        public WordRepository(DataContext context)
        {
            this.context = context;
        }
        public void AddWord(Word word)
        {
            context.Words.Add(word);
        }

        public async Task<IEnumerable<Word>> GetWordsAync()
        {
            var words = await context.Words
                .Include(w => w.WordClass)
                .ToListAsync();

            return words;
        }

        public async Task<Word> GetWordByIdAsync(int id)
        {
            var word = await context.Words
                .Include(w => w.WordClass)
                .Where(w => w.WordId == id).FirstOrDefaultAsync();

            return word;
        }

        public void DeleteWord(int id)
        {
            var word = context.Words.Find(id);
            context.Words.Remove(word);
        }
    }
}
