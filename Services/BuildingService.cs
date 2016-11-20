using System.Collections.Generic;
using System.Linq;
using CsQuery;
using TribalWarsBot.Domain.ValueObjects;
using TribalWarsBot.Enums;
using TribalWarsBot.Helpers;
using static TribalWarsBot.Helpers.Constants;


namespace TribalWarsBot.Services
{
    public class BuildingService
    {
        private readonly RequestManager _reqManager;

        public BuildingService(RequestManager reqManager)
        {
            _reqManager = reqManager;
        }

        public int GetBuildingLevel(BuildingTypes builing)
        {
            const string str = "#main_buildrow_";
            var html = GetHeadQScreenHtml();
            var id = $"{str}{BuildingHelper.GetNameForType(builing)}";
            var hqTableElement = html.Select(id);

            return HtmlParse.GetCurrentLevelOfBuilingFromTableRow(hqTableElement);
        }

        public List<BuildingQueueItem> GetActiveBuilingQueue()
        {
            var matchesList = GetHeadQScreenHtml().Select("tbody#buildqueue tr").ToList();

            var list = matchesList
                .Where(ele => ele.ClassName.Contains("buildorder"))
                .Select(GetBuildingQueueItem).ToList();

            return list;
        }

        public bool CancelBuildingUpgradeFromActiveQueue(string id, string csrfToken, int currentVillage)
        {
            var cancelOrderUrl = CancelOrderUrl
                .Replace("__village__", currentVillage.ToString())
                .Replace("__type__", "main")
                .Replace("__csrfToken__", csrfToken);

            var url = $"{BaseUrl}{cancelOrderUrl}";
            var postData = $"id={id}&destroy=0";

            var resNotParsed = _reqManager.GeneratePOSTRequest(url, postData, null, null, true);
            var res = _reqManager.GetResponse(resNotParsed);
            var html = RequestManager.GetResponseStringFromResponse(res);

            return html.Contains("success\":true");
        }

        public bool AddBuildingUppgradeToActiveQeueu(BuildingTypes building, string csrfToken, int currentVillage)
        {
            var uppgradeUrl = UpgradeBuildingUrl
                .Replace("__village__", currentVillage.ToString())
                .Replace("__type__", "main")
                .Replace("__csrfToken__", csrfToken);

            var url = $"{BaseUrl}{uppgradeUrl}";
            var postData = $"id={BuildingHelper.GetNameForType(building)}&force=1&destroy=0&source={currentVillage}";

            var resNotParsed = _reqManager.GeneratePOSTRequest(url, postData, null, null, true);
            var res = _reqManager.GetResponse(resNotParsed);
            var html = RequestManager.GetResponseStringFromResponse(res);

            return html.ToString().Contains("Byggnationen har beordrats");
        }

        private CQ GetHeadQScreenHtml()
        {
            var url = "https://sv36.tribalwars.se/game.php?village=2145&screen=main";
            var req = _reqManager.GenerateGETRequest(url, null, null, true);
            var res = _reqManager.GetResponse(req);

            return RequestManager.GetResponseStringFromResponse(res);
        }

        private BuildingQueueItem GetBuildingQueueItem(IDomObject item)
        {
            CQ trElement = item.OuterHTML;
            var timeUntillCompleeteStr = trElement.Select("td span").FirstElement().TextContent;
            var timeUntillCompleete = RegExHelper.GetTimeFromString(timeUntillCompleeteStr);

            var builingName = RegExHelper.GetTextWithRegEx("buildorder_([a-z]+)", item.ClassName);
            var buildingType = BuildingHelper.GetBuildingTypeFromString(builingName);

            var subString = item.FirstElementChild.InnerText.Split('\n')[1];
            var lvl = int.Parse(RegExHelper.GetTextWithRegEx(@"([\d]+)", subString));
            var id = GetBuildingQueueId(item);
            var buildingQueueItem = new BuildingQueueItem
            {
                Level = lvl,
                Type = buildingType,
                TimeLeft = timeUntillCompleete,
                Id = id
            };

            return buildingQueueItem;
        }

        private string GetBuildingQueueId(IDomObject item)
        {
            var pattern = @"/game\.php\?village=\d+&screen=main&action=cancel&id=(\d+)&mode=build";
            return RegExHelper.GetTextWithRegEx(pattern, item.OuterHTML);
        }
    }
}