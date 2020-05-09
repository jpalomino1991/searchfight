using System;
using System.Text.RegularExpressions;
using System.Linq;

namespace Searchfight
{
    class Util
    {
        public static string formarQuery(string query)
        {
            if (!query.Any(Char.IsWhiteSpace))
            {
                return query;
            }
            var newQuery = Regex.Replace(query, @"\s+", "+");
            //if a term has space we have to change with + to browse
            return $"\"{newQuery}\"";
        }
    }
}