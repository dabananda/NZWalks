using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.CustomActionFilters;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO.WalkDtos;
using NZWalks.API.Repositories;

namespace NZWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WalksController : ControllerBase
    {
        private readonly IWalkRepository walkRepository;
        private readonly IMapper mapper;

        public WalksController(IWalkRepository walkRepository, IMapper mapper)
        {
            this.walkRepository = walkRepository;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetWalks([FromQuery] string? filterOn, [FromQuery] string? filterQuery, [FromQuery] string? sortBy, [FromQuery] bool? isAscending, [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 1000)
        {
            var walksDomain = await walkRepository.GetWalks(filterOn, filterQuery, sortBy, isAscending ?? true, pageNumber, pageSize);
            var walksDto = mapper.Map<IEnumerable<WalkDto>>(walksDomain);
            return Ok(walksDto);
        }

        [HttpGet]
        [Route("{id:guid}")]
        public async Task<IActionResult> GetWalkById(Guid id)
        {
            var walkDomain = await walkRepository.GetWalk(id);
            if (walkDomain == null)
            {
                return NotFound();
            }
            var walkDto = mapper.Map<WalkDto>(walkDomain);
            return Ok(walkDto);
        }

        [HttpPost]
        [ValidateModel]
        public async Task<IActionResult> AddWalk(CreateWalkRequest addWalkDto)
        {
            var walkDomain = mapper.Map<Walk>(addWalkDto);
            walkDomain = await walkRepository.AddWalk(walkDomain);
            var walkDto = mapper.Map<WalkDto>(walkDomain);
            return CreatedAtAction(nameof(GetWalkById), new { id = walkDto.Id }, walkDto);
        }

        [HttpPut]
        [Route("{id:guid}")]
        [ValidateModel]
        public async Task<IActionResult> UpdateWalk(Guid id, UpdateWalkRequest updateWalkDto)
        {
            var walkDomain = await walkRepository.GetWalk(id);
            if (walkDomain == null)
            {
                return NotFound();
            }
            walkDomain = mapper.Map<Walk>(updateWalkDto);
            walkDomain = await walkRepository.UpdateWalk(id, walkDomain);
            var walkDto = mapper.Map<WalkDto>(walkDomain);
            return Ok(walkDto);
        }

        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> DeleteWalk(Guid id)
        {
            var walk = await walkRepository.DeleteWalk(id);
            if (walk == null)
            {
                return NotFound();
            }
            return Ok(walk);
        }
    }
}
