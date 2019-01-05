namespace CrusioUploader.Services.File
{
    using CrusioUploader.Db;
    using Dapper;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using System.Linq;
    using System;
    using CrusioUploader.Models.File;

    public class FileService : IFileService
    {
        private readonly UploaderDbContext uploaderDbContext;

        public FileService(UploaderDbContext uploaderDbContext)
        {
            this.uploaderDbContext = uploaderDbContext;
        }

        public async Task<List<File>> GetAllFilesAsync()
        {
            string query = @"select
                                [File].Id as Id,
                                [File].Uid as Uid,
                                [File].CreatedOn as CreatedOn,
                                [File].FileName as FileName
                            from [File]
                            where DeletedOn is null
                            order by CreatedOn desc";

            return (await uploaderDbContext.Connection.QueryAsync<File>(query)).ToList();
        }

        public async Task<File> GetFleByUidAsync(Guid fileUid)
        {
            string query = @"select top 1
                                [File].Id as Id,
                                [File].Uid as Uid,
                                [File].CreatedOn as CreatedOn,
                                [File].FileName as FileName,
                                [File].FileContent as FileContent
                            from [File]
                            where [File].Uid = @Uid
                                and DeletedOn is null";

            return (await uploaderDbContext.Connection.QueryFirstOrDefaultAsync<File>(query, new
            {
                Uid = fileUid
            }));
        }

        public async Task<File> UploadFileAsync(FileRequest fileRequest)
        {
            File file = new File
            {
                Uid = Guid.NewGuid(),
                CreatedOn = DateTime.Now,
                FileName = fileRequest.FileName,
                FileContent = fileRequest.FileContent
            };

            string query = @"insert into [File]
                            (Uid, CreatedOn, FileName, FileContent) values
                            (@Uid, @CreatedOn, @FileName, @FileContent)";

            await uploaderDbContext.Connection.ExecuteAsync(query, new
            {
                Uid = file.Uid,
                CreatedOn = file.CreatedOn,
                FileName = fileRequest.FileName,
                FileContent = fileRequest.FileContent
            });

            return file;
        }
    }
}
