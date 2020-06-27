using System;
using System.ComponentModel.DataAnnotations;

namespace SampleWebApp.Models
{
    public class FileDetails
    {
        [Key]
        public string  FileId { get; set; }

        [Required]
        public string FileName { get; set; }

        [Required]
        public string FileBytes { get; set; }
        
        [Required]
        public string FileType { get; set; }

        [Required]
        public int CreatedBy { get; set; }

        [Required]
        public DateTime CreatedTime { get; set; }

        public int ModifiedBy { get; set; }

        public DateTime ModifiedTime { get; set; }
    }
}
