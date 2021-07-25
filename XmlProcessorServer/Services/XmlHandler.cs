using System.Collections.Generic;
using System.Threading.Tasks;

namespace XmlProcessorServer.Services
{
    public class XmlHandler : IXmlHandler
    {
        private readonly IXmlProcessorService _processor;
        private readonly IFileAnalyticsRepository _repo;

        public XmlHandler(IXmlProcessorService processor, IFileAnalyticsRepository repo)
        {
            _processor = processor;
            _repo = repo;
        }

        public async Task Execute(byte[] file, IList<string> tags)
        {
            var model = await _processor.Process(file, tags);
            await _repo.AddEntry(model);
        }
    }
}