using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

using CsQuery;
using CsQuery.StringScanner.ExtensionMethods;

using Newtonsoft.Json;

using TribalWarsBot.Helpers;
using TribalWarsBot.Screens;
using TribalWarsBot.Screens.Structures;
using TribalWarsBot.Services;

namespace TribalWarsBot {

    internal class Program {

        private RootObject _rootObject;

        private void Start() {
            var reqManager = new RequestManager();
            var loginService = new PlayerService(reqManager);
            loginService.DoLogin("newUser", "0000");

            _rootObject = loginService.GetPlayerAndCurrentVillageInfo();

            if (_rootObject.csrf == null || _rootObject.village.id == 0)
                throw new Exception("_csrfToken or _currentVillage is not set!");

            Console.WriteLine("Login succeded!");
            var data = SendAttackInit(reqManager, _rootObject.village.id);
            SendAttackConfirm(reqManager,data,_rootObject.csrf);

            var buildingService = new BuildingService(reqManager);

//            Console.WriteLine($"The HQ is level {buildingService.GetBuildingLevel(BuildingTypes.Main)}");
//            Console.WriteLine($"The Barracks is level {buildingService.GetBuildingLevel(BuildingTypes.Barracks)}");
//            Console.WriteLine($"The Stable is level {buildingService.GetBuildingLevel(BuildingTypes.Stable)}");
//            Console.WriteLine($"The Storage is level {buildingService.GetBuildingLevel(BuildingTypes.Storage)}");

            var storage = new Storage(0);


            var timeLeftWood = storage.TimeLeftUntillWoodFull(_rootObject);
            var woodStr = TimeFormater.TimeSpanToString(timeLeftWood);
            Console.WriteLine($"time untill wood is full {woodStr}");

            var timeLeftClay = storage.TimeLeftUntillClayFull(_rootObject);
            var clayStr = TimeFormater.TimeSpanToString(timeLeftClay);
            Console.WriteLine($"time untill clay is full {clayStr}");

            var timeLeftIron = storage.TimeLeftUntillIronFull(_rootObject);
            var ironStr = TimeFormater.TimeSpanToString(timeLeftIron);
            Console.WriteLine($"time untill iron is full {ironStr}");


//            var upgradingReqStatus = buildingService.UppgradeBuilding(BuildingTypes.Wall, _csrfToken, _currentVillage);
//            Console.WriteLine(upgradingReqStatus ? "Upgrading the building" : "Error, upgrade not registered");
//
//            var canselReqStatus = buildingService.CancelBuildingUpgrade(BuildingTypes.Wall,_csrfToken,_currentVillage);
//            Console.WriteLine(canselReqStatus ? "Canceling the order" : "Error, the order was not registered");
        }

        public string SendAttackInit(RequestManager requestManager, int villageNr) {
            var url = $"https://sv36.tribalwars.se/game.php?village={villageNr}&screen=place&try=confirm";
            var postData =
                "22c2d931d74cd8a06d92b4=e6c57ef722c2d9&template_id=&source_village=2173&spear=&sword=63&axe=&spy=&light=&heavy=&ram=&catapult=&knight=&snob=&x=525&y=473&target_type=coord&input=&attack=Attack";
            var req = requestManager.GeneratePOSTRequest(url, postData, null, null, true);
            var res = requestManager.GetResponse(req);

            //post data 22c2d931d74cd8a06d92b4=e6c57ef722c2d9&template_id=&source_village=2173&spear=&sword=63&axe=&spy=&light=&heavy=&ram=&catapult=&knight=&snob=&x=525&y=473&target_type=coord&input=&attack=Attack
            // Response html is in attacjResponseHtml
            var str = RequestManager.GetResponseStringFromResponse(res);
            CQ html = str;
            var formElement = html.Select("#command-data-form");
            var inputs = formElement.Children("input").Where(ele => ele.Type == "hidden").ToList();

            var queryString = inputs.Aggregate("", (current, input) => current + $"{input.Name}={input.Value}&");
            return queryString;
        }


        public void SendAttackConfirm(RequestManager requestManager, string postData,string token) {
            var url = $"https://sv36.tribalwars.se/game.php?village=2173&screen=place&action=command&h={token}";
            var req =requestManager.GeneratePOSTRequest(url, postData, null, null, true);
            var res = requestManager.GetResponse(req);
            var htmlResponse = RequestManager.GetResponseStringFromResponse(res);
            //https://sv36.tribalwars.se/game.php?village=2173&screen=place&action=command&h=3d778f4e
            //post data attack=true&ch=5691301055711e723e118f2f603014f44c1ba158&x=525&y=473&source_village=2173&action_id=110047&spear=0&sword=63&axe=0&spy=0&light=0&heavy=0&ram=0&catapult=0&knight=0&snob=0&building=main
        }

        public void PrepareToSendAttack() {}


        public static void Main(string[] args) {
            new Program().Start();
        }

    }

}