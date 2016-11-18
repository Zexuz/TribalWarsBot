using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms.VisualStyles;

using CsQuery;

namespace TribalWarsBot.Services {

    public class EventService {

        private readonly RequestManager _requestManager;

        private const string Url = "https://sv36.tribalwars.se/game.php?village=2173&screen=event_crest";

        public EventService(RequestManager requestManager) {
            _requestManager = requestManager;
        }

        private CQ SetFilters(string token) {
            var url =
                "https://sv36.tribalwars.se/game.php?village=2145&screen=event_crest&action=set_filters&h=" + token;
            var postData = "filter%5Bhide_friends%5D=on&filter%5Bhide_crestless%5D=on";

            var req = _requestManager.GeneratePOSTRequest(url, postData, null, null, true);
            var res = _requestManager.GetResponse(req);
            CQ html = RequestManager.GetResponseStringFromResponse(res);
            return html;
        }

        private CQ GetHtmlForPage() {
            var req = _requestManager.GenerateGETRequest(Url, null, null, false);
            var res = _requestManager.GetResponse(req);
            CQ html = RequestManager.GetResponseStringFromResponse(res);
            return html;
        }

        public int GetAvalibleMasters() {
            var ele = GetHtmlForPage();
            var str = "Tillgängliga/försvarande:";
            var startIndex = ele.Text().IndexOf(str) + str.Length;
            var substring = ele.Text().Substring(startIndex, 10).Trim();

            return Convert.ToInt32(substring);
        }

        public MyFlags GetMyFlags() {
            var flagsEmlements = GetHtmlForPage().Select("span.crest-count.crest-count-unlocked").ToList();

            var myFlags = new MyFlags {
                Flags = new Dictionary<FlagType, int> {
                    {FlagType.Raven, Convert.ToInt32(flagsEmlements[0].TextContent)},
                    {FlagType.Dragon, Convert.ToInt32(flagsEmlements[1].TextContent)},
                    {FlagType.Panther, Convert.ToInt32(flagsEmlements[2].TextContent)},
                    {FlagType.Boar, Convert.ToInt32(flagsEmlements[3].TextContent)},
                    {FlagType.Horse, Convert.ToInt32(flagsEmlements[4].TextContent)},
                    {FlagType.Squirrel, Convert.ToInt32(flagsEmlements[5].TextContent)}
                }
            };


            return myFlags;
        }

        public List<Oponet> GetOponents(string token) {
            var tableBody = SetFilters(token).Select("#challenge_table tbody tr").ToList();
            var list = new List<Oponet>();

            foreach (var tableRow in tableBody) {
                if (tableRow.TextContent.Contains("Inga tillgängliga mästare") ||
                    tableRow.TextContent.Contains("Vapensköld krävs") ||
                    !tableRow.TextContent.Contains("Utmaning")) continue;

                var op = new Oponet();
                op.FlagType = GetFlagType(tableRow);
                op.Url = GetUrl(tableRow);
                op.Duration = GetDuration(tableRow);

                list.Add(op);
            }


            return list;
        }

        public TimeSpan GetDuration(IDomObject trElement) {
            var regEx = new Regex(@"(\d+):(\d+):(\d+)");
            var html = trElement.InnerHTML;
            var match = regEx.Match(html);

            if (!match.Success)
                throw new Exception("Did not find the duration");

            var hour = match.Groups[1].Value;
            var min = match.Groups[2].Value;
            var sec = match.Groups[3].Value;

            return new TimeSpan(int.Parse(hour), int.Parse(min), int.Parse(sec));
        }


        public string GetUrl(IDomObject trElement) {
            var list = trElement.Cq().Children("td").ToList();
            var html = list[3].FirstElementChild.GetAttribute("href");
            return "https://sv36.tribalwars.se" + html;
        }

        public FlagType GetFlagType(IDomObject trElement) {
            var html = trElement.InnerHTML;
            if (html.Contains("horse_small")) {
                return FlagType.Horse;
            }

            if (html.Contains("boar_small")) {
                return FlagType.Boar;
            }
            if (html.Contains("panther_small")) {
                return FlagType.Panther;
            }
            if (html.Contains("squirrel_small")) {
                return FlagType.Squirrel;
            }
            if (html.Contains("raven_small")) {
                return FlagType.Raven;
            }
            if (html.Contains("dragon_small")) {
                return FlagType.Dragon;
            }

            throw new Exception("Unknows flag type");
        }

    }


    public class MyFlags {

        public Dictionary<FlagType, int> Flags { get; set; }

    }

    public class Oponet {

        public string Name { get; set; }
        public FlagType FlagType { get; set; }
        public TimeSpan Duration { get; set; }
        public string Url { get; set; }

    }

    public enum FlagType {

        Raven,
        Dragon,
        Panther,
        Boar,
        Horse,
        Squirrel

    }

}