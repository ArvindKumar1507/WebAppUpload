using System;

namespace SampleWebApp.Models.BusinessModels
{
    public class BusinessFile
    {
       
        public string FileId { get; set; }
      
        public string FileName { get; set; }
            
        public string FileType { get; set; }
       
        public int CreatedBy { get; set; }
      
        public DateTime CreatedTime { get; set; }
    
    }
}
