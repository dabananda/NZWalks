using Microsoft.EntityFrameworkCore;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repositories
{
    public class RegionRepository : IRegionRepository
    {
        private readonly NZWalksDbContext _nZWalksDbContext;

        public RegionRepository(NZWalksDbContext nZWalksDbContext)
        {
            _nZWalksDbContext = nZWalksDbContext;
        }

        public async Task<Region> AddRegionAsync(Region region)
        {
            region.Id = Guid.NewGuid();
            await _nZWalksDbContext.Regions.AddAsync(region);
            await _nZWalksDbContext.SaveChangesAsync();
            return region;
        }

        public async Task<Region> DeleteRegionAsync(Guid id)
        {
            var region = await _nZWalksDbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);
            if (region == null)
            {
                return null;
            }

            _nZWalksDbContext.Regions.Remove(region);
            await _nZWalksDbContext.SaveChangesAsync();

            return region;
        }

        public async Task<Region> GetRegionAsync(Guid id)
        {
            return await _nZWalksDbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IEnumerable<Region>> GetRegionsAsync()
        {
            return await _nZWalksDbContext.Regions.ToListAsync();
        }

        public async Task<Region> UpdateRegionAsync(Guid id, Region region)
        {
            var regionDomain = await _nZWalksDbContext.Regions.FirstOrDefaultAsync(x =>x.Id == id);
            if (regionDomain == null)
            {
                return null;
            }

            regionDomain.Code = region.Code;
            regionDomain.Name = region.Name;
            regionDomain.RegionImageUrl = region.RegionImageUrl;

            await _nZWalksDbContext.SaveChangesAsync();

            return regionDomain;
        }
    }
}
