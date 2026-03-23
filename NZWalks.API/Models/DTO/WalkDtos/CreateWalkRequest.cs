using System.ComponentModel.DataAnnotations;

namespace NZWalks.API.Models.DTO.WalkDtos
{
    public class CreateWalkRequest
    {
        [Required]
        public string Name { get; set; }

        [Required]
        [MinLength(10, ErrorMessage = "Description must be at least 10 characters long.")]
        public string Description { get; set; }

        [Required]
        [Range(0.1, 50, ErrorMessage = "Length in km must be greater than 0km and less than 50km.")]
        public double LengthInKm { get; set; }

        public string? WalkImageUrl { get; set; }
        public Guid DifficultyId { get; set; }
        public Guid RegionId { get; set; }
    }
}
