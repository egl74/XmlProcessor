using System.Collections.Generic;

namespace XmlProcessorServer.Models
{
    public class FileAnalyticsModel
    {
        public string FileName { get; set; }
        public string Content { get; set; }
        public Dictionary<string, int> Duplications { get; set; }
    }
}

