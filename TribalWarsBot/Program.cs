using System;
using System.IO;

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

            Console.WriteLine("Login succeded!");

            var buildingService = new BuildingService(reqManager);

            Console.WriteLine($"The HQ is level {buildingService.GetBuildingLevel(Buildings.Main)}");
            Console.WriteLine($"The Barracks is level {buildingService.GetBuildingLevel(Buildings.Barracks)}");
            Console.WriteLine($"The Stable is level {buildingService.GetBuildingLevel(Buildings.Stable)}");

            var url =
                "https://sv36.tribalwars.se/game.php?village=2173&screen=main&ajaxaction=upgrade_building&type=main&h=5b639478";

            var postData = "id=iron&force=1&destroy=0&source=2173";
            var resNotParsed = reqManager.GeneratePOSTRequest(url,postData , null, null, true);
            var res = reqManager.GetResponse(resNotParsed);
            Console.WriteLine(res.StatusCode);

            using (var stream = new StreamReader(res.GetResponseStream())) {
                Console.WriteLine(stream.ReadToEnd());
            }

            Console.WriteLine($"The HQ is level {buildingService.GetBuildingLevel(Buildings.Main)}");
            Console.WriteLine($"The Barracks is level {buildingService.GetBuildingLevel(Buildings.Barracks)}");
            Console.WriteLine($"The Stable is level {buildingService.GetBuildingLevel(Buildings.Stable)}");
        }

    }

}