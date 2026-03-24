using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO.Image;
using NZWalks.API.Repositories;

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
        [Route("upload")]
        public async Task<IActionResult> Upload([FromForm] ImageUploadRequestDto request)
        {
            ValidateFile(request);

            if (ModelState.IsValid)
            {
                var imageDomainModel = new Image
                {
                    File = request.File,
                    FileName = request.FileName,
                    Description = request.Description,
                    FileExtension = Path.GetExtension(request.File.FileName).ToLower(),
                    FileSizeInBytes = request.File.Length,
                };

                await imageRepository.Upload(imageDomainModel);

                return Ok(new { Message = "File uploaded successfully!", Image = imageDomainModel });
            }

            return BadRequest(ModelState);
        }

        private void ValidateFile(ImageUploadRequestDto request)
        {
            var allowedExtensions = new[] { ".jpg", ".jpeg", ".png" };

            if (request.File == null || request.File.Length == 0)
            {
                ModelState.AddModelError("File", "No file uploaded.");
                return;
            }

            if (!allowedExtensions.Contains(Path.GetExtension(request.File.FileName).ToLower()))
            {
                ModelState.AddModelError("File", "Invalid file type. Only .jpg, .jpeg, and .png are allowed.");
                return;
            }

            if (request.File.Length > 2 * 1024 * 1024)
            {
                ModelState.AddModelError("File", "File size exceeds the 2MB limit.");
                return;
            }
        }
    }
}
