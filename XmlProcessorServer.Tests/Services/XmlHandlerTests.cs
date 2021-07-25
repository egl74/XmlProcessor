using System.Collections.Generic;
using System.Threading.Tasks;
using Moq;
using XmlProcessorServer.Models;
using XmlProcessorServer.Services;
using Xunit;

namespace XmlProcessorServer.Tests
{
    public class XmlHandlerTests
    {
        private readonly IXmlHandler _sut;

        private readonly Mock<IFileAnalyticsRepository> _repoMock = new Mock<IFileAnalyticsRepository>();

        private readonly Mock<IXmlProcessorService> _processorMock = new Mock<IXmlProcessorService>();

        public XmlHandlerTests()
        {
            _sut = new XmlHandler(_processorMock.Object, _repoMock.Object);
        }

        [Fact]
        public async Task Execute()
        {
            _processorMock.Setup(processor => processor.Process(It.IsAny<byte[]>(), It.IsAny<IList<string>>()));
            _repoMock.Setup(repo => repo.AddEntry(It.IsAny<FileAnalyticsModel>()));
            await _sut.Execute(new byte[] { 32, 43 }, new[] { "43" });
            _processorMock.Verify(processor => processor.Process(It.IsAny<byte[]>(), It.IsAny<IList<string>>()), Times.Once());
            _repoMock.Verify(repo => repo.AddEntry(It.IsAny<FileAnalyticsModel>()), Times.Once());
        }
    }
}