using System.Collections.Generic;

using TribalWarsBot.Enums;

namespace TribalWarsBot.Helpers {

    public class Misc {


        public static string GetUnitQueryStringFromUnitDict(Dictionary<Units, int> units) {
            return
                $"spear={units[Units.Spear]}&" +
                $"sword={units[Units.Sword]}&" +
                $"axe={units[Units.Axe]}&" +
                $"spy={units[Units.Spy]}&" +
                $"light={units[Units.Light]}&" +
                $"heavy={units[Units.Heavy]}&" +
                $"ram={units[Units.Ram]}&" +
                $"catapult={units[Units.Catapult]}&" +
                $"knight={units[Units.Knight]}&" +
                $"snob={units[Units.Snob]}";
        }
    }

}