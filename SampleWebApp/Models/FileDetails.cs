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
        public int UserId { get; set; }
    }
}
