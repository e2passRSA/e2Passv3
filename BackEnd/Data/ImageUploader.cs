using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace BackEnd.Data
{
    public class ImageUploader : DataLoader<string, string>
    {
        private readonly IConfiguration _config;
        private readonly ILogger<ImageUploader> _logger;
        public ImageUploader(ILogger<ImageUploader> logger, IConfiguration configuration)
        {
            _config = configuration;
            _logger = logger;
        }
        public override async Task<ServiceResponse<string>> DataLoaderAsync(IFormFile file, string storage)
        {
            var now = DateTime.UtcNow;
            try
            {
                var _pathToSave = Path.Combine(_config[$"Images:{storage}"]);
                
                if (!Directory.Exists(_pathToSave)) 
                    Directory.CreateDirectory(_pathToSave);
                
                var mime = file.FileName.Substring(file.FileName.LastIndexOf('.'));
                
                var fileName = $"{storage}_{now.ToString("dd-MM-yyyy-HH-mm-ss")}{mime}";
                
                using (var fileStream = new FileStream(Path.Combine(_pathToSave, fileName), FileMode.Create))
                    await file.CopyToAsync(fileStream);
                
                return new ServiceResponse<string>
                {
                    Time = now,IsSuccess = true,Message = "Image Saved Sucessfully!", Data = fileName
                };
            }catch(Exception e)
            {
                return new ServiceResponse<string>
                {
                    Time = now,IsSuccess = false,Message = e.Message,Data = "An Error Occured!"
                };
            }
        }

        public  FileStream StreamFile(string storage, string fileName)
        {
            var path = _config[$"Images:{storage}"];
            return new FileStream(Path.Combine(path, fileName), FileMode.Open, FileAccess.Read);
        }
    }
}
