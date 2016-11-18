﻿using System;
using System.Collections.Generic;
using System.Threading;

using TribalWarsBot.Domain;
using TribalWarsBot.Enums;
using TribalWarsBot.Helpers;
using TribalWarsBot.Screens.Structures;
using TribalWarsBot.Services;

namespace TribalWarsBot {

    internal class Program {

        private RootObject _rootObject;

        private void Start() {
            var reqManager = new RequestManager();
            var loginService = new PlayerService(reqManager);
            loginService.DoLogin("kalle hjularbo", "961107");

            _rootObject = loginService.GetPlayerAndCurrentVillageInfo();

            if (_rootObject.csrf == null || _rootObject.village.id == 0)
                throw new Exception("_csrfToken or _currentVillage is not set!");

            Console.WriteLine("Login succeded!");
            var eventSevice = new EventService(reqManager);

            while (true) {
                var masters = eventSevice.GetAvalibleMasters();

                if (masters == 0) {
                    Console.WriteLine($"Sleeping for 2 min {DateTime.Now:hh:mm:ss}");
                    Thread.Sleep(60 * 2 * 1000);
                    continue;
                }

                var myFlags = eventSevice.GetMyFlags();
                var oponents = eventSevice.GetOponents(_rootObject.csrf);

                foreach (var op in oponents) {
                    if (myFlags.Flags[op.FlagType] > 4) {
                        continue;
                    }

                    Console.WriteLine($"Send to get {op.FlagType}");

                    Console.WriteLine(op.Url);
                   var req = reqManager.GenerateGETRequest(op.Url, null, null, true);
                    var res = reqManager.GetResponse(req);
                    var htmlres = RequestManager.GetResponseStringFromResponse(res);
                }
            }


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


        public static void Main(string[] args) {
            new Program().Start();
        }

    }

}