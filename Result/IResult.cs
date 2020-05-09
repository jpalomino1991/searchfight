using System;
using System.Threading.Tasks;

namespace Searchfight
{
    interface IResult
    {
        string Name { get; set; }
        Task<long> GetResult(string query);
        long ProcessContent(string data);
    }
}