using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Xunit;
using XmlProcessorServer.Services;

namespace XmlProcessorServer.Tests
{
    public class XmlProcessorServiceTests
    {
        private IXmlProcessorService _sut;
        public XmlProcessorServiceTests()
        {
            _sut = new XmlProcessorService();
        }

        [Theory]
        [InlineData(
            "TestData/Compathy_Manual_ContentGrasp SX_en.xml",
            "TestData/Compathy_Manual_ContentGrasp SX_en_expected.txt",
            new[] { "the", "headset" },
            new[] { 42, 9 }
            )]
        public async Task Test1(string inputFilePath, string expectedresultFilePath, string[] expectedKeys, int[] expectedDuplications)
        {
            using (var inputFile = File.Open(inputFilePath, FileMode.Open))
            using (var expectedFile = File.Open(expectedresultFilePath, FileMode.Open))
            {
                var inputBuf = new byte[inputFile.Length];
                var expectedBuf = new byte[expectedFile.Length];
                await Task.WhenAll(inputFile.ReadAsync(inputBuf, 0, inputBuf.Length), expectedFile.ReadAsync(expectedBuf, 0, expectedBuf.Length));
                var res = await _sut.Process(inputBuf, new List<string> { "li", "p" });
                Assert.Equal(System.Text.Encoding.Default.GetString(expectedBuf).Replace("\n", "\r\n"), res.Content);
                for (var i = 0; i < expectedKeys.Length; i++)
                {
                    Assert.Equal(expectedDuplications[i], res.Duplications[expectedKeys[i]]);
                }
            }
        }
    }
}
