using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO.DifficultyDtos;
using NZWalks.API.Repositories;

namespace NZWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DifficultiesController : ControllerBase
    {
        private readonly IDifficultyRepository difficultyRepository;

        public DifficultiesController(IDifficultyRepository difficultyRepository)
        {
            this.difficultyRepository = difficultyRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetDifficulties()
        {
            return Ok(await difficultyRepository.GetAllDifficulties());
        }

        [HttpGet]
        [Route("{id:guid}")]
        public async Task<IActionResult> GetDifficulty(Guid id)
        {
            var difficulty = await difficultyRepository.GetDifficultyById(id);
            if (difficulty == null)
            {
                return NotFound();
            }
            return Ok(difficulty);
        }

        [HttpPost]
        public async Task<IActionResult> AddDifficulty(CreateDifficultyRequest createDifficultyRequest)
        {
            var difficulty = new Difficulty
            {
                Name = createDifficultyRequest.Name
            };
            difficulty = await difficultyRepository.AddDifficulty(difficulty);
            return CreatedAtAction(nameof(GetDifficulty), new { id = difficulty.Id }, difficulty);
        }

        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> UpdateDifficulty(Guid id, UpdateDifficultyRequest updateDifficultyRequest)
        {
            var difficulty = new Difficulty
            {
                Name = updateDifficultyRequest.Name
            };
            difficulty = await difficultyRepository.UpdateDifficulty(id, difficulty);
            if (difficulty == null)
            {
                return NotFound();
            }
            return Ok(difficulty);
        }

        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> DeleteDifficulty(Guid id)
        {
            var difficulty = await difficultyRepository.DeleteDifficulty(id);
            if (difficulty == null)
            {
                return NotFound();
            }
            return Ok(difficulty);
        }
    }
}
