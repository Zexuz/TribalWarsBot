using System;
using System.Collections.Generic;
using System.Linq;

using CsQuery;

using TribalWarsBot.Enums;
using TribalWarsBot.Helpers;

namespace TribalWarsBot.Services {

    public class UnitService {

        public bool AddOrderToActiveQueue(RequestManager reqManager, Dictionary<Units, int> units, string token,
            int village) {
            var url =
                $"https://sv36.tribalwars.se/game.php?village={village}&screen=train&ajaxaction=train&mode=train&h={token}";
            var postData = "";
            foreach (var unitKeyPair in units) {
                var unitName = UnitHelper.GetNameForType(unitKeyPair.Key);
                postData += $"units%5B{unitName}%5D={unitKeyPair.Value}&";
            }

            var res = reqManager.SendPOSTRequest(url, postData, null, null, true);
            var jsonRes = RequestManager.GetResponseStringFromResponse(res);

            return jsonRes.Contains("success:true");
        }

        public bool CancelOrderFromActiveQueue(RequestManager reqManager, int orderNr, string token, int village) {
            var url =
                $"https://sv36.tribalwars.se/game.php?village={village}&screen=train&ajaxaction=cancel&h={token}";
            var postData = $"id={orderNr}";
            var res = reqManager.SendPOSTRequest(url, postData, null, null, true);
            var jsonRes = RequestManager.GetResponseStringFromResponse(res);

            return jsonRes.Contains("success:true");
        }

        public List<UnitQueueItem> GetActiveQueueForBarracks(RequestManager reqManager, int village) {
            return GetActiveQueue(reqManager, village, BuildingTypes.Barracks);
        }

        public List<UnitQueueItem> GetActiveQueueForStable(RequestManager reqManager, int village) {
            return GetActiveQueue(reqManager, village, BuildingTypes.Stable);
        }

        public Dictionary<Units, int> GetMyUnitsInVillage(RequestManager reqManager,int village) {
            CQ html = GetPlaceHtml(reqManager,village);
            var units = new Dictionary<Units,int>();

            var selector = "form#command-data-form table tbody table.vis td.nowrap a.units-entry-all";
            var linkList = html.Select(selector).ToList();

            foreach (var item in linkList) {
                var typeStr = RegExHelper.GetTextWithRegEx(@"units_entry_all_([a-z]+)",item.Id);
                var type = UnitHelper.GetTypeForString(typeStr);

                var nrOfUnits = RegExHelper.GetNumberWithRegEx(@"\((\d+)\)",item.InnerText);

                if(nrOfUnits == 0) continue;
                units.Add(type,nrOfUnits);
            }

            return units;

        }

        private static string GetPlaceHtml(RequestManager reqManager,int village) {
            string url = $"https://sv36.tribalwars.se/game.php?village={village}&screen=place";
            var res =reqManager.SendGETRequest(url, null, null, false);
            return RequestManager.GetResponseStringFromResponse(res);
        }

        private static List<UnitQueueItem> GetActiveQueue(RequestManager reqManager, int village, BuildingTypes building) {
            var typeStr = BuildingHelper.GetNameForType(building);
            var url = $"https://sv36.tribalwars.se/game.php?village={village}&screen={typeStr}";
            var res = reqManager.SendGETRequest(url, null, null, true);
            CQ htmlString = RequestManager.GetResponseStringFromResponse(res);
            var list = htmlString
                .Select($"#trainqueue_wrap_{typeStr} tbody tr")
                .Where(ele => ele.ClassName.Length > 0)
                .Select(ele => GetUnitQueueItem(ele, typeStr));

            return list.ToList();
        }

        private static int GetOrderQueueId(IDomObject item, string building) {
            var pattern = @"/game\.php\?village=\d+&screen=__building__&action=cancel&id=(\d+)";
            var replacedString = pattern.Replace("__building__", building);
            return RegExHelper.GetNumberWithRegEx(replacedString, item.OuterHTML);
        }

        private static UnitQueueItem GetUnitQueueItem(IDomObject item, string typeStr) {
            CQ itemCq = item.OuterHTML;
            var queantAndTypeStr = itemCq.Select("div.unit_sprite.unit_sprite_smaller").ToList().First();

            var type = UnitHelper.GetTypeForString(queantAndTypeStr.Classes.ToList().Last());
            var domElements = item.ChildElements.ToList();
            var quantity = RegExHelper.GetNumberWithRegEx(@"(\d+)", domElements[0].InnerText);

            var timeUntillCompleeteStr = itemCq.Text();
            var timeUntillCompleete = RegExHelper.GetTimeFromString(timeUntillCompleeteStr);

            var queueItem = new UnitQueueItem {
                Quantity = quantity,
                Type = type,
                TimeLeft = timeUntillCompleete,
                Id = GetOrderQueueId(item, typeStr)
            };

            return queueItem;
        }

    }

    public class UnitQueueItem {

        public Units Type { get; set; }
        public int Quantity { get; set; }
        public RegExHelper.Time TimeLeft { get; set; }
        public int Id { get; set; }

        public override string ToString() {
            return $"The unit {Type} is recuruting {Quantity}, Duration: {TimeLeft}, Id:{Id}";
        }

    }

}