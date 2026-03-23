using System.ComponentModel.DataAnnotations;

namespace NZWalks.API.Models.DTO.DifficultyDtos
{
    public class CreateDifficultyRequest
    {
        [Required]
        public string Name { get; set; }
    }
}
