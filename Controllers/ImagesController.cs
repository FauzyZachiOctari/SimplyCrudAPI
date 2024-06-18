using SimplyCrudAPI.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SimplyCrudAPI.Models.Images;
using Microsoft.AspNetCore.Hosting;
using System.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace SimplyCrudAPI.Controllers
{
    [ApiController]
    public class ImagesController : ControllerBase
    {
        private static IWebHostEnvironment _webHostEnvironment;
        private readonly ImagesAPIDbContext _context;

        public ImagesController(IWebHostEnvironment webHostEnvironment, ImagesAPIDbContext context)
        {
            _webHostEnvironment = webHostEnvironment;
            this._context = context;
        }

        [HttpPost("PostImages")]
        public async Task<string> Upload([FromForm]ImagesRequest imagesRequest)
        {
            if(imagesRequest.fileImages.Length > 0)
            {
                try
                {
                    var fileExtension = Path.GetExtension(imagesRequest.fileImages.FileName).ToLowerInvariant();
                    var allowedExtensions = new[] { ".jpg", ".jpeg", ".png" };
                    if (!allowedExtensions.Contains(fileExtension))
                    {
                        return "Only JPG, JPEG, and PNG are alliwed.";
                    }

                    string fileName = $"images_{DateTime.Now:yyyyMMddHHmmss}_{imagesRequest.fileImages.FileName}";
                    byte[] imageData;
                    using (var memoryStream = new MemoryStream())
                    {
                        await imagesRequest.fileImages.CopyToAsync(memoryStream);
                        imageData = memoryStream.ToArray();
                    }

                    long sizeFile =  imagesRequest.fileImages.Length;
                    string formatFile = imagesRequest.fileImages.ContentType;

                    string ConvertSizeToString(long size)
                    {
                        if(size < 1024)
                        {
                            return $"{size} B";
                        }else if(size < 1024 * 1024)
                        {
                            return $"{size / 1024} KB";
                        }
                        else if (size < 1024 * 1024 * 1024)
                        {
                            return $"{size / (1024 * 1024)} MB";
                        }
                        else
                        {
                            return $"{size / (1024 * 1024 * 1024)} GB";
                        }
                    }

                    var addImages = new Images()
                    {
                        nameImages = imagesRequest.nameImages,
                        descriptionImages = imagesRequest.descriptionImages,
                        imageData = imageData,
                        imagesCapacity = ConvertSizeToString(sizeFile),
                        fileImages = fileName,
                        imagesFormat = formatFile,
                    };

                    await _context.Imagined.AddAsync(addImages);
                    await _context.SaveChangesAsync();

                    string uploadPatch = Path.Combine(_webHostEnvironment.WebRootPath, "Images", "UploadExample", fileName);

                    if (!Directory.Exists(Path.GetDirectoryName(uploadPatch)))
                    {
                        Directory.CreateDirectory(Path.GetDirectoryName(uploadPatch));
                    }

                    using (FileStream fileStream = System.IO.File.Create(uploadPatch))
                    {
                        imagesRequest.fileImages.CopyTo(fileStream);
                        fileStream.Flush();
                    }

                    return "/Images/UploadExample/" + fileName;

                }
                catch(Exception ex)
                {
                    throw ex;
                }
            }
            else
            {
                return "Upload Failed!";
            }
        }

        [HttpGet("GetImagesFolder/{fileName}")]
        public async Task<IActionResult> Get([FromRoute] string fileName)
        {
            string[] extensions = { ".png", ".jpg", ".jpeg" };
            string path = _webHostEnvironment.WebRootPath + "/Images/UploadExample/";

            foreach (var ext in extensions)
            {
                var filePath = Path.Combine(path, fileName + ext);
                if (System.IO.File.Exists(filePath))
                {
                    byte[] fileBytes = System.IO.File.ReadAllBytes(filePath);
                    string mimeType = GetMimeType(ext);
                    return File(fileBytes, mimeType);
                }
            }
            return NotFound();
        }

        private string GetMimeType(string extension)
        {
            return extension switch
            {
                ".png" => "image/png",
                ".jpg" => "image/jpeg",
                ".jpeg" => "image/jpeg",
                _ => "application/octet-stream",
            };
        }
    }
}
