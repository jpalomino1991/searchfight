using System;
using System.Threading.Tasks;

namespace Searchfight
{
    interface IRequest
    {
        Task<string> GetContentAsString(string path);
    }
}