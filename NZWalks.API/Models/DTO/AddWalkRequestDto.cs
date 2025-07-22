using System.ComponentModel.DataAnnotations;

namespace NZWalks.API.Models.DTO
{
    public class AddWalkRequestDto
    {
        [Required]
        [Length(2, 100, ErrorMessage = "Name must be between 2 and 100 characters")]
        public string Name { get; set; }
        [Required]
        [Length(10, 250, ErrorMessage = "Description must be between 10 and 250 characters")]
        public string Description { get; set; }
        [Required]
        [MinLength(0, ErrorMessage = "Minimum length should atleast 0 km")]
        public double LengthInKm { get; set; }
        public string? WalkImageUrl { get; set; }
        [Required]
        public Guid Region { get; set; }
        [Required]
        public Guid Difficulty { get; set; }
    }
}
