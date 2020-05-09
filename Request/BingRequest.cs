using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text.RegularExpressions;

namespace Searchfight
{
    class BingRequest : IResult
    {
        public string Name { get; set; }

        public BingRequest()
        {
            Name = "Bing Search";
        }

        public async Task<long> GetResult(string query)
        {
            RequestSender req = new RequestSender();
            var response = await req.GetContentAsString($"https://www.bing.com/search?q={Util.formarQuery(query)}");
            return ProcessContent(response);
        }

        public long ProcessContent(string data)
        {
            //looking for this pattern ex: 1.000 results
            string rgxPattern = @"(\d{1,3}(.\d{3})*(\.\d+)?) result\w+";
            Match results = Regex.Match(data, rgxPattern);
            if (!results.Success)
            {
                throw new NotSupportedException("No result found.");
            }

            //results[1] -> has the data we want
            string rawResult = results.Groups[1].Value.ToString();
            //removing dots because bing uses as thousand separator
            string finalResult = rawResult.Replace(".", string.Empty);
            return long.Parse(finalResult);
        }
    }
}