using System.ComponentModel.DataAnnotations;

namespace SampleWebApp.Models
{
    public class FileDetails
    {
        [Key]
        public string  FileID { get; set; }

        public string FileName { get; set; }
    
        public string FileBytes { get; set; }

        public string FileType { get; set; }
    }
}
