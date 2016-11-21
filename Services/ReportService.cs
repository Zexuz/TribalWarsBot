using System.Collections.Generic;
using System.Linq;
using CsQuery;
using TribalWarsBot.Enums;
using TribalWarsBot.Helpers;

namespace TribalWarsBot.Services
{
    public class ReportService
    {
        public List<ReportItem> GetReportItemsFrom(RequestManager requestManager, int from, int village)
        {
            var html = GetHtml(requestManager, from, village);
            var trElements = html.Select("#report_list tbody tr").ToList();

            var list = new List<ReportItem>();
            for (var i = 1; i < trElements.Count - 2; i++)
            {
                var col1 = HtmlParse.GetElementFromHtmlTable(trElements, i, 1);
                var col2 = HtmlParse.GetElementFromHtmlTable(trElements, i, 2);
                list.Add(GetReportItemFromTableRow(col1, col2));
            }

            return list;
        }

        public Report GetReport(RequestManager requestManager, int village, int reportItemId)
        {
            var html = GetReportHtml(requestManager, village, reportItemId);
            var topLevelTableRows =
                html.Select("table.vis tbody tr td.nopad table.vis:nth-child(2) tr").ToList();

            var title = HtmlParse.GetElementFromHtmlTable(topLevelTableRows, 0, 1).InnerText.Trim();
            var timeReceived = HtmlParse.GetElementFromHtmlTable(topLevelTableRows, 1, 1).InnerText.Trim();
            var reportDataEle = HtmlParse.GetElementFromHtmlTable(topLevelTableRows, 2, 0);

            var luckStr = reportDataEle.Cq()["#attack_luck tbody tr"].Text().Trim();
            var luck = RegExHelper.GetdoubleWithRegEx(@"(-?\d+\.\d+)%", luckStr);

            var report = new Report();

            report.Attacker = GetAttackInfo(reportDataEle.Cq(), true);
            report.Defernder = GetAttackInfo(reportDataEle.Cq(), false);
            report.Title = title;
            report.TimeReceived = timeReceived;
            report.Id = reportItemId;

            report.AtteckersLuck = luck;


            return report;
        }

        private VillageInReport GetAttackInfo(CQ html, bool attackTable)
        {
            var str = attackTable ? "att" : "def";

            var tableInfo = html.Select($"#attack_info_{str} tbody tr").ToList();
            var nameLink = HtmlParse.GetElementFromHtmlTable(tableInfo, 0, 1);
            var villageLink = HtmlParse.GetElementFromHtmlTable(tableInfo, 1, 1);

            var unitTable = html.Select($"#attack_info_{str}_units tbody tr").ToList();

            var units = GetUnitsFromReport(unitTable, false, attackTable);
            var unitsLost = GetUnitsFromReport(unitTable, true, attackTable);

            const string regExPatternPlayer = @"/game\.php\?village=\d+&screen=info_player&id=(\d+)";
            const string regExPatternVillage = @"/game\.php\?village=\d+&screen=info_village&id=(\d+)";
            int playerId;

            if (nameLink.InnerText.Equals("---")) playerId = 0;
            else playerId = RegExHelper.GetNumberWithRegEx(regExPatternPlayer, nameLink.InnerHTML);

            var villageId = RegExHelper.GetNumberWithRegEx(regExPatternVillage, villageLink.InnerHTML);


            return new VillageInReport
            {
                LostUnits = unitsLost,
                PlayerId = playerId,
                Units = units,
                VillageId = villageId
            };
        }

        private Dictionary<Units, int> GetUnitsFromReport(List<IDomObject> table, bool lostUnits, bool isAttackTable)
        {
            var maxIndex = isAttackTable ? 11 : 12;
            var row = lostUnits ? 2 : 1;

            var dict = new Dictionary<Units, int>();

            for (var i = 1; i < maxIndex; i++)
            {
                var classes = HtmlParse.GetElementFromHtmlTable(table, row, i).ClassName;
                var typeStr = RegExHelper.GetTextWithRegEx("unit-item-([a-z]+)", classes);
                var type = UnitHelper.GetTypeForString(typeStr);

                var nrStr = HtmlParse.GetElementFromHtmlTable(table, row, i).InnerText;
                var nr = int.Parse(nrStr);
                dict.Add(type, nr);
            }

            return dict;
        }

        private CQ GetReportHtml(RequestManager requestManager, int village, int reportItemId)
        {
            var url = $"{Constants.BaseUrl}village={village}&screen=report&mode=all&group_id=-1&view={reportItemId}";
            var res = requestManager.SendGETRequest(url, null, null, false);
            var resStr = RequestManager.GetResponseStringFromResponse(res);
            return resStr;
        }

        private ReportItem GetReportItemFromTableRow(IDomObject col1, IDomObject col2)
        {
            var textEle = col1.Cq().Select("span.quickedit-content");
            var text = textEle.FirstElement().InnerText.Trim();
            var url = textEle.Select("span.quickedit-content a").FirstElement().GetAttribute("href");
            var dateTime = col2.InnerText;

            var id =
                RegExHelper.GetNumberWithRegEx(
                    @"/game\.php\?village=\d+&screen=report&mode=attack&group_id=-1&view=(\d+)", url);
            var reportItem = new ReportItem
            {
                Id = id,
                Title = text,
                TimeReceived = dateTime
            };

            return reportItem;
        }

        private static CQ GetHtml(RequestManager requestManager, int from, int village)
        {
            var url = $"https://sv36.tribalwars.se/game.php?village={village}&screen=report&mode=attack&from={from}";
            var res = requestManager.SendGETRequest(url, null, null, false);
            var html = RequestManager.GetResponseStringFromResponse(res);
            return html;
        }
    }

    public class ReportItem
    {
        public int Id { get; set; }
        public string TimeReceived { get; set; }
        public string Title { get; set; }
    }

    public class Report : ReportItem
    {
        public double AtteckersLuck { get; set; }
        public string Moral { get; set; }

        public VillageInReport Attacker { get; set; }
        public VillageInReport Defernder { get; set; }

        public Dictionary<ResourceTypes, int> ResSeen { get; set; }

        public Dictionary<BuildingTypes, int> BuildingLevels { get; set; }
        public Dictionary<ResourceTypes, int> Haul;


    }

    public struct VillageInReport
    {
        public int PlayerId { get; set; }
        public int VillageId { get; set; }
        public Dictionary<Units, int> Units { get; set; }
        public Dictionary<Units, int> LostUnits { get; set; }
    }
}