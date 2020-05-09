using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Searchfight
{
    class ResultsProcess
    {
        IResult[] Fetchers { get; set; }

        public ResultsProcess(IResult[] fetchers, string[] queries)
        {
            Fetchers = fetchers;
            var foundDuplicates = fetchers.GroupBy(x => x.Name).Any(x => x.Count() > 1);
            if (foundDuplicates)
            {
                throw new InvalidOperationException("Searchers must be different.");
            }
        }

        public void Compare(string[] queries)
        {
            string TotalWinnerName = "";
            long TotalWinnerValue = -1;
            StringBuilder strSearchWinner = new StringBuilder();

            //rows: fetchers
            //columns: queries
            for (int i = 0; i < queries.Length; i++)
            {
                long maxValue = -1;
                int maxIndex = -1;
                Console.Write($"{queries[i]}: ");
                for (int j = 0; j < Fetchers.Length; j++)
                {
                    //blocking call since we are calculatin the total
                    Task<long> task = Fetchers[j].GetResult(queries[i]);
                    task.Wait();

                    Console.Write($"{Fetchers[j].Name}: {task.Result} ");

                    if (task.Result > maxValue)
                    {
                        maxValue = task.Result;
                        maxIndex = i;
                    }
                }
                Console.WriteLine();
                strSearchWinner.Append($"{Fetchers[maxIndex].Name} Winner: {queries[maxIndex]}{Environment.NewLine}");
                if (maxValue > TotalWinnerValue)
                {
                    TotalWinnerValue = maxValue;
                    TotalWinnerName = queries[maxIndex];
                }
            }
            Console.Write(strSearchWinner);
            Console.WriteLine($"Total Winner: {TotalWinnerName}");
        }
    }
}