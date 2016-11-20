using System;
using System.Collections.Generic;
using System.Linq;
using CsQuery;
using TribalWarsBot.Enums;
using TribalWarsBot.Helpers;

namespace TribalWarsBot.Services
{
    public class BarrackService
    {
        public bool AddOrderToActiveQueue(RequestManager reqManager, Dictionary<Units, int> units, string token,
            int village)
        {
            var url =
                $"https://sv36.tribalwars.se/game.php?village={village}&screen=train&ajaxaction=train&mode=train&h={token}";
            var postData = "";
            foreach (var unitKeyPair in units)
            {
                var unitName = UnitHelper.GetNameForType(unitKeyPair.Key);
                postData += $"units%5B{unitName}%5D={unitKeyPair.Value}&";
            }
            var res = reqManager.SendPOSTRequest(url, postData, null, null, true);
            var jsonRes = RequestManager.GetResponseStringFromResponse(res);

            return jsonRes.Contains("success:true");
        }

        public bool CancelOrderFromActiveQueue(RequestManager reqManager, int orderNr, string token, int village)
        {
            var url =
                $"https://sv36.tribalwars.se/game.php?village={village}&screen=stable&ajaxaction=cancel&h={token}";
            var postData = $"id={orderNr}";
            var res = reqManager.SendPOSTRequest(url, postData, null, null, true);
            var jsonRes = RequestManager.GetResponseStringFromResponse(res);

            return jsonRes.Contains("success:true");
        }

        public List<UnitQueueItem> GetActiveQueue(RequestManager reqManager, int village)
        {
            var url = $"https://sv36.tribalwars.se/game.php?village={village}&screen=barracks";
            var res = reqManager.SendGETRequest(url, null, null, true);
            CQ htmlString = RequestManager.GetResponseStringFromResponse(res);
            var list = htmlString
                .Select("#trainqueue_wrap_barracks tbody tr")
                .Where(ele => ele.ClassName.Length > 0)
                .Select(GetUnitQueueItem);

            return list.ToList();
        }

        private static string GetOrderQueueId(IDomObject item)
        {
            var pattern = @"/game\.php\?village=\d+&screen=barracks&action=cancel&id=(\d+)";
            return RegExHelper.GetTextWithRegEx(pattern, item.OuterHTML);
        }

        private static UnitQueueItem GetUnitQueueItem(IDomObject item)
        {
            CQ itemCq = item.OuterHTML;
            var queantAndTypeStr = itemCq.Select("div.unit_sprite.unit_sprite_smaller").ToList().First();

            var type = UnitHelper.GetTypeForString(queantAndTypeStr.Classes.ToList().Last());
            var domElements = item.ChildElements.ToList();
            var quantityStr = RegExHelper.GetTextWithRegEx(@"(\d+)", domElements[0].InnerText);

            var timeUntillCompleeteStr = itemCq.Text();
            var timeUntillCompleete = RegExHelper.GetTimeFromString(timeUntillCompleeteStr);

            var queueItem = new UnitQueueItem
            {
                Quantity = Convert.ToInt32(quantityStr),
                Type = type,
                TimeLeft = timeUntillCompleete,
                Id = GetOrderQueueId(item)
            };

            return queueItem;
        }
    }

    public class UnitQueueItem
    {
        public Units Type { get; set; }
        public int Quantity { get; set; }
        public RegExHelper.Time TimeLeft { get; set; }
        public string Id { get; set; }

        public override string ToString()
        {
            return $"The unit {Type} is recuruting {Quantity}, Duration: {TimeLeft}, Id:{Id}";

        }
    }
}