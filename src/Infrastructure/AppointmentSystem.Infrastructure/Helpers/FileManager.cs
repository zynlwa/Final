using Microsoft.AspNetCore.Http;
using System;
using System.IO;

namespace AppointmentSystem.Infrastructure.Helpers;


    public static class FileManager
    {
        public static string SaveFile(this IFormFile formFile, string folderName)
        {
            var fileName = Guid.NewGuid() + Path.GetExtension(formFile.FileName);
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", folderName, fileName);
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                formFile.CopyTo(stream);
            }
            return fileName;
        }
        public static void DeleteFile(string folderName, string fileName)
        {
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", folderName, fileName);
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
        }

    }


