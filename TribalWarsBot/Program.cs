using System;
using System.IO;
using System.Net;

using CsQuery;

using TribalWarsBot.Screens;
using TribalWarsBot.Services;

namespace TribalWarsBot {

    internal class Program {

        public static void Main(string[] args) {
            new Program().Start();
        }

        private void Start() {
            var reqManager = new RequestManager();
            var loginService = new LoginService("newUser", "0000", reqManager);
            loginService.DoLogin();

            var req = reqManager.GenerateGETRequest("https://sv36.tribalwars.se/game.php?village=2145&screen=main", null,
                null, true);


            CQ html;
            using (var stream = new StreamReader(reqManager.GetResponse(req).GetResponseStream())) {
                html = stream.ReadToEnd();
            }

            var str = "#main_buildrow_";
            var hqLevel = html.Select($"{str}{GetBuldingNameFromEnumType.Get(Buildings.Main)}");
            var barrackLevel = html.Select($"{str}{GetBuldingNameFromEnumType.Get(Buildings.Barracks)}");
            var stableLevel = html.Select($"{str}{GetBuldingNameFromEnumType.Get(Buildings.Stable)}");



            Console.WriteLine($"The HQ is level {HtmlParse.GetCurrentLevelOfBuilingFromTableRow(hqLevel)}");
            Console.WriteLine($"The Barracks is level {HtmlParse.GetCurrentLevelOfBuilingFromTableRow(barrackLevel)}");
            Console.WriteLine($"The Stable is level {HtmlParse.GetCurrentLevelOfBuilingFromTableRow(stableLevel)}");
        }

    }

}