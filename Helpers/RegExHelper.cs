using System;
using System.Text.RegularExpressions;

namespace TribalWarsBot.Helpers
{
    public class RegExHelper
    {
        public static string GetTextWithRegEx(string pattern, string text)
        {
            var regEx = new Regex(pattern);
            var match = regEx.Match(text);

            if (!match.Success)
                throw new Exception("Did not find the building id");

            return match.Groups[1].Value;
        }
    }
}