using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;
using NZWalks.API.Repositories;
using System.Threading.Tasks;

namespace NZWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : ControllerBase
    {
        private readonly IImageRepository imageRepository;

        public ImagesController(IImageRepository imageRepository)
        {
            this.imageRepository = imageRepository;
        }

        [HttpPost]
        [Route("Upload")]
        public async Task<IActionResult> Upload([FromForm] ImageUploadRequestDto imageUploadRequestDto)
        {
            ValidateImageUpload(imageUploadRequestDto);

            if (ModelState.IsValid)
            {
                // Convert the DTO to the domain model
                var imageDomainModel = new Image
                {
                    File = imageUploadRequestDto.File,
                    FileName = imageUploadRequestDto.File.FileName,
                    FileDescription = imageUploadRequestDto.FileDescription,
                    FileExtension = Path.GetExtension(imageUploadRequestDto.File.FileName).ToLower(),
                    FileSizeInBytes = imageUploadRequestDto.File.Length,
                };

                // Upload the image
                await imageRepository.Upload(imageDomainModel);

                return Ok(new { message = "Image uploaded successfully", image = imageDomainModel });
            }

            return BadRequest(ModelState);
        }

        public void ValidateImageUpload(ImageUploadRequestDto imageUploadRequestDto)
        {
            var allowedExtensions = new string[] { ".jpg", ".jpeg", ".png" };

            // Check if the file type is valid
            if (!allowedExtensions.Contains(Path.GetExtension(imageUploadRequestDto.File.FileName).ToLower()))
            {
                ModelState.AddModelError("file", "Invalid image file type. Allowed types are: .jpg, .jpeg and .png");
            }

            // Check if the file size exceeds 2 MB
            if (imageUploadRequestDto.File.Length > 2 * 1024 * 1024)
            {
                ModelState.AddModelError("file", "Image file size exceeds the maximum limit of 2 MB");
            }
        }
    }
}
