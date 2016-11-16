using System;
using System.IO;
using System.Text.RegularExpressions;

using TribalWarsBot.Screens;
using TribalWarsBot.Services;

namespace TribalWarsBot {

    internal class Program {

        private string _csrfToken;
        private string _currentVillage;

        private void Start() {
            var reqManager = new RequestManager();
            var loginService = new LoginService("newUser", "0000", reqManager);
            loginService.DoLogin();

            SetCsrfTokenAndCurrentVillage(reqManager);

            if(_csrfToken == null || _currentVillage == null)
                throw new Exception("_csrfToken or _currentVillage is not set!");

            Console.WriteLine("Login succeded!");

            var buildingService = new BuildingService(reqManager);

            Console.WriteLine($"The HQ is level {buildingService.GetBuildingLevel(Buildings.Main)}");
            Console.WriteLine($"The Barracks is level {buildingService.GetBuildingLevel(Buildings.Barracks)}");
            Console.WriteLine($"The Stable is level {buildingService.GetBuildingLevel(Buildings.Stable)}");

            var succes = buildingService.UppgradeBuilding(Buildings.Wall, _csrfToken, _currentVillage);
            Console.WriteLine(succes ? "Upgrading the building" : "Error, upgrade not registered");
        }


        private void SetCsrfTokenAndCurrentVillage(RequestManager requestManager) {
            const string url = "https://sv36.tribalwars.se/game.php?village=2173&screen=overview";
            var req = requestManager.GenerateGETRequest(url, null, null, true);
            var res = requestManager.GetResponse(req);
            var htmlString = RequestManager.GetResponseStringFromResponse(res);

            SetCurrentVillage(htmlString);
            SetCsrfToken(htmlString);
        }


        private void SetCurrentVillage(string html) {
            var regEx = new Regex(@"/game\.php\?village=([\d]+)&screen=");

            var match = regEx.Match(html);

            if (!match.Success)
                throw new Exception("Did not find the csrf token");

            _currentVillage = match.Groups[1].Value;
        }

        private void SetCsrfToken(string html) {
            var regEx = new Regex(@"var csrf_token = '([a-zA-Z0-9]+)';");

            var match = regEx.Match(html);


            if (!match.Success)
                throw new Exception("Did not find the csrf token");

            _csrfToken = match.Groups[1].Value;
        }

        public static void Main(string[] args) {
            new Program().Start();
        }

    }

}