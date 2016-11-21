using System;
using System.Globalization;
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
                throw new Exception($"Did not find a regex match. Pattern {pattern}, text:{text}");

            return match.Groups[1].Value;
        }

        public static int GetNumberWithRegEx(string pattern, string text)
        {
            return int.Parse(GetTextWithRegEx(pattern, text));
        }
        public static double GetdoubleWithRegEx(string pattern, string text)
        {
            return double.Parse(GetTextWithRegEx(pattern, text), CultureInfo.InvariantCulture);
        }


        public static Time GetTimeFromString(string text)
        {
            var regEx = new Regex(@"(\d+):(\d+):(\d+)");
            var match = regEx.Match(text);

            if (!match.Success)
                throw new Exception("Did not find the time");

            var time = new Time
            {
                Hours = Convert.ToInt32(match.Groups[1].Value),
                Minutes = Convert.ToInt32(match.Groups[2].Value),
                Secounds = Convert.ToInt32(match.Groups[3].Value)
            };

            return time;
        }

        public struct Time
        {
            public int Hours { get; set; }
            public int Minutes { get; set; }
            public int Secounds { get; set; }

            public override string ToString()
            {
                return $"{Hours}:{Minutes}:{Secounds}";
            }
        }
    }
}