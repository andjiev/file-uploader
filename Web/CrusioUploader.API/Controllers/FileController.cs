using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CrusioUploader.API.Mapper;
using CrusioUploader.Models.File;
using CrusioUploader.Services.File;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CrusioUploader.API.Controllers
{
    [Produces("application/json")]
    [Route("api/files")]
    public class FileController : Controller
    {
        private readonly IFileService fileService;

        public FileController(IFileService fileService)
        {
            this.fileService = fileService;
        }

        [HttpGet]
        public async Task<List<FileDto>> GetAllFilesAsync()
        {
            List<File> files = await fileService.GetAllFilesAsync();
            List<FileDto> response = FileMapper.MapFilesToDto(files);
            return response;
        }

        [HttpGet]
        [Route("{fileUid:guid}")]
        public async Task<IActionResult> GetFileAsync(Guid fileUid)
        {
            File file = await fileService.GetFleByUidAsync(fileUid);
            return File(file.FileContent, "application/octet-stream", file.FileName);
        }

        [HttpPost]
        public async Task<FileDto> UploadFileAsync()
        {
            var file = Request.Form.Files.FirstOrDefault();
            if (file == null)
            {
                throw new Exception("No file has been requested");
            }

            FileRequest fileRequest = new FileRequest
            {
                FileName = file.FileName,
                FileContent = await ReadContent(file)
            };

            File uploadedFile = await fileService.UploadFileAsync(fileRequest);
            FileDto response = FileMapper.MapFileToDto(uploadedFile);
            return response;
        }

        private async Task<byte[]> ReadContent(IFormFile file)
        {
            using (var memoryStream = new System.IO.MemoryStream())
            {
                await file.CopyToAsync(memoryStream);
                return memoryStream.ToArray();
            }
        }
    }
}