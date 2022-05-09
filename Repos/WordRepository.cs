using Microsoft.EntityFrameworkCore;
using NaijaPidginAPI.DbContexts;
using NaijaPidginAPI.Entities;
using NaijaPidginAPI.Interfaces;
using System;
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

        public async Task<IEnumerable<Word>> GetApprovedWordsAync()
        {
            var words = await context.Words
                .Include(w => w.WordClass)
                .Where(w => w.IsApproved == true)
                .ToListAsync();

            return words;
        }

        public async Task<IEnumerable<Word>> GetNotApprovedWordsAync()
        {
            var words = await context.Words
                .Include(w => w.WordClass)
                .Where(w => w.IsApproved == false)
                .ToListAsync();

            return words;
        }

        public async Task<Word> GetApprovedWordByIdAsync(int id)
        {
            var word = await context.Words
                .Include(w => w.WordClass)
                .Where(w => w.WordId == id && w.IsApproved == true).FirstOrDefaultAsync();

            return word;
        }

        public async Task<Word> GetNotApprovedWordByIdAsync(int id)
        {
            var word = await context.Words
                .Include(w => w.WordClass)
                .Where(w => w.WordId == id && w.IsApproved == false).FirstOrDefaultAsync();

            return word;
        }

        public async Task<Word> GetWordByIdAsync(int id)
        {
            var word = await context.Words
                .Include(w => w.WordClass)
                .Where(w => w.WordId == id).FirstOrDefaultAsync();

            return word;
        }

        public async Task<IEnumerable<Word>> GetAllWordsByUserIdAsync(int userId)
        {
            var words = await context.Words
                .Include(w => w.WordClass)
                .Where(w => w.PostedBy == userId).ToListAsync();

            return words;
        }


        public void DeleteWord(int id)
        {
            var word = context.Words.Find(id);
            context.Words.Remove(word);
        }

        public async Task<bool> WordAlreadyExists(string word, int? id)
        {
            return await context.Words.AnyAsync(x => x.WordInput == word && x.WordId != id);
        }

        public int CountApprovedWordsByUserId(int userId)
        {
            var totalWords = context.Words
                .Include(w => w.WordClass)
                .Where(w => w.PostedBy == userId && w.IsApproved == true).Count();

            return totalWords;
        }

        public int CountTotalWordsByUserId(int userId)
        {
            var totalWords = context.Words
                .Include(w => w.WordClass)
                .Where(w => w.PostedBy == userId).Count();

            return totalWords;
        }

        public int CountTotalWords()
        {
            var totalWords = context.Words
                .Include(w => w.WordClass).Count();

            return totalWords;
        }

        public async Task<IEnumerable<Word>> SearchWords(string keyword)
        {
            var words = await context.Words
                .Where(w => w.IsApproved == true && w.WordInput.Contains(keyword))
                .OrderBy(w => w.WordInput)
                .Select(w => new Word { WordId = w.WordId, WordInput = w.WordInput })
                .Take(10)
                .ToListAsync();

            return words;
        }
    }
}
