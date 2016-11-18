using System;
using System.Threading;

using TribalWarsBot.Helpers;
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
            new BuildingService(reqManager).GetActiveBuilingQueue();

           /* var eventSevice = new EventService(reqManager);

            while (true) {
                var masters = eventSevice.GetAvalibleMasters();

                if (masters == 0) {
                    var sleepInMs = (60 * 2 + GetRandomInt(0, 300)) * 1000;
                    var timeSpan = new TimeSpan(0, 0, 0, 0, sleepInMs);
                    var timeStr = $"minutes {timeSpan.Minutes}, sec {timeSpan.Seconds}";
                    var currentTime = DateTime.Now;
                    Console.Write($"Sleeping for {timeStr}, CurrenTime {currentTime:hh:mm:ss}, Running again at ");
                    var dateTime = currentTime.Add(timeSpan);
                    Console.WriteLine($"{dateTime:hh:mm:ss}");
                    Thread.Sleep(sleepInMs);
                    continue;
                }

                var myFlags = eventSevice.GetMyFlags();
                var oponents = eventSevice.GetOponents(_rootObject.csrf);

                foreach (var op in oponents) {
                    if (myFlags.Flags[op.FlagType] > 4) {
                        continue;
                    }

                    if(masters == 0)continue;
                    masters -= 1;

                    Console.WriteLine($"Send to get {op.FlagType}");

                    Console.WriteLine(op.Url);
                   var req = reqManager.GenerateGETRequest(op.Url, null, null, true);
                    var res = reqManager.GetResponse(req);
                    var htmlres = RequestManager.GetResponseStringFromResponse(res);
                }
            }
*/

            /*  new MapService(reqManager).GetMapForGrid(480,500);

              var buildingService = new BuildingService(reqManager);
              var attackService = new AttackService(reqManager);

              var planedAttack = new PlanedAttack {
                  Units = new Dictionary<Units, int> {
                      {Units.Spear, 10},
                      {Units.Sword, 10}
                  },
                  Attacker = _rootObject.village,
                  EnemyVillageXCord = 521,
                  EnemyVillageYCord = 474
              };


              attackService.SendAttack(_rootObject, planedAttack);

              Console.WriteLine("Attack sent!");

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
  */

//            var upgradingReqStatus = buildingService.UppgradeBuilding(BuildingTypes.Wall, _csrfToken, _currentVillage);
//            Console.WriteLine(upgradingReqStatus ? "Upgrading the building" : "Error, upgrade not registered");
//
//            var canselReqStatus = buildingService.CancelBuildingUpgrade(BuildingTypes.Wall,_csrfToken,_currentVillage);
//            Console.WriteLine(canselReqStatus ? "Canceling the order" : "Error, the order was not registered");
        }

        private int GetRandomInt(int min,int max) {
            var random = new Random();
            return random.Next(min,max);
        }


        public static void Main(string[] args) {
            new Program().Start();
        }

    }

}