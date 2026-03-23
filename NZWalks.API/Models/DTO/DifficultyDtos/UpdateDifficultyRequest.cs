using System.ComponentModel.DataAnnotations;

namespace NZWalks.API.Models.DTO.DifficultyDtos
{
    public class UpdateDifficultyRequest
    {
        [Required]
        public string Name { get; set; }
    }
}
