using System;

namespace TestProject.Domain.Entities
{
    internal class FileMetadata
    {
        public string FileName { get; set; }
        public string ContentType { get; set; }
        public string StoragePath { get; set; }
        public DateTime UploadedAt { get; set; }
    }
}