namespace CrusioUploader.Models.File
{
    using System;

    public class FileDto
    {
        public int Id { get; set; }
        public Guid Uid { get; set; }
        public DateTime CreatedOn { get; set; }
        public string FileName { get; set; }
    }
}
