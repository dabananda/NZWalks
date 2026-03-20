using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repositories
{
    public interface IRegionRepository
    {
        Task<IEnumerable<Region>> GetRegionsAsync();
        Task<Region> GetRegionAsync(Guid id);
        Task<Region> AddRegionAsync(Region region);
        Task<Region> UpdateRegionAsync(Guid id, Region region);
        Task<Region> DeleteRegionAsync(Guid id);
    }
}
