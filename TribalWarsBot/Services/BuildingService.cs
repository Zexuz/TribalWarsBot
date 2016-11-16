using System;
using System.IO;

using CsQuery;

using TribalWarsBot.Helpers;

using static TribalWarsBot.Helpers.Constants;
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
            var id = $"{str}{BuildingHelper.GetNameForType(builing)}";
            var hqTableElement = html.Select(id);

            return HtmlParse.GetCurrentLevelOfBuilingFromTableRow(hqTableElement);
        }


        private CQ GetHeadQScreenHtml() {
            var url = "https://sv36.tribalwars.se/game.php?village=2145&screen=main";
            var req = _reqManager.GenerateGETRequest(url, null, null, true);
            var res = _reqManager.GetResponse(req);

            return RequestManager.GetResponseStringFromResponse(res);
        }

        public bool UppgradeBuilding(Buildings building, string csrfToken, string currentVillage) {

            var uppgradeUrl = UppgradeBuildingUrl
                .Replace("__village__", currentVillage)
                .Replace("__type__", "main")
                .Replace("__csrfToken__", csrfToken);

            var url = $"{BaseUrl}{uppgradeUrl}";
            var postData = $"id={BuildingHelper.GetNameForType(building)}&force=1&destroy=0&source={currentVillage}";

            var resNotParsed = _reqManager.GeneratePOSTRequest(url, postData, null, null, true);
            var res = _reqManager.GetResponse(resNotParsed);
            var html = RequestManager.GetResponseStringFromResponse(res);

            return html.ToString().Contains("Byggnationen har beordrats");
        }

    }

}