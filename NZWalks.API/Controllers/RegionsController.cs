using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;
using NZWalks.API.Repositories;

namespace NZWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegionsController : ControllerBase
    {
        private readonly IRegionRepository _regionRepository;

        public RegionsController(IRegionRepository regionRepository)
        {
            _regionRepository = regionRepository;
        }

        [HttpGet]
        public IActionResult GetAllRegions()
        {
            var regions = _regionRepository.GetRegions();
           
            var regionsDto = new List<Region>();
            regions.ToList().ForEach(region =>
            {
                var regionDto = new Region()
                {
                    Id = region.Id,
                    Code = region.Code,
                    Name = region.Name,
                    RegionImageUrl = region.RegionImageUrl
                };

                regionsDto.Add(regionDto);
            });

            return Ok(regionsDto);
        }
    }
}
