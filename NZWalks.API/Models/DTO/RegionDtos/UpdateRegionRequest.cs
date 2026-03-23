using System.ComponentModel.DataAnnotations;

namespace NZWalks.API.Models.DTO.RegionDtos
{
    public class UpdateRegionRequest
    {
        [Required]
        [Length(3, 3, ErrorMessage = "Code must be exactly 3 characters long.")]
        public string Code { get; set; }

        [Required]
        public string Name { get; set; }

        public string? RegionImageUrl { get; set; }
    }
}
