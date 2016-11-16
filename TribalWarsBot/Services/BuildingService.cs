using System;
using System.IO;
using System.Text.RegularExpressions;

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


        public int GetBuildingLevel(BuildingTypes builing) {
            const string str = "#main_buildrow_";
            var html = GetHeadQScreenHtml();
            var id = $"{str}{BuildingHelper.GetNameForType(builing)}";
            var hqTableElement = html.Select(id);

            return HtmlParse.GetCurrentLevelOfBuilingFromTableRow(hqTableElement);
        }

        public bool CancelBuildingUpgrade(BuildingTypes building, string csrfToken, string currentVillage) {
            var id = GetBuildingQueueId(GetHeadQScreenHtml());

            var cancelOrderUrl = CancelOrderUrl
                .Replace("__village__", currentVillage)
                .Replace("__type__", "main")
                .Replace("__csrfToken__", csrfToken);

            var url = $"{BaseUrl}{cancelOrderUrl}";
            var postData = $"id={id}&destroy=0";

            var resNotParsed = _reqManager.GeneratePOSTRequest(url, postData, null, null, true);
            var res = _reqManager.GetResponse(resNotParsed);
            var html = RequestManager.GetResponseStringFromResponse(res);

            return html.Contains("success\":true");
        }


        public bool UppgradeBuilding(BuildingTypes building, string csrfToken, string currentVillage) {
            var uppgradeUrl = UpgradeBuildingUrl
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

        private CQ GetHeadQScreenHtml() {
            var url = "https://sv36.tribalwars.se/game.php?village=2145&screen=main";
            var req = _reqManager.GenerateGETRequest(url, null, null, true);
            var res = _reqManager.GetResponse(req);

            return RequestManager.GetResponseStringFromResponse(res);
        }

        private string GetBuildingQueueId(CQ html) {
            var tableRowElement = html.Select("#buildqueue tr.buildorder_wall");
            var cancelButtonHref = tableRowElement.Select("td.lit-item a.btn.btn-cancel").Attr("href");

            var regEx = new Regex(@"/game\.php\?village=\d+&screen=main&action=cancel&id=(\d+)&mode=build");
            var match = regEx.Match(cancelButtonHref);

            if (!match.Success)
                throw new Exception("Did not find the building id");

            var id = match.Groups[1].Value;
            return id;
        }

    }

}