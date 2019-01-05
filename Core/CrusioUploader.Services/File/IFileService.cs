namespace CrusioUploader.Services.File
{
    using CrusioUploader.Models.File;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IFileService
    {
        Task<List<File>> GetAllFilesAsync();

        Task<File> GetFleByUidAsync(Guid fileUid);

        Task<File> UploadFileAsync(FileRequest fileRequest);
    }
}
