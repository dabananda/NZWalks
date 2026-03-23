using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repositories
{
    public interface IWalkRepository
    {
        Task<IEnumerable<Walk>> GetWalks(string? filterOn = null, string? filterQuery = null);
        Task<Walk> GetWalk(Guid id);
        Task<Walk> AddWalk(Walk walk);
        Task<Walk> UpdateWalk(Guid id, Walk walk);
        Task<Walk> DeleteWalk(Guid id);
    }
}
