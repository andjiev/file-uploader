namespace CrusioUploader.API.Mapper
{
    using CrusioUploader.Models.File;
    using CrusioUploader.Services.File;
    using System.Collections.Generic;
    using System.Linq;

    public static class FileMapper
    {
        public static List<FileDto> MapFilesToDto(List<File> files)
        {
            if(files == null)
            {
                return null;
            }

            return files.Select(s => new FileDto
            {
                Id = s.Id,
                Uid = s.Uid,
                CreatedOn = s.CreatedOn,
                FileName = s.FileName
            }).ToList();
        }

        public static FileDto MapFileToDto(File file)
        {
            if (file == null)
            {
                return null;
            }

            return new FileDto
            {
                Id = file.Id,
                Uid = file.Uid,
                CreatedOn = file.CreatedOn,
                FileName = file.FileName
            };
        }
    }
}
