using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Text.Json;

namespace Searchfight
{
    class GoogleRequest : IResult
    {
        public string Name { get; set; }
        string GOOGLE_API_KEY = "YOUR API KEY";
        string GOOGLE_CONTEXT_SEARCH = "017576662512468239146:omuauf_lfve";

        public GoogleRequest()
        {
            Name = "Google Search";
        }
        public async Task<long> GetResult(string query)
        {
            RequestSender req = new RequestSender();
            var response = await req.GetContentAsString($"https://www.googleapis.com/customsearch/v1?key={GOOGLE_API_KEY}&cx={GOOGLE_CONTEXT_SEARCH}&q={Util.formarQuery(query)}");
            return ProcessContent(response);
        }
        //I'm using google api so need to read the json result and get the data
        public long ProcessContent(string data)
        {
            long response = 0;

            var options = new JsonDocumentOptions
            {
                AllowTrailingCommas = true
            };

            using (JsonDocument document = JsonDocument.Parse(data, options))
            {
                JsonElement elem = document.RootElement.GetProperty("queries");
                foreach (JsonElement element in elem.GetProperty("request").EnumerateArray())
                {
                    response = long.Parse(element.GetProperty("totalResults").ToString());
                }
            }

            return response;
        }
    }
}