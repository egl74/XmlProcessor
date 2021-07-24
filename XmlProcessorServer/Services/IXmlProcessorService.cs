using System.Collections.Generic;
using System.Threading.Tasks;
using XmlProcessorServer.Models;

namespace XmlProcessorServer.Services
{

    public interface IXmlProcessorService
    {
        Task<FileAnalyticsModel> Process(byte[] file, IList<string> tags);
    }
}