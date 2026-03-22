using Microsoft.EntityFrameworkCore;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repositories
{
    public class DifficultyRepository : IDifficultyRepository
    {
        private readonly NZWalksDbContext nZWalksDbContext;

        public DifficultyRepository(NZWalksDbContext nZWalksDbContext)
        {
            this.nZWalksDbContext = nZWalksDbContext;
        }

        public async Task<Difficulty> AddDifficulty(Difficulty difficulty)
        {
            await nZWalksDbContext.Difficulties.AddAsync(difficulty);
            await nZWalksDbContext.SaveChangesAsync();
            return difficulty;
        }

        public async Task<Difficulty> DeleteDifficulty(Guid id)
        {
            var difficulty = await nZWalksDbContext.Difficulties.FindAsync(id);
            if (difficulty == null)
            {
                return null;
            }

            nZWalksDbContext.Difficulties.Remove(difficulty);
            await nZWalksDbContext.SaveChangesAsync();

            return difficulty;
        }

        public async Task<IEnumerable<Difficulty>> GetAllDifficulties()
        {
            return await nZWalksDbContext.Difficulties.ToListAsync();
        }

        public async Task<Difficulty> GetDifficultyById(Guid id)
        {
            return await nZWalksDbContext.Difficulties.FindAsync(id);
        }

        public async Task<Difficulty> UpdateDifficulty(Guid id, Difficulty difficulty)
        {
            var difficultyDomain = await nZWalksDbContext.Difficulties.FindAsync(id);
            if (difficultyDomain == null)
            {
                return null;
            }

            difficultyDomain.Name = difficulty.Name;
            await nZWalksDbContext.SaveChangesAsync();

            return difficultyDomain;
        }
    }
}
