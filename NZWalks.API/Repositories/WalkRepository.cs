using Microsoft.EntityFrameworkCore;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repositories
{
    public class WalkRepository : IWalkRepository
    {
        private readonly NZWalksDbContext nZWalksDbContext;

        public WalkRepository(NZWalksDbContext nZWalksDbContext)
        {
            this.nZWalksDbContext = nZWalksDbContext;
        }

        public async Task<Walk> AddWalk(Walk walk)
        {
            await nZWalksDbContext.Walks.AddAsync(walk);
            await nZWalksDbContext.SaveChangesAsync();
            return walk;
        }

        public async Task<Walk> DeleteWalk(Guid id)
        {
            var walk = await nZWalksDbContext.Walks.FindAsync(id);
            if (walk == null)
            {
                return null;
            }
            nZWalksDbContext.Walks.Remove(walk);
            await nZWalksDbContext.SaveChangesAsync();
            return walk;
        }

        public async Task<Walk> GetWalk(Guid id)
        {
            return await nZWalksDbContext.Walks.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IEnumerable<Walk>> GetWalks()
        {
            return await nZWalksDbContext.Walks.ToListAsync();
        }

        public async Task<Walk> UpdateWalk(Guid id, Walk walk)
        {
            var walkDomain = await nZWalksDbContext.Walks.FirstOrDefaultAsync(x => x.Id == id);
            if (walkDomain == null)
            {
                return null;
            }

            walkDomain.Name = walk.Name;
            walkDomain.Description = walk.Description;
            walkDomain.LengthInKm = walk.LengthInKm;
            walkDomain.WalkImageUrl = walk.WalkImageUrl;
            walkDomain.DifficultyId = walk.DifficultyId;
            walkDomain.RegionId = walk.RegionId;

            await nZWalksDbContext.SaveChangesAsync();

            return walk;
        }
    }
}
