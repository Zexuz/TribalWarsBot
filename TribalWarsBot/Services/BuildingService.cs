using System;
using System.IO;

using CsQuery;

using TribalWarsBot.Screens;

namespace TribalWarsBot.Services {

    public class BuildingService {

        private readonly RequestManager _reqManager;

        public BuildingService(RequestManager reqManager) {
            _reqManager = reqManager;
        }


        public int GetBuildingLevel(Buildings builing) {
            const string str = "#main_buildrow_";
            var html = GetHeadQScreenHtml();
            var id = $"{str}{GetBuldingNameFromEnumType.Get(builing)}";
            var hqTableElement = html.Select(id);

            return HtmlParse.GetCurrentLevelOfBuilingFromTableRow(hqTableElement);
        }


        private CQ GetHeadQScreenHtml() {
            var req = _reqManager.GenerateGETRequest("https://sv36.tribalwars.se/game.php?village=2145&screen=main",
                null,
                null, true);


            CQ html;
            using (var stream = new StreamReader(_reqManager.GetResponse(req).GetResponseStream())) {
                html = stream.ReadToEnd();
            }

            return html;
        }

    }

}