using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using XmlProcessorServer.Models;

namespace XmlProcessorServer.Services
{
    public class FileAnalyticsRepository : IFileAnalyticsRepository
    {
        private readonly IConfiguration _config;
        public FileAnalyticsRepository(IConfiguration config)
        {
            _config = config;
        }

        public async Task AddEntry(FileAnalyticsModel model)
        {
            using (var db = new FileAnalyticsContext(_config))
            {
                db.Add(model);
                await db.SaveChangesAsync();
            }
        }
    }
}