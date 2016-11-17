using System;
using System.Collections.Generic;
using System.Linq;

using TribalWarsBot.Domain.ValueObjects;
using TribalWarsBot.Enums;
using TribalWarsBot.Helpers;
using TribalWarsBot.Interfacies;
using TribalWarsBot.Services;

namespace TribalWarsBot.Domain {

    public class Attack : PlanedAttack {

        public DateTime TimeSent { get; }
        public TimeSpan TravelTime { get; }

        public AttackStatus Status { get; }

        public Attack(PlanedAttack planedAttack) {
            TimeSent = DateTime.Now;

            Attacker = planedAttack.Attacker;
            EnemyVillageXCord = planedAttack.EnemyVillageXCord;
            EnemyVillageYCord = planedAttack.EnemyVillageYCord;
            Units = planedAttack.Units;

            var dX = Attacker.x - EnemyVillageXCord;
            var dY = Attacker.y - EnemyVillageYCord;


            var distance = Math.Sqrt(dX * dX + dY * dY);

            var typeAndTime = GetSlowestUnit(Units);
            var durration = Math.Floor(typeAndTime.Value * distance);
            Console.WriteLine($"The distance is: {distance} and will take {durration}ms with {typeAndTime.Key}");

            // sqrt(2) = 1.44
            //sqrt (5) = 2.2775
        }

        public KeyValuePair<Units, int> GetSlowestUnit(Dictionary<Units, int> units) {
            var listOfTypesOrderedByLeast = units.Where(pair => pair.Value > 0);
            var slowest = 0;

            var slowestType = Enums.Units.Snob;
            foreach (var unit in listOfTypesOrderedByLeast) {
                var currentSpeed = GetUnitSpeedFromType(unit.Key);

                if (currentSpeed <= slowest) continue;

                slowest = currentSpeed;
                slowestType = unit.Key;
            }

            return new KeyValuePair<Units, int>(slowestType, slowest);
        }

        public int GetUnitSpeedFromType(Units type) {
            switch (type) {
                case Enums.Units.Spear:
                    return Constants.Units.Spear.SpeedInMs;
                case Enums.Units.Sword:
                    return Constants.Units.Sword.SpeedInMs;
                case Enums.Units.Axe:
                    return Constants.Units.Axe.SpeedInMs;
                case Enums.Units.Spy:
                    return Constants.Units.Spy.SpeedInMs;
                case Enums.Units.Light:
                    return Constants.Units.Light.SpeedInMs;
                case Enums.Units.Heavy:
                    return Constants.Units.Heavy.SpeedInMs;
                case Enums.Units.Ram:
                    return Constants.Units.Ram.SpeedInMs;
                case Enums.Units.Catapult:
                    return Constants.Units.Catapult.SpeedInMs;
                case Enums.Units.Knight:
                    return Constants.Units.Knight.SpeedInMs;
                case Enums.Units.Snob:
                    return Constants.Units.Snob.SpeedInMs;
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }
        }

    }

}