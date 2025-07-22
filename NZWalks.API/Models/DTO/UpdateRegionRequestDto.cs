using System.ComponentModel.DataAnnotations;

namespace NZWalks.API.Models.DTO
{
    public class UpdateRegionRequestDto
    {
        [Required]
        [MinLength(3, ErrorMessage = "Code must be minimum 3 characters")]
        [MaxLength(3, ErrorMessage = "Code must be maximum 3 characters")]
        public string Code { get; set; }
        [Required]
        [Length(2, 100, ErrorMessage = "Name must be between 2 and 100 characters")]
        public string Name { get; set; }
        public string? RegionImageUrl { get; set; }
    }
}
