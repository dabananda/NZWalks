using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repositories
{
    public interface IDifficultyRepository
    {
        Task<Difficulty> GetDifficultyById(Guid id);
        Task<IEnumerable<Difficulty>> GetAllDifficulties();
        Task<Difficulty> AddDifficulty(Difficulty difficulty);
        Task<Difficulty> UpdateDifficulty(Guid id, Difficulty difficulty);
        Task<Difficulty> DeleteDifficulty(Guid id);
    }
}
