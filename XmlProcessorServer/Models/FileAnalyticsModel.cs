using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace XmlProcessorServer.Models
{
    public class FileAnalyticsModel
    {
        [Key]
        public int Id { get; set; }
        public string FileName { get; set; }
        public string Content { get; set; }
        public Dictionary<string, int> Duplications { get; set; }
    }
}

