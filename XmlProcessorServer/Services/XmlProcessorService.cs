using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using XmlProcessorServer.Models;

namespace XmlProcessorServer.Services
{
    public class XmlProcessorService : IXmlProcessorService
    {
        public async Task<FileAnalyticsModel> Process(byte[] file, IList<string> tags)
        {
            return await Task.Run(() =>
            {
                var content = GetContent(file, tags);
                return new FileAnalyticsModel { Content = content.ToString(), Duplications = GetDuplications(content) };
            });
        }

        private string GetContent(byte[] file, IList<string> tags)
        {
            var extractedContent = new StringBuilder();
            var fileContent = System.Text.Encoding.Default.GetString(file);
            var startIndexes = new List<int>();
            var endIndexes = new List<int>();
            foreach (var tag in tags)
            {
                startIndexes.AddRange(GetIndexes(fileContent, $"<{tag}>").ConvertAll(i => i + 2 + tag.Length));
                endIndexes.AddRange(GetIndexes(fileContent, $"</{tag}>"));
            }

            startIndexes.Sort((i, j) => i.CompareTo(j));
            endIndexes.Sort((i, j) => i.CompareTo(j));

            if (startIndexes.Count != endIndexes.Count)
            {
                throw new Exception("Invalid XML");
            }

            for (var i = 0; i < startIndexes.Count; i++)
            {
                extractedContent.Append(fileContent.Substring(startIndexes[i], endIndexes[i] - startIndexes[i]));
                if (i < startIndexes.Count - 1)
                {
                    extractedContent.Append(' ');
                }
            }
            return extractedContent.ToString();
        }
        private Dictionary<string, int> GetDuplications(string str)
        {
            var duplications = new Dictionary<string, int>();
            var cleanedUpString = Regex.Replace(str, "[^\\w\\s]", "");
            var words = cleanedUpString.Split(new[] { " ", "\r\n" }, StringSplitOptions.None);
            var keys = words.Distinct();
            foreach (var key in keys)
            {
                var duplicationsCount = words.Where(word => word == key).Count();
                if (duplicationsCount >= 2)
                {
                    duplications.Add(key, duplicationsCount);
                }
            }
            var result = duplications.ToList().OrderByDescending(dup => dup.Value).ToDictionary(dup => dup.Key, dup => dup.Value);

            return result;
        }

        private List<int> GetIndexes(string str, string value)
        {
            var indexes = new List<int>();
            for (int index = 0; ; index += value.Length)
            {
                index = str.IndexOf(value, index);
                if (index == -1)
                {
                    return indexes;
                }
                indexes.Add(index);
            }
        }
    }
}