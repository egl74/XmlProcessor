using System.Threading.Tasks;
using XmlProcessorServer.Models;

namespace XmlProcessorServer.Services
{
    public interface IFileAnalyticsRepository
    {
        Task AddEntry(FileAnalyticsModel model);
    }
}