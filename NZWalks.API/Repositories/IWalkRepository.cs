using NZWalks.API.Models.Domain;
using System.Globalization;

namespace NZWalks.API.Repositories
{
    public interface IWalkRepository
    {
        Task<IEnumerable<Walk>> GetWalks(string? filterOn = null, string? filterQuery = null, string? sortBy = null, bool isAscending = true, int pageNumber = 1, int pageSize = 1000);
        Task<Walk> GetWalk(Guid id);
        Task<Walk> AddWalk(Walk walk);
        Task<Walk> UpdateWalk(Guid id, Walk walk);
        Task<Walk> DeleteWalk(Guid id);
    }
}
