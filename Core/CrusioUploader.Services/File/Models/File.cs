namespace CrusioUploader.Services.File
{
    using System;

    public class File
    {
        public int Id { get; set; }
        public Guid Uid { get; set; }
        public DateTime CreatedOn { get; set; }
        public string FileName { get; set; }
        public byte[] FileContent { get; set; }
    }
}
