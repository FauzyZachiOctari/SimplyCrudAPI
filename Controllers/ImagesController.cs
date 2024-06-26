﻿using SimplyCrudAPI.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SimplyCrudAPI.Models.Images;
using Microsoft.AspNetCore.Hosting;
using System.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SimplyCrudAPI.ExampleData.BookData;
using SimplyCrudAPI.Models.Book;

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

        ///<summary>
        /// Get data from database use fileName. If pict get remove from folder, the pict still can display as result. Because this metode get the image pict (Binary data) from database.
        /// </summary>
        /// <param name="fileName"></param>
        [HttpGet("GetImagesDatabase/{fileName}")]
        public async Task<IActionResult> GetImages([FromRoute] string fileName)
        {
            var imageFromDb = await _context.Imagined.FirstOrDefaultAsync(i => i.fileImages == fileName);

            if (imageFromDb != null)
            {
                return File(imageFromDb.imageData, "image/png");
            }

            return NotFound();
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

        [HttpGet("GetImageWithName")]
        public IActionResult GetImage([FromQuery] string fileName)
        {
            var allowedExtensions = new[] { ".jpg", ".jpeg", ".png" };

            if (string.IsNullOrEmpty(fileName))
            {
                return BadRequest("File name is required");
            }
            var fileExtension = Path.GetExtension(fileName).ToLowerInvariant();

            if (!allowedExtensions.Contains(fileExtension))
            {
                return BadRequest("File extension is not allowed");
            }

            string imagePath = Path.Combine(_webHostEnvironment.WebRootPath, "Images/UploadExample", fileName);

            if (System.IO.File.Exists(imagePath))
            {
                var fileBytes = System.IO.File.ReadAllBytes(imagePath);
                return File(fileBytes, "image/" + fileExtension.Substring(1));
            }
            else
            {
                return NotFound("File not found");
            }
        }

        /// <summary>
        /// Deletes an Book By Id Book.
        /// </summary>
        /// <param name="idImages"></param>
        /// <returns>True if the Book is successfully deleted, otherwise false.</returns>
        /// <remarks>
        /// Id Book must be filled in. If the ID Book is found, the Book data will be deleted
        /// </remarks>
        [HttpDelete]
        [Route("DeleteImages/{idImages:guid}")]
        public async Task<IActionResult> DeleteAddress([FromRoute] Guid idImages)
        {
            try
            {
                // Ekstensi file yang didukung
                string[] extensions = { ".png", ".jpg", ".jpeg" };

                // Temukan entri gambar di database
                var image = await _context.Imagined.FindAsync(idImages);

                if (image != null)
                {
                    // Loop melalui ekstensi yang didukung untuk menemukan file yang sesuai
                    string fileName = image.fileImages;
                    string filePath = Path.Combine(_webHostEnvironment.WebRootPath, "Images", "UploadExample", fileName);

                    // Jika file ditemukan, hapus file
                    if (System.IO.File.Exists(filePath))
                    {
                        System.IO.File.Delete(filePath);
                    }

                    // Menghapus entri dari database
                    _context.Remove(image);
                    await _context.SaveChangesAsync();

                    return Ok(new
                    {
                        message = "Image Deleted successfully"
                    });
                }

                return NotFound();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

    }
}
