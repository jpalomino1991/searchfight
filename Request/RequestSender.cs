using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Searchfight
{
    public class RequestSender : IRequest
    {
        private static HttpClient _CLIENT;

        public RequestSender()
        {
            if (_CLIENT == null)
            {
                _CLIENT = new HttpClient();
            }
        }

        public async Task<string> GetContentAsString(string path)
        {
            using (var response = await _CLIENT.GetAsync(path))
            {
                return await response.Content.ReadAsStringAsync();
            }
        }
    }
}