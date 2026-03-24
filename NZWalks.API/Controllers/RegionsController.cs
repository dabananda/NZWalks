using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.CustomActionFilters;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO.RegionDtos;
using NZWalks.API.Repositories;

namespace NZWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegionsController : ControllerBase
    {
        private readonly IRegionRepository _regionRepository;
        private readonly IMapper _mapper;

        public RegionsController(IRegionRepository regionRepository, IMapper mapper)
        {
            _regionRepository = regionRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [Authorize(Roles = "Reader, Writer")]
        public async Task<IActionResult> GetAllRegions()
        {
            var regions = await _regionRepository.GetRegionsAsync();

            var regionsDto = _mapper.Map<List<RegionDto>>(regions);

            return Ok(regionsDto);
        }

        [HttpGet]
        [Route("{id:guid}")]
        [Authorize(Roles = "Reader, Writer")]
        public async Task<IActionResult> GetRegion(Guid id)
        {
            var region = await _regionRepository.GetRegionAsync(id);

            if (region == null)
            {
                return NotFound();
            }

            var regionDto = _mapper.Map<RegionDto>(region);

            return Ok(regionDto);
        }

        [HttpPost]
        [ValidateModel]
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> AddRegion(AddRegionRequest addRegionRequest)
        {
            var regionDomain = _mapper.Map<Region>(addRegionRequest);
            var region = await _regionRepository.AddRegionAsync(regionDomain);
            var regionDtoResult = _mapper.Map<RegionDto>(region);
            return CreatedAtAction(nameof(GetRegion), new { id = regionDtoResult.Id }, regionDtoResult);
        }

        [HttpPut]
        [Route("{id:guid}")]
        [ValidateModel]
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> UpdateRegion([FromRoute] Guid id, [FromBody] UpdateRegionRequest updateRegionRequest)
        {
            var regionDomain = _mapper.Map<Region>(updateRegionRequest);
            var region = await _regionRepository.UpdateRegionAsync(id, regionDomain);
            if (region == null)
            {
                return NotFound();
            }
            var regionDto = _mapper.Map<RegionDto>(region);
            return Ok(regionDto);
        }

        [HttpDelete]
        [Route("{id:guid}")]
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> DeleteRegion([FromRoute] Guid id)
        {
            var region = await _regionRepository.DeleteRegionAsync(id);
            if (region == null)
            {
                return NotFound();
            }
            var regionDto = _mapper.Map<RegionDto>(region);
            return Ok(regionDto);
        }
    }
}
