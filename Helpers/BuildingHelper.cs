using System;

using TribalWarsBot.Screens;

namespace TribalWarsBot.Helpers {

    public static class BuildingHelper {

        public static string GetNameForType(BuildingTypes building) {
            switch (building) {
                case BuildingTypes.Main:
                    return "main";
                case BuildingTypes.Barracks:
                    return "barracks";
                case BuildingTypes.Smith:
                    return "smith";
                case BuildingTypes.Place:
                    return "place";
                case BuildingTypes.Statue:
                    return "statue";
                case BuildingTypes.Market:
                    return "market";
                case BuildingTypes.Wood:
                    return "wood";
                case BuildingTypes.Clay:
                    return "stone";
                case BuildingTypes.Iron:
                    return "iron";
                case BuildingTypes.Farm:
                    return "farm";
                case BuildingTypes.Storage:
                    return "storage";
                case BuildingTypes.Hide:
                    return "hide";
                case BuildingTypes.Wall:
                    return "wall";
                case BuildingTypes.Stable:
                    return "stable";
                case BuildingTypes.Garage:
                    return "garage";
                case BuildingTypes.Watchtower:
                    return "watchtower";
                case BuildingTypes.Snob:
                    return "snob";
                default:
                    throw new ArgumentOutOfRangeException(nameof(building), building, null);
            }
        }

    }

}