using System.Collections.Generic;
using System.Threading.Tasks;

namespace XmlProcessorServer.Services
{
    public interface IXmlHandler
    {
         Task Execute(byte[] file, IList<string> tags);
    }
}