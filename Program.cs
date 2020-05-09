using System;
using System.IO;

namespace Searchfight
{
    class Program
    {
        static void Main(string[] args)
        {
            IResult[] lstSearchers = {
                new GoogleRequest(),
                new BingRequest()
            };

            ResultsProcess result = new ResultsProcess(lstSearchers, args);
            result.Compare(args);
        }
    }
}
