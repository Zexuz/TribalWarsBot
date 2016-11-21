using System;
using System.Collections.Generic;
using System.Configuration;
using System.Threading;
using TribalWarsBot.Cache;
using TribalWarsBot.Domain;
using TribalWarsBot.Enums;
using TribalWarsBot.Helpers;
using TribalWarsBot.Services;

namespace TribalWarsBot
{
    internal class Program
    {
        private RootObject _rootObject;

        private void Start()
        {
            var userName = ConfigurationManager.AppSettings["username"];
            var userPassword = ConfigurationManager.AppSettings["password"];
            var user = new User(userName, userPassword);

            var reqCache = new RequestCache(user);
            var loginService = new PlayerService();

            reqCache.DoLogin();
            _rootObject = loginService.GetPlayerAndCurrentVillageInfo(reqCache);

            if (_rootObject.csrf == null || _rootObject.village.id == 0)
                throw new Exception("_csrfToken or _currentVillage is not set!");

            Console.WriteLine("Login succeded!");

            while (true)
            {
                var villageCache = new WorldMapVillageCache();
                var worldMapVillageService = new WorldMapVillageService(villageCache);
                var barbarianVillages = worldMapVillageService.GetBarbarianVillagesInRange(5, 507, 530);

                Console.WriteLine($"Nr of villages {barbarianVillages.Count}");

                var attackService = new AttackService();
                var unitService = new UnitService();

                foreach (var village in barbarianVillages)
                {
                    var planedAttack = new PlanedAttack
                    {
                        Units = new Dictionary<Units, int>
                        {
                            {Units.Light, 4},
                            {Units.Spy, 1}
                        },
                        Attacker = _rootObject.village,
                        EnemyVillageXCord = village.X,
                        EnemyVillageYCord = village.Y
                    };

                    var unitsInVillageNow = unitService.GetMyUnitsInVillage(reqCache.Manager, _rootObject.village.id);
                    var canSendAttack = true;
                    foreach (var keyValuePair in planedAttack.Units)
                    {
                        if (unitsInVillageNow[keyValuePair.Key] < keyValuePair.Value) canSendAttack = false;
                    }

                    if (!canSendAttack)
                    {
                        Console.WriteLine("We don't have enough units to send the attack!");
                        continue;
                    }

                    attackService.SendAttack(reqCache.Manager, _rootObject, planedAttack);
                    Thread.Sleep(200 + GetRandomInt(0, 500));
                }

                Console.WriteLine($"Sleeping for half hour, {DateTime.Now:hh:mm:ss}");
                Thread.Sleep(60 * 35 * 1000);
            }

            #region Comments....

            /*       var barrackService = new UnitService();

                   Console.WriteLine("barracks");
                   foreach (var item in barrackService.GetActiveQueueForBarracks(reqCache.Manager, _rootObject.village.id))
                   {
                       Console.WriteLine(item);
                   }

                   Console.WriteLine("stable");
                   foreach (var item in barrackService.GetActiveQueueForStable(reqCache.Manager, _rootObject.village.id))
                   {
                       Console.WriteLine(item);
                   }

                   Environment.Exit(0);*/

            /*foreach (var buildingQueueItem in new BuildingService(reqCache.Manager).GetActiveBuilingQueue())
            {
                Console.WriteLine(buildingQueueItem);
            }

            var buildingService = new BuildingService(reqCache.Manager);
            const BuildingTypes type = BuildingTypes.Stable;
            var csrfToken = _rootObject.csrf;
            var villageId = _rootObject.village.id;
            buildingService.AddBuildingUppgradeToActiveQeueu(type, csrfToken, villageId);

            foreach (var buildingQueueItem in buildingService.GetActiveBuilingQueue())
            {
                Console.WriteLine(buildingQueueItem);
            }

            buildingService.CancelBuildingUpgradeFromActiveQueue(buildingService.GetActiveBuilingQueue()[2].Id, csrfToken, villageId);

            foreach (var buildingQueueItem in new BuildingService(reqCache.Manager).GetActiveBuilingQueue())
            {
                Console.WriteLine(buildingQueueItem);
            }
*/

//            var units = new Dictionary<Units, int>
//            {
//                {Units.Spear, 1}
//            };
//            new UnitService(reqCache.Manager).AddOrderToActiveQueue(units, _rootObject.csrf, _rootObject.village.id);
//            new BuildingService(reqManager).GetActiveBuilingQueue();

            /*   var eventSevice = new EventService(reqManager);
               var masterLimit = 0;
               while (true) {
                   var masters = eventSevice.GetAvalibleMasters();

                   if (masters <= masterLimit) {
                       var sleepInMs = ( GetRandomInt(0, 120)) * 1000;
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

                       if(masters <= masterLimit)continue;
                       masters -= 1;

                       Console.WriteLine($"Send to get {op.FlagType}");

                       Console.WriteLine(op.Url);
                      var req = reqManager.GenerateGETRequest(op.Url, null, null, true);
                       var res = reqManager.GetResponse(req);
                       var htmlres = RequestManager.GetResponseStringFromResponse(res);
                   }
               }*/

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

            #endregion
        }

        private int GetRandomInt(int min, int max)
        {
            var random = new Random();
            return random.Next(min, max);
        }


        public static void Main(string[] args)
        {
            new Program().Start();
        }
    }
}